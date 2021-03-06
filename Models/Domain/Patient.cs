using System;
using System.Collections.Generic;

namespace EClinic.Models.Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public List<MedicalCard> MedicalCards { get; set; }
        public List<Meetting> Meettings { get; set; }
    }
}
