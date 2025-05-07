using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization
{

    public record CreateSpecializationRequest(
       [Required] string Name,
       [Required] string Description,
       [Required] IFormFile Image
        );

}
