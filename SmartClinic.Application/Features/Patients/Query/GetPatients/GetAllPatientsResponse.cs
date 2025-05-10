using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.GetPatients
{
    public class GetAllPatientsResponse 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public int Age { get; set; }

        public GetAllPatientsResponse()
        {
        }

        public GetAllPatientsResponse(int id, string firstName, string lastName, string userPhoneNumber, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserPhoneNumber = userPhoneNumber;
            Age = age;
        }
    }

}