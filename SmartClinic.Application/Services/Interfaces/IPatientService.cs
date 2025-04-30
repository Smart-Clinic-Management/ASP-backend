using SmartClinic.Application.Features.Patients.Query.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Response<List<GetAllPatientsResponse>>> GetAllPatientsAsync(int pageSize = 20, int pageIndex = 1);
    }
}
