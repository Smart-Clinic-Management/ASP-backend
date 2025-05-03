using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule
{
    public record CreateDoctorScheduleResponse(
      int Id,
      string Message = "Schedule created successfully"
  );
}
