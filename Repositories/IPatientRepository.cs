using System;
using System.Threading.Tasks;
using EClinic.Models.Domain;

namespace EClinic.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByUserIdAsync(string userId);
    }
}
