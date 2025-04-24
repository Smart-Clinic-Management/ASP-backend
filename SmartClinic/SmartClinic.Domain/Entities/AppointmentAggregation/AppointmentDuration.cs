namespace SmartClinic.Domain.Entities.AppointmentAggregation;

public record AppointmentDuration
    (
    TimeOnly StartTime,
    TimeOnly EndTime
    );
