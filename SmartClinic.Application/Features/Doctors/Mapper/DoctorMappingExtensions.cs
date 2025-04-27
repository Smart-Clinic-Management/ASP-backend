using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;

namespace SmartClinic.Application.Features.Doctors.Mapper
{
    public static class DoctorMappingExtensions
    {
        public static Doctor UpdateDoctorWithRequest(this Doctor doctor, UpdateDoctorRequest request, string userId)
        {
            doctor.User.FirstName = request.FirstName;
            doctor.User.LastName = request.LastName;
            doctor.User.PhoneNumber = request.PhoneNumber;
            doctor.User.Email = request.Email;
            doctor.User.BirthDate = request.BirthDate;
            doctor.User.Address = request.Address;
            doctor.Description = request.Description;
            doctor.WaitingTime = request.WaitingTime;

            doctor.UserId = userId;
            doctor.Specializations.Clear();
            //    doctor.Specializations.AddRange(request.Specializations.Select(id => new Specialization { Id = id }));
            return doctor;
        }


        public static UpdateDoctorResponse ToUpdateDoctorResponse(this Doctor doctor)
        {
            return new UpdateDoctorResponse(
                FirstName: doctor.User.FirstName,
                LastName: doctor.User.LastName,
                PhoneNumber: doctor.User.PhoneNumber,
                Email: doctor.User.Email,
                BirthDate: doctor.User.BirthDate,
                Address: doctor.User.Address,
                Description: doctor.Description,
                WaitingTime: doctor.WaitingTime,
                Specializations: doctor.Specializations.Select(s => s.Id).ToList()
            );
        }



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

    }
}

