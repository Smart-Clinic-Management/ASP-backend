using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Auth;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Domain.DTOs.Auth;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly Mock<ResponseHandler> _mockResponseHandler;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockProfileService = new Mock<IProfileService>();
            _mockResponseHandler = new Mock<ResponseHandler>();
            _controller = new AuthController(_mockResponseHandler.Object, _mockAuthService.Object, _mockProfileService.Object);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginRequest = new LoginRequestDTO
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            var expectedResponse = new Response<LoginResponseDTO>
            {
                Data = new LoginResponseDTO
                {
                    Token = "test-jwt-token"
                },
                Message = "Login successful",
                StatusCode = HttpStatusCode.OK
            };

            _mockAuthService
                .Setup(s => s.Login(loginRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<LoginResponseDTO>>().Subject;
            response.Message.Should().Be("Login successful");
            response.Data?.Token.Should().Be("test-jwt-token");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginRequest = new LoginRequestDTO
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };

            var expectedResponse = new Response<LoginResponseDTO>
            {
                Data = null,
                Message = "Invalid credentials",
                StatusCode = HttpStatusCode.BadRequest
            };

            _mockAuthService
                .Setup(s => s.Login(loginRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var response = badRequestResult.Value.Should().BeOfType<Response<LoginResponseDTO>>().Subject;
            response.Message.Should().Be("Invalid credentials");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_WithValidData_ReturnsCreatedResult()
        {
            // Arrange
            var registerRequest = new RegisterRequestDTO
            {
                Email = "newuser@example.com",
                Password = "NewPassword123!",
                ConfirmPassword = "NewPassword123!",
                Firstname = "John",
                Lastname = "Doe",
                Address = "123 Main St"
            };

            var expectedResponse = new Response<RegisterResponseDTO>
            {
                Data = new RegisterResponseDTO
                {
                    Id = 1,
                    Email = "newuser@example.com"
                },
                Message = "User registered successfully",
                StatusCode = HttpStatusCode.Created
            };

            _mockAuthService
                .Setup(s => s.Register(registerRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;
            createdResult.StatusCode.Should().Be(201);
            var response = createdResult.Value.Should().BeOfType<Response<RegisterResponseDTO>>().Subject;
            response.Message.Should().Be("User registered successfully");
            response.Data?.Id.Should().Be(1);
            response.Data?.Email.Should().Be("newuser@example.com");
        }
    }
}