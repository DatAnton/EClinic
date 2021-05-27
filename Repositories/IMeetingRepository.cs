using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Models.Domain;

namespace EClinic.Repositories
{
    public interface IMeetingRepository
    {
        Task CreateMeetingAsync(Meetting meetting);
        Task<Meetting> GetMeettingAsync(int Id);
        Task<List<Meetting>> GetMeettingsOnDayAsync(DateTime dateTime);
        Task<List<Meetting>> GetMeettingsOnDayForDoctorTypeAsync(DateTime dateTime, int doctorTypeId);
        Task<List<Meetting>> GetMeettingsOnDayTimeForDoctorTypeAsync(DateTime dateTime, int doctorTypeId);
        Task<List<Meetting>> GetMeettingsOnWeekForAsync(int personId, bool isDoctor = false);
        Task<List<Meetting>> GetMeettingsAsync(int? doctorTypeId, string doctorName, DateTime? date);
        Task UpdateMeetingAsync(Meetting meetting);
    }
}