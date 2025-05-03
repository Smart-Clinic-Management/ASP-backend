using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Infrastructure.Repos;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Infrastructure.Interfaces;
using SmartClinic.Application.MappingExtensions;
using SmartClinic.Application.Features.DoctorSchedule.Command.DeleteDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule.SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;

namespace SmartClinic.Application.Services.Implementation
{
    public class DoctorScheduleServices : IDoctorScheduleService
    {
        private readonly IDoctorSchedule _doctorScheduleRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorScheduleServices(IDoctorSchedule doctorScheduleRepo, IUnitOfWork unitOfWork)
        {
            _doctorScheduleRepo = doctorScheduleRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId)
        {
            var schedules = await _doctorScheduleRepo.GetByDoctorIdAsync(doctorId);

            if (schedules == null || !schedules.Any())
            {
                return new Response<IEnumerable<GetDoctorSchedule>>(null, "No schedules found for the doctor.");
            }

            var scheduleDtos = schedules.Select(schedule => schedule.ToGetDoctorScheduleDto()).ToList();

            return new Response<IEnumerable<GetDoctorSchedule>>(scheduleDtos);
        }

        public async Task<DeleteSchedulesResponse> DeleteScheduleByIdAsync(int scheduleId)
        {
            var isDeleted = await _doctorScheduleRepo.SoftDeleteAsync(scheduleId);

            if (!isDeleted)
            {
                return new DeleteSchedulesResponse(false, $"No schedule found with ID {scheduleId}.");
            }

            return new DeleteSchedulesResponse(true, $"Schedule with ID {scheduleId} was deleted successfully.");
        }

        public async Task<CreateDoctorScheduleResponse> CreateAsync(CreateDoctorScheduleRequest request)
        {
            var schedule = request.ToEntity();

            try
            {
                await _doctorScheduleRepo.AddAsync(schedule);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the schedule.", ex);
            }

            return new CreateDoctorScheduleResponse(schedule.Id);
        }



        public async Task<UpdateDoctorScheduleResponse> UpdateAsync(UpdateDoctorScheduleRequest request)
        {
            var schedule = await _doctorScheduleRepo.GetByIdAsync(request.Id);

            if (schedule is null)
            {
                return new UpdateDoctorScheduleResponse(
                    Message: $"Schedule with ID {request.Id} not found."
                );
            }

            schedule.UpdateFromRequest(request);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new UpdateDoctorScheduleResponse(
                    Message: "An error occurred while updating the schedule."
                );
            }

            return new UpdateDoctorScheduleResponse();
        }



    }
}
