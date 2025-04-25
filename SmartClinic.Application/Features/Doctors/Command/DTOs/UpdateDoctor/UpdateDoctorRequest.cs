using System;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor
{
    public class UpdateDoctorRequest
    {
        public int SpecializationId { get; set; }

        public string? Description { get; set; }

        public int? WaitingTime { get; set; }

        public string? Address { get; set; }

        public DateOnly BirthDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhoneNumber { get; set; }
    }
}
