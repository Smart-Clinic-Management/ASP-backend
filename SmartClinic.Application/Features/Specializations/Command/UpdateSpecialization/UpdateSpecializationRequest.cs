using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization
{
    public record UpdateSpecializationRequest(
     string? Name,
     string? Description,
     IFormFile? Image
 );

}
