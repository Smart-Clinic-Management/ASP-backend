using SmartClinic.Application.Features.Patients.Query.DTOs;

namespace SmartClinic.Application.Features.Patients.Mapper;

public static class PatientMappingExtensions
{
    public static GetAllPatientsResponse ToGetAllPatientsResponse(this Patient patient, IHttpContextAccessor httpContextAccessor)
    {
        var appointmentsDto = patient.Appointments.Select(appt =>
        {
            var doctor = appt.Doctor;
            var imageUrl = DoctorMappingExtensions.GetImgUrl(doctor.User.ProfileImage, httpContextAccessor);

            var specializationName = doctor.Specialization.Name;

            var doctorDto = new DoctorInAppointmentDto(
                doctor.Id,
                doctor.User.FirstName,
                doctor.User.LastName,
                imageUrl,
                specializationName
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
