using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Features.Doctors.Mapper;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Infrastructure.Repos;
public class DoctorRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    : GenericRepository<Doctor>(context),
    IDoctorRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<int> CountAsync(ISpecification<Doctor> specification)
    {
        return await base.CountAsync(specification.Criteria);
    }

    public override Task<bool> ExistsAsync(int doctorId)
      => context.Doctors.AnyAsync(x => x.Id == doctorId && x.IsActive);

    public Task<Doctor?> GetByIdAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive,
            includes: nameof(Doctor.User));

    public Task<Doctor?> GetByIdNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
            nameof(Doctor.User));

    public Task<Doctor?> GetByIdWithIncludesAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, true,
          nameof(Doctor.User), nameof(Doctor.Specialization), nameof(Doctor.DoctorSchedules));

    public Task<Doctor?> GetByIdWithIncludesNoTrackingAsync(int id)
        => base.GetSingleAsync(x => x.Id == id && x.IsActive, false,
              nameof(Doctor.User), nameof(Doctor.Specialization));

    public async Task<Doctor?> GetDoctorWithSpecificScheduleAsync(int doctorId, DateOnly appointmentDate, TimeOnly startTime)
    {
        return await context.Doctors.AsNoTracking()
                .Where(x => x.Id == doctorId && x.IsActive)
                .Include(x => x.DoctorSchedules
                            .Where(s => s.DayOfWeek == appointmentDate.DayOfWeek &&
                             s.StartTime <= startTime &&
                             s.EndTime >= startTime.AddMinutes(s.SlotDuration)))
                .Include(x => x.Appointments
                            .Where(a => a.AppointmentDate == appointmentDate &&
                            a.Duration.StartTime <= startTime &&
                            a.Duration.EndTime > startTime))
                .FirstOrDefaultAsync();

    }

    public async Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate)
    {
        var endDate = startDate.AddDays(3);

        return await context.Doctors.AsNoTracking()
            .Where(x => x.Id == id && x.IsActive)
            .Include(x => x.Specialization)
            .Include(x => x.DoctorSchedules)
            .Include(x => x.User)
            .Include(x => x.Appointments
            .Where(a => a.AppointmentDate >= startDate &&
                                 a.AppointmentDate < endDate))
            .FirstOrDefaultAsync();
    }


    public async Task<bool> IsValidDoctorSpecialization(int specializationId, int doctorId)
        => await context.Doctors.AsNoTracking().AnyAsync(x => x.Id == doctorId &&
                  x.SpecializationId == specializationId && x.IsActive);


    public async Task<IEnumerable<GetAllDoctorsResponse>> ListNoTrackingAsync(GetAllDoctorsParams doctorsParams, ISpecification<Doctor> specification)
    {
        var result = await base.ListAllAsync(x => new GetAllDoctorsResponse()
        {
            Id = x.Id,
            Age = x.User.Age,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Image = DoctorMappingExtensions.GetImgUrl(x.User.ProfileImage, _httpContextAccessor),
            Specialization = x.Specialization.Name,
            Description = x.Specialization.Description,
            WaitingTime = x.WaitingTime
        }
        ,
        specification.Criteria,

          doctorsParams.PageSize,
          doctorsParams.PageIndex,
          doctorsParams.OrderBy,
          doctorsParams.IsDescending
        );
        return result;
    }



}

