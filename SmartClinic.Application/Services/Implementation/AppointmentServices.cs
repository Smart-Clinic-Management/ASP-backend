using SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
using SmartClinic.Application.Features.Appointments.Mapper;
using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.PatientAppointments;

namespace SmartClinic.Application.Services.Implementation;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Response<string>> CreateAppointment(CreateAppointmentDto appointmentDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<AppointmentResponseDto>>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1)
    {
        var appointments = await _unitOfWork.Repository<IAppointment>().ListAllAppointmentsAsync(pageSize, pageIndex);
        var appointmentDtos = appointments.Select(a => a.ToDto()).ToList();

        return new ResponseHandler().Success(appointmentDtos);
    }

    public async Task<Response<List<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1)
    {
        var appointments = await _unitOfWork.Repository<IAppointment>().ListDoctorAppointmentsAsync(doctorId, pageSize, pageIndex);

        if (!appointments.Any())
            return new ResponseHandler().Success(new List<DoctorWithAppointmentsResponseDto>(), "");

        var appointmentDtos = appointments.Select(a => a.ToDoctorDto()).ToList();
        return new ResponseHandler().Success(appointmentDtos);
    }

    public async Task<Response<List<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, int pageSize = 20, int pageIndex = 1)
    {
        var appointments = await _unitOfWork.Repository<IAppointment>().ListPatientAppointmentsAsync(patientId, pageSize, pageIndex);

        if (!appointments.Any())
            return new ResponseHandler().Success(new List<PatientAppointmentsWithDoctorDetailsDto>(), "");

        var appointmentDtos = appointments.Select(a => a.ToPatientDto()).ToList();
        return new ResponseHandler().Success(appointmentDtos);
    }
}
