namespace SmartClinic.Application.Features.Doctors.Query.GetDoctors;

public class GetAllDoctorsResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string Image { get; set; }
    public string Specialization { get; set; }
    public string? Description { get; set; }
    public int? WaitingTime { get; set; }



    public GetAllDoctorsResponse()
    {

    }

}