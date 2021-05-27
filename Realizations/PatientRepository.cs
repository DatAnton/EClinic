using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EClinic.Models.Domain;
using EClinic.Repositories;
using EClinic.Data;

namespace EClinic.Realizations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicContext _ClinicContext;

        public PatientRepository(ClinicContext clinicContext)
        {
            _ClinicContext = clinicContext;
        }

        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            return await _ClinicContext.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
