using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;

namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationsController(ISpecializationService _specializationService) : AppControllerBase
{

    [Authorize(Roles = "admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSpecialization([FromForm] CreateSpecializationRequest request)
    {
        var response = await _specializationService.CreateSpecialization(request);
        return NewResult(response);
    }




    [HttpGet("{id}")]
    [ProducesResponseType<Response<GetSpecializationByIdResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetSpecializationByIdResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await _specializationService.GetSpecializationByIdAsync(id));
    }






    [HttpGet]
    [ProducesResponseType<Response<Pagination<GetAllSpecializationsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDoctors([FromQuery] GetAllSpecializationsParams allSpecializationsParams)
    {
        return NewResult(await _specializationService.GetAllSpecializationsAsync(allSpecializationsParams));
    }


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
