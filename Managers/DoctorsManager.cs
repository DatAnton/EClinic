using System;
using System.Threading.Tasks;
using System.Linq;
using EClinic.Repositories;
using EClinic.Models.Domain;
using System.Collections.Generic;

namespace EClinic.Managers
{
    public class DoctorsManager
    {
        private readonly IMeetingRepository _MeetingRepository;
        private readonly IDoctorRepository _DoctorRepository;

        public DoctorsManager(IMeetingRepository meetingRepository, IDoctorRepository doctorRepository)
        {
            _MeetingRepository = meetingRepository;
            _DoctorRepository = doctorRepository;
        }

        public async Task<Doctor> GetFreeDoctorOnDateTime(DateTime dateTime, int doctorTypeId)
        {
            List<Meetting> dayMeettings = await _MeetingRepository.GetMeettingsOnDayForDoctorTypeAsync(dateTime, doctorTypeId);

            List<Doctor> allDoctors = dayMeettings.Select(m => m.Doctor).ToList();

            List<Doctor> uniqueDoctors = new List<Doctor>();

            foreach(var doctor in allDoctors)
            {
                if(uniqueDoctors.FirstOrDefault(d => d.Id == doctor.Id) == null)
                {
                    uniqueDoctors.Add(doctor);
                }
            }
            if(uniqueDoctors.Count() == 0)
            {
                var doctors = await _DoctorRepository.GetDoctorsByTypesAsync(doctorTypeId);
                return doctors.Count() == 0 ? null : doctors[0];
            }
            else
            {
                Doctor minMeetingsDoctor = uniqueDoctors[0];
                int minCountMeetingsDoctor = dayMeettings.Where(m => m.DoctorId == uniqueDoctors[0].Id).Count();
                for (int i = 1; i < uniqueDoctors.Count; i++)
                {
                    int currentDoctorMeeting = dayMeettings.Where(m => m.DoctorId == uniqueDoctors[i].Id).Count();
                    if(currentDoctorMeeting < minCountMeetingsDoctor)
                    {
                        minCountMeetingsDoctor = currentDoctorMeeting;
                        minMeetingsDoctor = uniqueDoctors[i];
                    }
                }

                return minMeetingsDoctor;
            }
        }

        public async Task<List<DateTime>> GetFreeTimeMeetingOnDay(DateTime dateTime, int doctorTypeId)
        {
            List<DateTime> result = new List<DateTime>();
            List<Meetting> dayMeettings = await _MeetingRepository.GetMeettingsOnDayForDoctorTypeAsync(dateTime, doctorTypeId);

            int numberOfDoctorsOfTypes = (await _DoctorRepository.GetDoctorsByTypesAsync(doctorTypeId)).Count();
            if(numberOfDoctorsOfTypes == 0)
            {
                return result;
            }

            DateTime endDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 18, 0, 0);

            for (DateTime iterator = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 9, 0, 0); iterator < endDate; iterator = iterator.AddHours(1))
            {
                var timeMeeting = dayMeettings.Where(m => m.MeettingDate == iterator);
                if(timeMeeting.Count() < numberOfDoctorsOfTypes)
                {
                    result.Add(iterator);
                }    
            }

            return result;
        }
    }
}
