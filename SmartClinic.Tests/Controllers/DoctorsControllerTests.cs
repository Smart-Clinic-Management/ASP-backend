using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartClinic.API.Controllers;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Doctors.Command.CreateDoctor;
using SmartClinic.Application.Features.Doctors.Command.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;
using SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
using SmartClinic.Application.Features.DoctorsSchedules.DTOs;
using SmartClinic.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SmartClinic.Tests.Controllers
{
    public class DoctorsControllerTests
    {
        private readonly Mock<IDoctorService> _mockDoctorService;
        private readonly DoctorsController _controller;

        public DoctorsControllerTests()
        {
            _mockDoctorService = new Mock<IDoctorService>();
            _controller = new DoctorsController(_mockDoctorService.Object);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int doctorId = 1;
            var doctorSchedules = new List<DoctorScheduleDto>();

            var expectedResponse = new Response<GetDoctorByIdResponse?>
            {
                Data = new GetDoctorByIdResponse(
                    FirstName: "Dr. John",
                    LastName: "Smith",
                    UserEmail: "john.smith@example.com",
                    PhoneNumber: "1234567890",
                    Age: 40,
                    Birthdate: new DateOnly(1983, 1, 1),
                    Address: "123 Main St",
                    Description: "Cardiologist with 10 years of experience",
                    WaitingTime: 15,
                    Image: "profile.jpg",
                    SpecializationId: 1,
                    Specialization: "Cardiology",
                    Schedule: doctorSchedules
                ),
                Message = "Doctor retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockDoctorService
                .Setup(s => s.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetById(doctorId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<GetDoctorByIdResponse?>>().Subject;
            response.Message.Should().Be("Doctor retrieved successfully");
            response.Data.Should().NotBeNull();
            response.Data!.FirstName.Should().Be("Dr. John");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int doctorId = 999;
            var expectedResponse = new Response<GetDoctorByIdResponse?>
            {
                Data = null,
                Message = "No Doctor With id : 999",
                StatusCode = HttpStatusCode.NotFound
            };

            _mockDoctorService
                .Setup(s => s.GetDoctorByIdAsync(doctorId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetById(doctorId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<Response<GetDoctorByIdResponse?>>().Subject;
            response.Message.Should().Be("No Doctor With id : 999");
            response.Data.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAllDoctors_ReturnsOkResultWithPagination()
        {
            // Arrange
            var parameters = new GetAllDoctorsParams { PageIndex = 1, PageSize = 10 };
            var doctorsList = new List<GetAllDoctorsResponse>
            {
                new() { Id = 1, FirstName = "Dr. John", LastName = "Smith", Age = 40, Specialization = "Cardiology" },
                new() { Id = 2, FirstName = "Dr. Jane", LastName = "Doe", Age = 35, Specialization = "Neurology" }
            };

            var pagination = new Pagination<GetAllDoctorsResponse>(
                pageIndex: 0,
                pageSize: 10,
                count: 2,
                data: doctorsList
            );

            var expectedResponse = new Response<Pagination<GetAllDoctorsResponse>>
            {
                Data = pagination,
                Message = "Doctors retrieved successfully",
                StatusCode = HttpStatusCode.OK
            };

            _mockDoctorService
                .Setup(s => s.GetAllDoctorsAsync(parameters))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAllDoctors(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<Response<Pagination<GetAllDoctorsResponse>>>().Subject;
            response.Message.Should().Be("Doctors retrieved successfully");
            response.Data?.Data.Should().HaveCount(2);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}