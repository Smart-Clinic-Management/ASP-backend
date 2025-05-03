using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule
{
    public record CreateDoctorScheduleRequest(
       int DoctorId,
       DayOfWeek DayOfWeek,
       TimeOnly StartTime,
       TimeOnly EndTime,
       int SlotDuration
   );
}
