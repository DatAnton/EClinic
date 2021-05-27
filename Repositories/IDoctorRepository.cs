using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Models.Domain;

namespace EClinic.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetDoctorAsync(int Id);
        Task<List<Doctor>> GetDoctorsByTypesAsync(int typeId);
        Task<Doctor> GetDoctorByUserIDAsync(string userId);
    }
}
