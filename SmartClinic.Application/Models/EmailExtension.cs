namespace SmartClinic.Application.Models;
public static class EmailExtension
{
    public static EmailMessage GeneratePatientAppointmentMessage(this ReceiverData receiverData, Appointment appointment, Doctor doctor)
    {
        return new EmailMessage
        {
            To = receiverData.Email,
            Subject = "Smart Clinic Appointment",
            Body = $"Hello {receiverData.Name} you have successfully added appointment with doctor {doctor.User.FirstName} " +
                 $"on {appointment.AppointmentDate.DayOfWeek} {appointment.AppointmentDate} at time from " +
                 $"{appointment.Duration.StartTime} to {appointment.Duration.EndTime}"
        };
    }

    public static EmailMessage GeneratePatientCancelAppointmentMessage(this ReceiverData receiverData, Appointment appointment)
    {
        return new EmailMessage
        {
            To = receiverData.Email,
            Subject = "Smart Clinic Appointment",
            Body = $"Hello {receiverData.Name} you have successfully canceled your appointment with doctor " +
            $"{appointment.Doctor.User.FirstName} on {appointment.AppointmentDate.DayOfWeek} " +
            $"at time from {appointment.Duration.StartTime} to {appointment.Duration.EndTime}"
        };
    }

    public static EmailMessage GeneratePatientCancelInformMessage(this Appointment appointment, string doctorName)
    {
        return new EmailMessage
        {
            To = appointment.Patient.User.Email!,
            Subject = "Smart Clinic Appointment",
            Body = $"Hello {appointment.Patient.User.FirstName} we are very sorry to inform you that your appointment with doctor " +
            $"{doctorName} on {appointment.AppointmentDate.DayOfWeek} " +
            $"at time from {appointment.Duration.StartTime} to {appointment.Duration.EndTime} has been canceled"
        };
    }

    public static EmailMessage GenerateDoctorCancelAppointmentMessage(this ReceiverData receiverData, Appointment appointment)
    {
        return new EmailMessage
        {
            To = appointment.Doctor.User.Email!,
            Subject = "Smart Clinic Appointment",
            Body = $"Hello Dr. {appointment.Doctor.User.FirstName} your appointment with patient {receiverData.Name} " +
            $"{appointment.Doctor.User.FirstName} on {appointment.AppointmentDate.DayOfWeek} " +
            $"at time from {appointment.Duration.StartTime} to {appointment.Duration.EndTime} canceled"
        };
    }

}
