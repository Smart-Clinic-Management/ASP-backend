namespace SmartClinic.Application.Features.Doctors.Query.GetDoctors;

public class GetAllDoctorsResponse : IDto
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


    public GetAllDoctorsResponse(int id, string firstName, string? lastName, int age, string image, string specialization, string? description, int? waitingTime)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Image = image;
        Specialization = specialization;
        Description = description;
        WaitingTime = waitingTime;
    }
}