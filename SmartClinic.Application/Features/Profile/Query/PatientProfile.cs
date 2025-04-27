using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Profile.Query
{
    public class PatientProfile : BaseProfile
    {
        public string MedicalHistory { get; set; } = null!;
    }
}
