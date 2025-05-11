using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;

namespace SmartClinic.Tests.Controllers
{
    public class ChatBotControllerTests
    {
        private readonly Mock<IGeminiService> _mockGeminiService;
        private readonly Mock<ILogger<ChatBotController>> _mockLogger;
        private readonly ChatBotController _controller;

        public ChatBotControllerTests()
        {
            _mockGeminiService = new Mock<IGeminiService>();
            _mockLogger = new Mock<ILogger<ChatBotController>>();
            _controller = new ChatBotController(_mockGeminiService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ExtractKeyword_WithValidQuestion_ReturnsOkResult()
        {
            // Arrange
            var questionRequest = new QuestionRequest
            {
                Question = "How can I schedule an appointment with a cardiologist?"
            };

            var keyword = "appointment scheduling";

            _mockGeminiService
                .Setup(s => s.ExtractKeywordAsync(questionRequest.Question))
                .ReturnsAsync(keyword);

            // Act
            var result = await _controller.ExtractKeyword(questionRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value;

            // Convert to JSON and back to an anonymous type to access the properties
            var json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<KeywordResponse>(json);

            deserializedResponse.Should().NotBeNull();
            deserializedResponse.Keyword.Should().Be(keyword);
        }

        [Fact]
        public async Task ExtractKeyword_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var controller = _controller;
            controller.ModelState.AddModelError("Question", "Required");

            var questionRequest = new QuestionRequest
            {
                Question = "" // Empty question
            };

            // Act
            var result = await controller.ExtractKeyword(questionRequest);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ExtractKeyword_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var questionRequest = new QuestionRequest
            {
                Question = "How can I schedule an appointment with a cardiologist?"
            };

            var expectedException = new Exception("AI service unavailable");

            _mockGeminiService
                .Setup(s => s.ExtractKeywordAsync(questionRequest.Question))
                .ThrowsAsync(expectedException);

            // Act
            var result = await _controller.ExtractKeyword(questionRequest);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(500);
            var response = statusCodeResult.Value;

            // Convert to JSON and back to an anonymous type to access the properties
            var json = JsonSerializer.Serialize(response);
            var deserializedResponse = JsonSerializer.Deserialize<ErrorResponse>(json);

            deserializedResponse.Should().NotBeNull();
            deserializedResponse.Error.Should().Be(expectedException.Message);
        }
    }

    // Add helper classes for deserialization
    public class KeywordResponse
    {
        public string Keyword { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}