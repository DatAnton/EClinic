using System;
using EClinic.Models.ViewModels;
using EClinic.Models.Domain;
using EClinic.Repositories;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Threading.Tasks;

namespace EClinic.Managers
{
    public class MedicalCardsManager
    {
        private readonly IMedicalCardRepository _MedicalCardRepository;
        private readonly IMapper _Mapper;

        public MedicalCardsManager(IMapper mapper, IMedicalCardRepository medicalCardRepository)
        {
            _MedicalCardRepository = medicalCardRepository;
            _Mapper = mapper;
        }

        public async Task SaveMedicalCard(CreateMedicalCardViewModel model)
        {
            var domainMedicalCard = _Mapper.Map<MedicalCard>(model);
            domainMedicalCard.UpdatedAt = DateTime.Now;
            if(domainMedicalCard.Id == 0)
            {
                domainMedicalCard.CreatedAt = DateTime.Now;
            }
            await _MedicalCardRepository.SaveMedicalCardAsync(domainMedicalCard);
        }


    }
}
