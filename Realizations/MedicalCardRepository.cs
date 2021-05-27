using System;
using EClinic.Models.Domain;
using EClinic.Repositories;
using EClinic.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EClinic.Realizations
{
    public class MedicalCardRepository : IMedicalCardRepository
    {
        private readonly ClinicContext _ClinicContext;

        public MedicalCardRepository(ClinicContext clinicContext)
        {
            _ClinicContext = clinicContext;
        }

        public async Task SaveMedicalCardAsync(MedicalCard medicalCard)
        {
            _ClinicContext.MedicalCards.Add(medicalCard);
            if(medicalCard.Id != 0)
            {
                var existed = await _ClinicContext.MedicalCards.FindAsync(medicalCard.Id);
                _ClinicContext.Entry(medicalCard).State = EntityState.Modified;
            }
            await _ClinicContext.SaveChangesAsync();
        }

        public async Task<MedicalCard> GetMedicalCardAsync(int doctorId, int patientId)
        {
            return await _ClinicContext.MedicalCards.FirstOrDefaultAsync(mc => mc.DoctorId == doctorId && mc.PatientId == patientId);
        }
    }
}
