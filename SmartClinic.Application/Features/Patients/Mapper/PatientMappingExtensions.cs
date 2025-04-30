using SmartClinic.Application.Features.Patients.Query.DTOs;
using SmartClinic.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.Application.Features.Patients.Mapper
{
    public static class PatientMappingExtensions
    {
        public static GetAllPatientsResponse ToGetAllPatientsResponse(this Patient patient, IHttpContextAccessor httpContextAccessor)
        {
            var appointmentsDto = patient.Appointments.Select(appt =>
            {
                var doctor = appt.Doctor;
                var imageUrl = DoctorMappingExtensions.GetImgUrl(doctor.User.ProfileImage, httpContextAccessor);

                var specializationNames = doctor.Specializations != null && doctor.Specializations.Any()
                    ? string.Join("، ", doctor.Specializations.Select(s => s.Name))
                    : "";

                var doctorDto = new DoctorInAppointmentDto(
                    doctor.Id,
                    doctor.User.FirstName,
                    doctor.User.LastName,
                    imageUrl,
                    specializationNames
                );

                return new PatientAppointmentDto(
                    appt.AppointmentDate,
                    appt.Duration,
                    appt.Status,
                    doctorDto
                );
            }).ToList();

            return new GetAllPatientsResponse(
                patient.Id,
                patient.User.FirstName,
                patient.User.LastName,
                patient.MedicalHistory,
                appointmentsDto
            );
        }
    }
}
