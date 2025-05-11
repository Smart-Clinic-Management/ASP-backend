using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Specializations.Command.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;
using SmartClinic.Application.Features.Specializations.Query.GetSpecialization;
using SmartClinic.Application.Features.Specializations.Query.GetSpecializations;
using SmartClinic.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class SpecializationsControllerTests
    {
        private readonly Mock<ISpecializationService> _mockSpecializationService;
        private readonly SpecializationsController _controller;
        private readonly Mock<IFormFile> _mockFormFile;

        public SpecializationsControllerTests()
        {
            _mockSpecializationService = new Mock<ISpecializationService>();
            _controller = new SpecializationsController(_mockSpecializationService.Object);

            // Mock IFormFile for image uploads
            _mockFormFile = new Mock<IFormFile>();
            _mockFormFile.Setup(f => f.FileName).Returns("test-image.jpg");
            _mockFormFile.Setup(f => f.Length).Returns(1024);
        }

        [Fact]
        public async Task CreateSpecialization_ReturnsCreatedResult()
        {
            // Arrange
            var request = new CreateSpecializationRequest(
                Name: "Neurology",
                Description: "Specializes in disorders of the nervous system",
                Image: _mockFormFile.Object
            );

            var expectedResponse = new Response<string>
            {
                Data = "Specialization created successfully",
                Message = "Specialization has been created",
                StatusCode = HttpStatusCode.Created
            };

            _mockSpecializationService
                .Setup(s => s.CreateSpecialization(request))
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
            var result = await _controller.CreateSpecialization(request);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
            createdResult.StatusCode.Should().Be(201);
            var response = createdResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Specialization has been created");
            response.Data.Should().Be("Specialization created successfully");
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int specializationId = 1;
            var doctors = new List<DoctorDto>
            {
                new DoctorDto(Id: 1, FirstName: "John", LastName: "Smith", Image: "doctor1.jpg"),
                new DoctorDto(Id: 2, FirstName: "Jane", LastName: "Doe", Image: "doctor2.jpg")
            };

            var expectedResponse = new Response<GetSpecializationByIdResponse?>
            {
                Data = new GetSpecializationByIdResponse(
                    Id: specializationId,
                    Name: "Neurology",
                    Description: "Specializes in disorders of the nervous system",
                    Image: "neurology.jpg",
                    Doctors: doctors
                ),
                Message = "Specialization retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockSpecializationService
                .Setup(s => s.GetSpecializationByIdAsync(specializationId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetById(specializationId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<GetSpecializationByIdResponse?>>().Subject;
            response.Message.Should().Be("Specialization retrieved successfully");
            response.Data.Should().NotBeNull();
            response.Data!.Id.Should().Be(specializationId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int specializationId = 999;
            var expectedResponse = new Response<GetSpecializationByIdResponse?>
            {
                Data = null,
                Message = "No Specialization With id : 999",
                StatusCode = HttpStatusCode.NotFound
            };

            _mockSpecializationService
                .Setup(s => s.GetSpecializationByIdAsync(specializationId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetById(specializationId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<Response<GetSpecializationByIdResponse?>>().Subject;
            response.Message.Should().Be("No Specialization With id : 999");
            response.Data.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAllDoctors_ReturnsOkResult()
        {
            // Arrange
            var parameters = new GetAllSpecializationsParams { PageIndex = 0, PageSize = 10 };
            var specializationsList = new List<GetAllSpecializationsResponse>
            {
                new GetAllSpecializationsResponse(1, "Neurology", "Specializes in disorders of the nervous system", "neurology.jpg"),
                new GetAllSpecializationsResponse(2, "Cardiology", "Specializes in disorders of the heart", "cardiology.jpg")
            };

            var pagination = new Pagination<GetAllSpecializationsResponse>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: specializationsList
            );

            var expectedResponse = new Response<Pagination<GetAllSpecializationsResponse>>
            {
                Data = pagination,
                Message = "Specializations retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockSpecializationService
                .Setup(s => s.GetAllSpecializationsAsync(parameters))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAllDoctors(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<GetAllSpecializationsResponse>>>().Subject;
            response.Message.Should().Be("Specializations retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateSpecialization_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int specializationId = 1;
            var request = new UpdateSpecializationRequest(
                Name: "Clinical Neurology",
                Description: "Updated description for neurology specialization",
                Image: _mockFormFile.Object
            );

            var expectedResponse = new Response<GetSpecializationByIdResponse?>
            {
                Data = new GetSpecializationByIdResponse(
                    Id: specializationId,
                    Name: "Clinical Neurology",
                    Description: "Updated description for neurology specialization",
                    Image: "neurology.jpg",
                    Doctors: new List<DoctorDto>()
                ),
                Message = "Specialization updated successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockSpecializationService
                .Setup(s => s.UpdateSpecializationAsync(specializationId, request))
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
            var result = await _controller.UpdateSpecialization(specializationId, request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<GetSpecializationByIdResponse?>>().Subject;
            response.Message.Should().Be("Specialization updated successfully");
            response.Data.Should().NotBeNull();
            response.Data!.Id.Should().Be(specializationId);
            response.Data.Name.Should().Be("Clinical Neurology");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteSpecialization_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int specializationId = 1;
            var expectedResponse = new Response<string>
            {
                Data = "Specialization deleted successfully",
                Message = "Specialization has been deleted",
                StatusCode = HttpStatusCode.OK
            };

            _mockSpecializationService
                .Setup(s => s.DeleteSpecializationAsync(specializationId))
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
            var result = await _controller.DeleteSpecialization(specializationId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<string>>().Subject;
            response.Message.Should().Be("Specialization has been deleted");
            response.Data.Should().Be("Specialization deleted successfully");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}