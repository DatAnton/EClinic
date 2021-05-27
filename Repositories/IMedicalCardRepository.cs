using System;
using System.Threading.Tasks;
using EClinic.Models.Domain;

namespace EClinic.Repositories
{
    public interface IMedicalCardRepository
    {
        Task SaveMedicalCardAsync(MedicalCard medicalCard);
        Task<MedicalCard> GetMedicalCardAsync(int doctorId, int patientId);
    }
}
