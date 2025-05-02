using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization
{
    public class UpdateSpecializationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public IFormFile? Image { get; set; }
      

    }
}
