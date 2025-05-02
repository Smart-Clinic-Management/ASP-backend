using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.DoctorSchedule.Command.DeleteDoctorSchedule
{
    public class DeleteSchedulesResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public DeleteSchedulesResponse(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
