using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EClinic.Models.Domain;
using EClinic.Repositories;
using EClinic.Data;

namespace EClinic.Realizations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicContext _ClinicContext;

        public DoctorRepository(ClinicContext clinicContext)
        {
            _ClinicContext = clinicContext;
        }

        public async Task<Doctor> GetDoctorAsync(int Id)
        {
            return await _ClinicContext.Doctors.FindAsync(Id);
        }

        public async Task<Doctor> GetDoctorByUserIDAsync(string userId)
        {
            return await _ClinicContext.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<List<Doctor>> GetDoctorsByTypesAsync(int typeId)
        {
            return await _ClinicContext.Doctors.Where(d => d.DoctorTypeId == typeId).ToListAsync();
        }
    }
}
