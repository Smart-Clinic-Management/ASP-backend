using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Patients.Query.GetPatient;
using SmartClinic.Application.Features.Patients.Query.GetPatients;
using SmartClinic.Application.Services.Interfaces;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _mockPatientService;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _mockPatientService = new Mock<IPatientService>();
            _controller = new PatientsController(_mockPatientService.Object);
        }

        [Fact]
        public async Task GetAllPatients_ReturnsOkResult()
        {
            // Arrange
            var parameters = new GetAllPatientsParams { PageIndex = 0, PageSize = 10 };
            var patientsList = new List<GetAllPatientsResponse>
            {
                new GetAllPatientsResponse { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
                new GetAllPatientsResponse { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 25 }
            };

            var pagination = new Pagination<GetAllPatientsResponse>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: patientsList
            );

            var expectedResponse = new Response<Pagination<GetAllPatientsResponse>>
            {
                Data = pagination,
                Message = "Patients retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockPatientService
                .Setup(s => s.GetAllPatientsAsync(parameters))
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
            var result = await _controller.GetAllPatients(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<GetAllPatientsResponse>>>().Subject;
            response.Message.Should().Be("Patients retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPatientById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int patientId = 1;
            var expectedResponse = new Response<GetPatientByIdResponse?>
            {
                Data = new GetPatientByIdResponse(
                    id: patientId,
                    firstName: "John",
                    lastName: "Doe",
                    userEmail: "john.doe@example.com",
                    userPhoneNumber: "1234567890",
                    age: 30,
                    address: "123 Main St",
                    image: "profile.jpg",
                    medicalHistory: "No previous conditions"
                ),
                Message = "Patient retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockPatientService
                .Setup(s => s.GetPatientByIdAsync(patientId))
                .ReturnsAsync(expectedResponse);

            // Set doctor role for authorization
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "doctor")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetPatientById(patientId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<GetPatientByIdResponse?>>().Subject;
            response.Message.Should().Be("Patient retrieved successfully");
            response.Data.Should().NotBeNull();
            response.Data!.id.Should().Be(patientId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPatientById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int patientId = 999;
            var expectedResponse = new Response<GetPatientByIdResponse?>
            {
                Data = null,
                Message = "No Patient With id : 999",
                StatusCode = HttpStatusCode.NotFound
            };

            _mockPatientService
                .Setup(s => s.GetPatientByIdAsync(patientId))
                .ReturnsAsync(expectedResponse);

            // Set doctor role for authorization
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "doctor")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set controller user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _controller.GetPatientById(patientId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<Response<GetPatientByIdResponse?>>().Subject;
            response.Message.Should().Be("No Patient With id : 999");
            response.Data.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}