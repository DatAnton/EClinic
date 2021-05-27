using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EClinic.Models.Domain;
using EClinic.Repositories;
using EClinic.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EClinic.Utils;

namespace EClinic.Realizations
{
    public class MeettingRepository : IMeetingRepository
    {
        private readonly ClinicContext _ClinicContext;
        
        public MeettingRepository(ClinicContext clinicContext)
        {
            _ClinicContext = clinicContext;
        }

        public async Task CreateMeetingAsync(Meetting meetting)
        {
            await _ClinicContext.Meettings.AddAsync(meetting);
            await _ClinicContext.SaveChangesAsync();
        }

        public async Task UpdateMeetingAsync(Meetting meetting)
        {
            _ClinicContext.Meettings.Update(meetting);
            await _ClinicContext.SaveChangesAsync();
        }

        public async Task<Meetting> GetMeettingAsync(int Id)
        {
            return await _ClinicContext.Meettings.FindAsync(Id);
        }

        public Task<List<Meetting>> GetMeettingsAsync(int? doctorTypeId, string doctorName, DateTime? date)
        {
            if(!doctorTypeId.HasValue && string.IsNullOrEmpty(doctorName) && !date.HasValue)
            {
                return Task.FromResult(new List<Meetting>());
            }
            IQueryable<Meetting> result = _ClinicContext.Meettings
                .Include(e => e.Doctor.User).Include(e => e.Patient.User)
                .Include(e => e.MeettingStatus).Include(e => e.Doctor.DoctorType);

            if(doctorTypeId.HasValue)
            {
                result = result.Where(e => e.Doctor.DoctorTypeId == doctorTypeId);
            }
            if(!string.IsNullOrEmpty(doctorName))
            {
                result = result.Where(e => e.Doctor.User.FullName.Contains(doctorName));
            }
            if (date.HasValue)
            {
                result = result.Where(e => e.MeettingDate.Date == date.Value.Date);

                if(date.Value.Hour != 0)
                {
                    result = result.Where(e => e.MeettingDate == date.Value);
                }
            }
            return result.ToListAsync();
        }

        public async Task<List<Meetting>> GetMeettingsOnDayAsync(DateTime dateTime)
        {
            return await _ClinicContext.Meettings.Where(m => m.MeettingDate.Date == dateTime.Date && m.MeettingStatusId != Constants.MeetingCancelStatusId).ToListAsync();
        }

        public async Task<List<Meetting>> GetMeettingsOnDayForDoctorTypeAsync(DateTime dateTime, int doctorTypeId)
        {
            return await _ClinicContext.Meettings.Where(m => m.MeettingDate.Date == dateTime.Date && m.Doctor.DoctorTypeId == doctorTypeId && m.MeettingStatusId != Constants.MeetingCancelStatusId).Include(p => p.Doctor).ToListAsync();
        }

        public async Task<List<Meetting>> GetMeettingsOnDayTimeForDoctorTypeAsync(DateTime dateTime, int doctorTypeId)
        {
            return await _ClinicContext.Meettings.Where(m => m.MeettingDate == dateTime && m.Doctor.DoctorTypeId == doctorTypeId && m.MeettingStatusId != Constants.MeetingCancelStatusId).Include(p => p.Doctor).ToListAsync();
        }

        public async Task<List<Meetting>> GetMeettingsOnWeekForAsync(int personId, bool isDoctor = false)
        {
            List<Meetting> result = new List<Meetting>();
            DateTime indexDay = DateTime.Today.Date;
            for (int intIndexDay = 0; intIndexDay < 7; intIndexDay++)
            {
                var tempRes = _ClinicContext.Meettings
                    .Where(m => m.MeettingDate.Date == indexDay.AddDays(intIndexDay).Date
                        && m.MeettingStatusId != Constants.MeetingCancelStatusId)
                    .OrderBy(q => q.MeettingDate).Include(a => a.Patient.User);
                if(isDoctor)
                {
                    result.AddRange(await tempRes.Where(e => e.DoctorId == personId).ToListAsync());
                }
                else
                {
                    result.AddRange(await tempRes.Where(e => e.PatientId == personId)
                        .Include(p => p.Doctor.User).Include(p => p.Doctor.DoctorType).ToListAsync());
                }
            }
            return result;
        }
    }
}
