﻿namespace SmartClinic.Application.Services.Interfaces;

public interface IProfileService
{

    Task<Response<ProfileResponse>> GetProfile(string userId);

    Task<Response<ProfileResponse>> RemoveImg(string userid);

    Task<Response<ImgResponse>> UpdateProfileImg(ImgUpdateRequest request, string userid);

}
