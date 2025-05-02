using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatient;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Response<GetDoctorSchedule>> GetPatientByIdAsync(int patientId);

        Task<Response<List<GetAllPatientsResponse>>> GetAllPatientsAsync(int pageSize = 20, int pageIndex = 1);


    }
}
