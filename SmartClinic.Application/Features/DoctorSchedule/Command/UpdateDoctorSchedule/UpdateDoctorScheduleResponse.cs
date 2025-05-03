using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule
{
    namespace SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule
    {
        public record UpdateDoctorScheduleResponse(
             string Message = "Schedule Updated successfully"
        );
    }


}
