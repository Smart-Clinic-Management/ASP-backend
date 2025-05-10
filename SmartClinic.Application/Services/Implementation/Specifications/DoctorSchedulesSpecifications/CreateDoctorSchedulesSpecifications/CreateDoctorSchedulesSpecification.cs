using SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;

namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSchedulesSpecifications.CreateDoctorSchedulesSpecifications;
public class CreateDoctorSchedulesSpecification(CreateDoctorScheduleRequest scheduleRequest)
    : BaseSpecification<DoctorSchedule>(x => x.DoctorId == scheduleRequest.DoctorId
    && x.DayOfWeek == scheduleRequest.DayOfWeek)
{
}
