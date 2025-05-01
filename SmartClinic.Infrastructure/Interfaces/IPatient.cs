
namespace SmartClinic.Infrastructure.Interfaces
{
    public interface IPatient : IRepository<Patient>
    {
        Task<bool> ExistsAsync(int patientId);
    }
}
