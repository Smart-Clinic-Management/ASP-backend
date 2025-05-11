using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
using SmartClinic.Application.Features.Appointments.Command.UpdateAppointmnet;
using SmartClinic.Application.Features.Appointments.Query.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Domain.DTOs;
using SmartClinic.Domain.Entities.AppointmentAggregation;
using SmartClinic.Application.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _mockAppointmentService;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockAppointmentService = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_mockAppointmentService.Object);
        }

        [Fact]
        public async Task GetAllAppointments_ReturnsOkResult()
        {
            // Arrange
            var parameters = new AllAppointmentsParams { PageIndex = 0, PageSize = 10 };
            var appointmentsList = new List<AllAppointmentsResponseDto>
            {
                new AllAppointmentsResponseDto(
                    AppointmentId: 1,
                    DoctorId: 1,
                    DoctorFullName: "Dr. Smith",
                    PatientId: 1,
                    PatientFullName: "John Doe",
                    SpecializationName: "Cardiology",
                    AppointmentDate: new DateOnly(2025, 5, 15),
                    StartTime: new TimeOnly(10, 0),
                    EndTime: new TimeOnly(10, 30),
                    Status: "Scheduled"
                ),
                new AllAppointmentsResponseDto(
                    AppointmentId: 2,
                    DoctorId: 2,
                    DoctorFullName: "Dr. Jones",
                    PatientId: 2,
                    PatientFullName: "Jane Doe",
                    SpecializationName: "Neurology",
                    AppointmentDate: new DateOnly(2025, 5, 16),
                    StartTime: new TimeOnly(11, 0),
                    EndTime: new TimeOnly(11, 30),
                    Status: "Scheduled"
                )
            };

            var pagination = new Pagination<AllAppointmentsResponseDto>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: appointmentsList
            );

            var expectedResponse = new Response<Pagination<AllAppointmentsResponseDto>>
            {
                Data = pagination,
                Message = "Appointments retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockAppointmentService
                .Setup(s => s.ListAllAppointmentsAsync(parameters))
                .ReturnsAsync(expectedResponse);

            // Set admin role for authorization
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetAllAppointments(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<AllAppointmentsResponseDto>>>().Subject;
            response.Message.Should().Be("Appointments retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDoctorAppointments_ReturnsOkResult()
        {
            // Arrange
            var doctorId = 1;
            var parameters = new GetDoctorAppointmentsParams { PageIndex = 0, PageSize = 10 };

            // Create test user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, doctorId.ToString()),
                new Claim(ClaimTypes.Role, "doctor"),
                new Claim(ClaimTypes.Email, "doctor@example.com"),
                new Claim(ClaimTypes.Name, "Dr. Smith")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var appointmentsList = new List<DoctorWithAppointmentsResponseDto>
            {
                new DoctorWithAppointmentsResponseDto(
                    Id: 1,
                    PatientId: 1,
                    PatientFullName: "John Doe",
                    AppointmentDate: new DateOnly(2025, 5, 15),
                    StartTime: new TimeOnly(10, 0),
                    EndTime: new TimeOnly(10, 30),
                    Status: AppointmentStatus.Pending.ToString()
                ),
                new DoctorWithAppointmentsResponseDto(
                    Id: 2,
                    PatientId: 2,
                    PatientFullName: "Jane Doe",
                    AppointmentDate: new DateOnly(2025, 5, 16),
                    StartTime: new TimeOnly(11, 0),
                    EndTime: new TimeOnly(11, 30),
                    Status: AppointmentStatus.Pending.ToString()
                )
            };

            var pagination = new Pagination<DoctorWithAppointmentsResponseDto>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: appointmentsList
            );

            var expectedResponse = new Response<Pagination<DoctorWithAppointmentsResponseDto>>
            {
                Data = pagination,
                Message = "Doctor appointments retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockAppointmentService
                .Setup(s => s.ListDoctorAppointmentsAsync(doctorId, parameters))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetDoctorAppointments(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<DoctorWithAppointmentsResponseDto>>>().Subject;
            response.Message.Should().Be("Doctor appointments retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPatientAppointments_ReturnsOkResult()
        {
            // Arrange
            var patientId = 1;
            var parameters = new GetPatientAppointmentsParams { PageIndex = 0, PageSize = 10 };

            // Create test user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patientId.ToString()),
                new Claim(ClaimTypes.Role, "patient"),
                new Claim(ClaimTypes.Email, "patient@example.com"),
                new Claim(ClaimTypes.Name, "John Doe")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var appointmentsList = new List<PatientAppointmentsWithDoctorDetailsDto>
            {
                new PatientAppointmentsWithDoctorDetailsDto(
                    AppointmentId: 1,
                    DoctorId: 1,
                    DoctorFullName: "Dr. Smith",
                    AppointmentDate: new DateOnly(2025, 5, 15),
                    StartTime: new TimeOnly(10, 0),
                    EndTime: new TimeOnly(10, 30),
                    Status: AppointmentStatus.Pending.ToString()
                ),
                new PatientAppointmentsWithDoctorDetailsDto(
                    AppointmentId: 2,
                    DoctorId: 2,
                    DoctorFullName: "Dr. Jones",
                    AppointmentDate: new DateOnly(2025, 5, 16),
                    StartTime: new TimeOnly(11, 0),
                    EndTime: new TimeOnly(11, 30),
                    Status: AppointmentStatus.Pending.ToString()
                )
            };

            var pagination = new Pagination<PatientAppointmentsWithDoctorDetailsDto>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: appointmentsList
            );

            var expectedResponse = new Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>
            {
                Data = pagination,
                Message = "Patient appointments retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockAppointmentService
                .Setup(s => s.ListPatientAppointmentsAsync(patientId, parameters))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetPatientAppointments(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>>().Subject;
            response.Message.Should().Be("Patient appointments retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateAppointment_ReturnsCreatedResult()
        {
            // Arrange
            var patientId = 1;
            var appointmentRequest = new CreateAppointmentRequest
            {
                DoctorId = 1,
                SpecializationId = 1,
                AppointmentDate = "2025-05-15",
                StartTime = new TimeOnly(10, 0)
            };

            var mailData = new MailData(
                Id: patientId,
                Name: "John Doe",
                Email: "patient@example.com"
            );

            // Create test user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patientId.ToString()),
                new Claim(ClaimTypes.Role, "patient"),
                new Claim(ClaimTypes.Email, "patient@example.com"),
                new Claim(ClaimTypes.Name, "John Doe")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var expectedResponse = new Response<string>
            {
                Data = "Appointment created successfully",
                Message = "Appointment has been scheduled",
                StatusCode = HttpStatusCode.Created
            };

            _mockAppointmentService
                .Setup(s => s.CreateAppointmentAsync(appointmentRequest, It.IsAny<MailData>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateAppointment(appointmentRequest);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
            createdResult.StatusCode.Should().Be(201);
            var response = createdResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Appointment has been scheduled");
            response.Data.Should().Be("Appointment created successfully");
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateAppointment_ReturnsOkResult()
        {
            // Arrange
            var doctorId = 1;
            var updateRequest = new UpdateAppointmentRequest(
                AppointmentId: 1,
                Status: AppointmentStatus.Completed
            );

            var mailData = new MailData(
                Id: doctorId,
                Name: "Dr. Smith",
                Email: "doctor@example.com"
            );

            // Create test user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, doctorId.ToString()),
                new Claim(ClaimTypes.Role, "doctor"),
                new Claim(ClaimTypes.Email, "doctor@example.com"),
                new Claim(ClaimTypes.Name, "Dr. Smith")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var expectedResponse = new Response<string>
            {
                Data = "Appointment updated successfully",
                Message = "Appointment has been updated",
                StatusCode = HttpStatusCode.OK
            };

            _mockAppointmentService
                .Setup(s => s.UpdateDoctorAppointmentAsync(It.IsAny<MailData>(), updateRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdateAppointment(updateRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Appointment has been updated");
            response.Data.Should().Be("Appointment updated successfully");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteAppointment_ReturnsOkResult()
        {
            // Arrange
            var patientId = 1;
            var appointmentId = 1;

            var mailData = new MailData(
                Id: patientId,
                Name: "John Doe",
                Email: "patient@example.com"
            );

            // Create test user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patientId.ToString()),
                new Claim(ClaimTypes.Role, "patient"),
                new Claim(ClaimTypes.Email, "patient@example.com"),
                new Claim(ClaimTypes.Name, "John Doe")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var expectedResponse = new Response<string>
            {
                Data = "Appointment cancelled successfully",
                Message = "Appointment has been cancelled",
                StatusCode = HttpStatusCode.OK
            };

            _mockAppointmentService
                .Setup(s => s.CancelPatientAppointmentAsync(It.IsAny<MailData>(), appointmentId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DeleteAppointment(appointmentId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Appointment has been cancelled");
            response.Data.Should().Be("Appointment cancelled successfully");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}