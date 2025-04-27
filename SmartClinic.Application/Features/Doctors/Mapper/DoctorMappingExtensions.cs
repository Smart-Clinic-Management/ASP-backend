

namespace SmartClinic.Application.Features.Doctors.Mapper
{
    public static class DoctorMappingExtensions
    {
        
        public static GetDoctorByIdResponse ToGetDoctorByIdResponse(this Doctor doctor)
        {
            return new GetDoctorByIdResponse(
                firstName: doctor.User.FirstName,
                lastName: doctor.User.LastName,
                userEmail: doctor.User.Email,
                userPhoneNumber: doctor.User.PhoneNumber,
                birthDate: doctor.User.BirthDate,
                address: doctor.User.Address,
                description: doctor.Description,
                waitingTime: doctor.WaitingTime,
                image: doctor.User.ProfileImage,
                SlotDuration: doctor.DoctorSchedules.FirstOrDefault()?.SlotDuration
            );
        }


        public static GetAllDoctorsResponse ToGetAllDoctorsResponse(this Doctor doctor)
        {
            return new GetAllDoctorsResponse(
                firstName: doctor.User.FirstName,
                lastName: doctor.User.LastName,
                image: doctor.User.ProfileImage,
                Specializations: doctor.Specializations.Select(s => s.Name).ToList()
            );
        }

        public static UpdateDoctorResponse ToUpdateDoctorResponse(this Doctor doctor, IHttpContextAccessor _httpContextAccessor)
        {
            return new UpdateDoctorResponse(
                Fname: doctor.User.FirstName,
                Lname: doctor.User.LastName,
                Email: doctor.User.Email,
                Image: doctor.User.ProfileImage != null ? GetImgUrl(doctor.User.ProfileImage, _httpContextAccessor) : null,
                Specialization: doctor.Specializations.Select(s => s.Id).ToList(),
                BirthDate: doctor.User.BirthDate,
                Address: doctor.User.Address,
                WaitingTime: doctor.WaitingTime,
                Description: doctor.Description
            );
        }


        public static string GetImgUrl(string? path, IHttpContextAccessor _httpContextAccessor)
        {
            if (path == null) return null!;

            var request = _httpContextAccessor.HttpContext?.Request;
            return $"{request!.Scheme}://{request.Host}/{path.Replace("\\", "/")}";
        }



    }
}

