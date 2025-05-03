using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule
{
    public record UpdateDoctorScheduleRequest(
      int Id,
      DayOfWeek? Day,
      TimeOnly? StartTime,
      TimeOnly? EndTime,
      int DoctorId,
      int SlotDuration
  );

}
