using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.GetPatients
{
  public class GetAllPatientsParams : PagingParams
    {
        public string? PatientName { get; set; }
    }
}
