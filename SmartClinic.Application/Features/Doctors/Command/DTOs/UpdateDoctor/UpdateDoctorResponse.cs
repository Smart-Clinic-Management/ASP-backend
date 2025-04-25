using System;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor
{
    public class UpdateDoctorResponse
    {
        public int Id { get; set; }

        public int SpecializationId { get; set; }

        public string? Description { get; set; }

        public int? WaitingTime { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string UserPhoneNumber { get; set; } = string.Empty;
    }
}
