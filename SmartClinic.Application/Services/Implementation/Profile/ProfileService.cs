using SmartClinic.Application.Features.Auth.Command;
using SmartClinic.Application.Features.Profile.Command;
using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Services.Implementation.Profile
{
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
            var user = await userMGR.FindByEmailAsync(userId);

            if (user == null)
            {
                return response.NotFound<ProfileResponse>();
            }

            var role = await userMGR.GetRolesAsync(user);


            profiles.TryGetValue(role.FirstOrDefault()!, out var fetcher);

            var result = new ProfileResponse
            {
                details = await fetcher!.FetchAsync(user)
            };

            return response.Success<ProfileResponse>(result);
        }


        public async Task<Response<ProfileResponse>> RemoveImg(string id)
        {

            var user = await userMGR.FindByEmailAsync(id);
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

            var user = await userMGR.FindByEmailAsync(userid!);

            if (user == null)
            {
                return response.NotFound<ImgResponse>("user not found")!;
            }

            var fileValidationOptions = new FileValidation
            {
                MaxSize = 2 * 1024 * 1024,
                AllowedExtenstions = new[] { ".jpg", ".jpeg", ".png" }
            };

            var fileResult = await fileHnadler.HanldeFile(request.File!, fileValidationOptions);

            if (!fileResult.Success)
            {
                return response.BadRequest<ImgResponse>([fileResult.Error!])!;
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
                return response.BadRequest<ImgResponse>(["Somthing Went Wrong"])!;
            }



            var removeResult = await fileHnadler.RemoveImg(exImg!);
            if (!removeResult && exImg != null)
            {
                return response.BadRequest<ImgResponse>([], Message: "update faild");
            }

            await fileHnadler.SaveFile(request.File, fileResult.FullFilePath!);

            var imgUrl = fileHnadler.GetFileURL(user.ProfileImage!);
            return response.Success<ImgResponse>(new ImgResponse { profileImg = imgUrl }, "updated success");

        }

    }
}
