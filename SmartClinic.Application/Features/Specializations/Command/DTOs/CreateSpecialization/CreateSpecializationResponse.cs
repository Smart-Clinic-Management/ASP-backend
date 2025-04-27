using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization
{
    public class CreateSpecializationResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
