
namespace SmartClinic.Application.Features.Doctors.Mapper
{
    public static class DoctorMappingExtensions
    {
        public static Doctor ToDoctor(this CreateDoctorRequest request, string userId)
        {
            return new Doctor
            {
                Description = request.Description,
                WaitingTime = request.WaitingTime,
                UserId = userId,
                Specializations = new List<Specialization>()
            };
        }

        public static void UpdateDoctorFromRequest(this Doctor doctor, UpdateDoctorRequest request)
        {
            doctor.Description = request.Description ?? doctor.Description;
            doctor.WaitingTime = request.WaitingTime ?? doctor.WaitingTime;

            if (doctor.User != null)
            {
                doctor.User.FirstName = request.FirstName ?? doctor.User.FirstName;
                doctor.User.LastName = request.LastName ?? doctor.User.LastName;
                doctor.User.Email = request.UserEmail ?? doctor.User.Email;
                doctor.User.PhoneNumber = request.UserPhoneNumber ?? doctor.User.PhoneNumber;
                doctor.User.Address = request.Address ?? doctor.User.Address;
            }

        }
    }
}
