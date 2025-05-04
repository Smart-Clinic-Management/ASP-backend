using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
using SmartClinic.Application.Services.Interfaces;
using System.Net;
using System.Security.Claims;

namespace Testing.Controllers;

public class AppointmentsControllerTests
{
    private readonly Mock<IAppointmentService> _mockAppointmentService;
    private readonly AppointmentsController _controller;

    public AppointmentsControllerTests()
    {
        _mockAppointmentService = new Mock<IAppointmentService>();
        _controller = new AppointmentsController(_mockAppointmentService.Object);

        // Setup default user claims to mock authorization
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "patient")
        }, "mock"));

        // Set up controller context with the mocked user
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task CreateAppointment_ValidRequest_ReturnsCreatedResult()
    {
        // Arrange
        var appointmentDto = new CreateAppointmentDto
        {
            DoctorId = 1,
            SpecializationId = 2,
            AppointmentDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            StartTime = TimeOnly.FromTimeSpan(new TimeSpan(10, 0, 0))
        };

        var expectedResponse = new Response<string>
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Created",
            Data = "Created"
        };

        _mockAppointmentService
            .Setup(service => service.CreateAppointmentAsync(
                It.IsAny<CreateAppointmentDto>(),
                It.IsAny<int>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateAppointment(appointmentDto);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);

        var response = Assert.IsType<Response<string>>(createdResult.Value);
        Assert.Equal("Created", response.Data);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Verify the service was called with correct parameters
        _mockAppointmentService.Verify(
            service => service.CreateAppointmentAsync(
                It.Is<CreateAppointmentDto>(dto =>
                    dto.DoctorId == appointmentDto.DoctorId &&
                    dto.SpecializationId == appointmentDto.SpecializationId),
                1), // PatientId from the mocked user claims
            Times.Once);
    }

    [Fact]
    public async Task CreateAppointment_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var appointmentDto = new CreateAppointmentDto
        {
            DoctorId = 1,
            SpecializationId = 2,
            AppointmentDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            StartTime = TimeOnly.FromTimeSpan(new TimeSpan(10, 0, 0))
        };

        var expectedResponse = new Response<string>
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = "Error",
            Errors = new List<string> { "Appointment not available" }
        };

        _mockAppointmentService
            .Setup(service => service.CreateAppointmentAsync(
                It.IsAny<CreateAppointmentDto>(),
                It.IsAny<int>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateAppointment(appointmentDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

        var response = Assert.IsType<Response<string>>(badRequestResult.Value);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response.Errors);
        Assert.Contains("Appointment not available", response.Errors);
    }

    [Fact]
    public async Task CreateAppointment_ServiceThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        var appointmentDto = new CreateAppointmentDto
        {
            DoctorId = 1,
            SpecializationId = 2,
            AppointmentDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            StartTime = TimeOnly.FromTimeSpan(new TimeSpan(10, 0, 0))
        };

        _mockAppointmentService
            .Setup(service => service.CreateAppointmentAsync(
                It.IsAny<CreateAppointmentDto>(),
                It.IsAny<int>()))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _controller.CreateAppointment(appointmentDto));
    }
}