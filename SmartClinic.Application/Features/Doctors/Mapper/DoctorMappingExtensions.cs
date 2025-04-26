

using Microsoft.AspNetCore.Identity;

namespace SmartClinic.Application.Features.Doctors.Mapper
{
    public static class DoctorMappingExtensions
    {
        public static Doctor UpdateDoctorWithRequest(this Doctor doctor, UpdateDoctorRequest request, string userId)
        {
            doctor.User.FirstName = request.Fname;
            doctor.User.LastName = request.Lname;
            doctor.User.Email = request.Email;
            doctor.User.BirthDate = request.BirthDate;
            doctor.User.Address = request.Address;
            doctor.Description = request.Description;
            doctor.WaitingTime = request.WaitingTime;

            return doctor;
        }

        public static UpdateDoctorResponse ToUpdateDoctorResponse(this Doctor doctor)
        {
            return new UpdateDoctorResponse(
                Fname: doctor.User.FirstName,
                Lname: doctor.User.LastName,
                Email: doctor.User.Email,
                Image: null, 
                Specialization: doctor.Specializations.Select(s => s.Id).ToList(),
                BirthDate: doctor.User.BirthDate,
                Address: doctor.User.Address,
                WaitingTime: doctor.WaitingTime,
                Description: doctor.Description
            );
        }
    


<<<<<<< Updated upstream

      public static GetDoctorByIdResponse ToGetDoctorByIdResponse(this Doctor doctor)
=======
        public static GetDoctorByIdResponse ToGetDoctorByIdResponse(this Doctor doctor)
>>>>>>> Stashed changes
        {
            return new GetDoctorByIdResponse(
                FirstName: doctor.User.FirstName,
                LastName: doctor.User.LastName,
                PhoneNumber: doctor.User.PhoneNumber,
                Email: doctor.User.Email,
                BirthDate: doctor.User.BirthDate,
                Address: doctor.User.Address,
                Description: doctor.Description,
                WaitingTime: doctor.WaitingTime,
                ProfileImage: doctor.User.ProfileImage,
                SlotDuration: doctor.DoctorSchedules.FirstOrDefault()?.SlotDuration
            );

        }

        public static GetAllDoctorsResponse ToGetAllDoctorsResponse(this Doctor doctor)
        {
            return new GetAllDoctorsResponse(
                FirstName: doctor.User.FirstName,
                LastName: doctor.User.LastName,
                ProfileImage: doctor.User.ProfileImage,
                Specializations: doctor.Specializations.Select(s => s.Name).ToList()
            );
        }
    }
}

