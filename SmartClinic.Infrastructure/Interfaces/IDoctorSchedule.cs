using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Infrastructure.Interfaces
{
    public interface IDoctorSchedule : IRepository<DoctorSchedule>
    {
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(int doctorId);
    }
}
