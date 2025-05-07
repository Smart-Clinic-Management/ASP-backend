using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;
using SmartClinic.Application.Features.Specializations.Query.GetSpecialization;

namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationController(ISpecializationService _specializationService) : AppControllerBase
{

    //[Authorize(Roles = "admin")]
    //[HttpPost]
    //[Consumes("multipart/form-data")]
    //[Route("Create")]
    //[ProducesResponseType<Response<CreateSpecializationResponse>>(StatusCodes.Status201Created)]
    //[ProducesResponseType<Response<CreateSpecializationResponse>>(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateSpecialization([FromForm] CreateSpecializationRequest request)
    //{
    //    var response = await _specializationService.CreateSpecializationAsync(request);
    //    return NewResult(response);
    //}



    [HttpGet("{id}")]
    [ProducesResponseType<Response<GetSpecializationByIdResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetSpecializationByIdResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await _specializationService.GetSpecializationByIdAsync(id));
    }



    //[HttpGet("GetAll")]
    //[ProducesResponseType<Response<List<CreateSpecializationResponse>>>(StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetAllSpecializations()
    //{
    //    var response = await _specializationService.GetAllSpecializationsAsync();
    //    return NewResult(response);
    //}


    //[Authorize(Roles = "admin")]
    //[HttpPut("Update/{id}")]
    //[ProducesResponseType<Response<UpdateSpecializationResponse>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<UpdateSpecializationResponse>>(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType<Response<UpdateSpecializationResponse>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateSpecialization(int id, [FromForm] UpdateSpecializationRequest request)
    //{
    //    var response = await _specializationService.UpdateSpecializationAsync(id, request);
    //    return NewResult(response);
    //}


    //[Authorize(Roles = "admin")]
    //[HttpDelete("Delete/{id}")]
    //[ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<string>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteSpecialization(int id)
    //{
    //    var response = await _specializationService.DeleteSpecializationAsync(id);
    //    return NewResult(response);
   // }

}
