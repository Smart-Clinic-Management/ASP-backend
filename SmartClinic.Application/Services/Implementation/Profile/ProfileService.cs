﻿namespace SmartClinic.Application.Services.Implementation.Profile;

internal class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> userMGR;
    private readonly ResponseHandler response;
    private readonly IFileHandlerService fileHnadler;
    private readonly Dictionary<string, IFetchProfile> profiles;

    public ProfileService(UserManager<AppUser> userMGR, ResponseHandler response, IEnumerable<IFetchProfile> profiles, IFileHandlerService fileHnadler)
    {
        this.userMGR = userMGR;
        this.response = response;
        this.fileHnadler = fileHnadler;
        this.profiles = profiles.ToDictionary(p => p.Role.ToLowerInvariant());
    }


    public async Task<Response<ProfileResponse>> GetProfile(string userId)
    {
        //var user = await userMGR.Users.Include( user => user. )



        var userExist = await userMGR.FindByIdAsync(userId);

        if (userExist == null)
        {
            return response.NotFound<ProfileResponse>();
        }

        var role = await userMGR.GetRolesAsync(userExist);

        var userWithInlcude = userMGR.Users.Where(user => user.Id == userExist.Id).AsQueryable();

        foreach (var item in role)
        {
            switch (item)
            {
                case "patient":
                    userWithInlcude = userWithInlcude.Include(user => user.Patient);
                    break;
                case "doctor":
                    userWithInlcude = userWithInlcude.Include(user => user.Doctor).ThenInclude(doctor => doctor!.Specialization);
                    break;
            }
        }

        profiles.TryGetValue(role.FirstOrDefault()!, out var fetcher);

        var result = new ProfileResponse
        {
            details = await fetcher!.FetchAsync(userWithInlcude.FirstOrDefault()!)
        };

        return response.Success<ProfileResponse>(result);
    }


    public async Task<Response<ProfileResponse>> RemoveImg(string id)
    {

        var user = await userMGR.FindByIdAsync(id);
        if (user == null)
        {
            return response.NotFound<ProfileResponse>("User not found")!;
        }
        var imgPath = user.ProfileImage;
        if (imgPath != null)
        {
            var fileResult = await fileHnadler.RemoveImg(imgPath);
            if (!fileResult)
            {

                return response.NotFound<ProfileResponse>("File Not Found")!;
            }
        }
        user.ProfileImage = null;
        await userMGR.UpdateAsync(user);

        return response.Success<ProfileResponse>(null!, "Profile Img Removed");

    }

    public async Task<Response<ImgResponse>> UpdateProfileImg(ImgUpdateRequest request, string userid)
    {

        var user = await userMGR.FindByIdAsync(userid!);

        if (user == null)
        {
            return response.NotFound<ImgResponse>("user not found")!;
        }

        var fileValidationOptions = new FileValidation
        {
            MaxSize = 2 * 1024 * 1024,
            AllowedExtenstions = new[] { ".jpg", ".jpeg", ".png" }
        };

        var fileResult = await fileHnadler.HandleFile(request.File!, fileValidationOptions);

        if (!fileResult.Success)
        {
            return response.BadRequest<ImgResponse>(errors: fileResult.Error!)!;
        }

        //if (user.ProfileImage != null)
        //{
        //    var fileResult2 = await fileHnadler.RemoveImg(user.ProfileImage);
        //    if (!fileResult2)
        //    {
        //        return response.NotFound<ProfileResponse>("File Not Found")!;
        //    }
        //}
        var exImg = user.ProfileImage;
        user.ProfileImage = fileResult.RelativeFilePath;

        var result = await userMGR.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return response.BadRequest<ImgResponse>("Something Went Wrong")!;
        }



        var removeResult = await fileHnadler.RemoveImg(exImg!);
        if (!removeResult && exImg != null)
        {
            return response.BadRequest<ImgResponse>("update failed");
        }

        await fileHnadler.SaveFile(request.File, fileResult.FullFilePath!);

        var imgUrl = fileHnadler.GetFileURL(user.ProfileImage!);
        return response.Success<ImgResponse>(new ImgResponse { profileImg = imgUrl }, "updated success");

    }

}
