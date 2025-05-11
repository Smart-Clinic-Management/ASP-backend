using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Command.DeleteDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Query.GetDoctorSchedule;
using SmartClinic.Application.Services.Interfaces;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class DoctorScheduleControllerTests
    {
        private readonly Mock<IDoctorScheduleService> _mockDoctorScheduleService;
        private readonly DoctorScheduleController _controller;

        public DoctorScheduleControllerTests()
        {
            _mockDoctorScheduleService = new Mock<IDoctorScheduleService>();
            _controller = new DoctorScheduleController(_mockDoctorScheduleService.Object);

            // Setup admin claims for authorization
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
        }

        [Fact]
        public async Task DeleteScheduleById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var deleteRequest = new DeleteDoctorScheduleRequest(
                ScheduleId: 1,
                DoctorId: 1
            );

            var expectedResponse = new Response<string>
            {
                Data = "Schedule deleted successfully",
                Message = "Schedule has been removed",
                StatusCode = HttpStatusCode.OK
            };

            _mockDoctorScheduleService
                .Setup(s => s.DeleteScheduleAsync(deleteRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DeleteScheduleById(deleteRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Schedule has been removed");
            response.Data.Should().Be("Schedule deleted successfully");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteScheduleById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var deleteRequest = new DeleteDoctorScheduleRequest(
                ScheduleId: 999,
                DoctorId: 1
            );

            var expectedResponse = new Response<string>
            {
                Data = null,
                Message = "Schedule not found",
                StatusCode = HttpStatusCode.NotFound
            };

            _mockDoctorScheduleService
                .Setup(s => s.DeleteScheduleAsync(deleteRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DeleteScheduleById(deleteRequest);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Schedule not found");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateDoctorSchedule_ReturnsCreatedResult()
        {
            // Arrange
            var createRequest = new CreateDoctorScheduleRequest(
                DoctorId: 1,
                DayOfWeek: DayOfWeek.Monday,
                StartTime: new TimeOnly(9, 0),
                EndTime: new TimeOnly(17, 0),
                SlotDuration: 30
            );

            var expectedResponse = new Response<GetDoctorSchedule>
            {
                Data = new GetDoctorSchedule(
                    Id: 1,
                    DoctorId: 1,
                    DayOfWeek: "Monday",
                    StartTime: new TimeOnly(9, 0),
                    EndTime: new TimeOnly(17, 0),
                    SlotDuration: 30
                ),
                Message = "Doctor schedule created successfully",
                StatusCode = HttpStatusCode.Created
            };

            _mockDoctorScheduleService
                .Setup(s => s.CreateAsync(createRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateDoctorSchedule(createRequest);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
            createdResult.StatusCode.Should().Be(201);
            var response = createdResult.Value.Should().BeOfType<Response<GetDoctorSchedule>>().Subject;
            response.Message.Should().Be("Doctor schedule created successfully");
            response.Data.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateDoctorSchedule_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var createRequest = new CreateDoctorScheduleRequest(
                DoctorId: 0, // Invalid ID
                DayOfWeek: DayOfWeek.Monday,
                StartTime: new TimeOnly(9, 0),
                EndTime: new TimeOnly(17, 0),
                SlotDuration: 30
            );

            var expectedResponse = new Response<GetDoctorSchedule>
            {
                Data = null,
                Message = "Invalid doctor ID",
                StatusCode = HttpStatusCode.BadRequest
            };

            _mockDoctorScheduleService
                .Setup(s => s.CreateAsync(createRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateDoctorSchedule(createRequest);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var response = badRequestResult.Value.Should().BeOfType<Response<GetDoctorSchedule>>().Subject;
            response.Message.Should().Be("Invalid doctor ID");
            response.Data.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}