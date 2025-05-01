using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Infrastructure.Interfaces
{
    public interface ISpecializaionRepository : IRepository<Specialization>
    {
        Task<bool> ExistsAsync(int specializationId);
    }
}
