using System;
using System.Collections.Generic;
using System.Linq;
using Api.Configurations;
using Api.Domain.Interfaces;
using Api.DTO;
using Api.Extensions;
using Api.Infrastructure;
using Api.Models;
using MongoDB.Driver;

namespace Api.Domain
{
    public class PetDomain : IPetDomain
    {
        private readonly IMongoCollection<PetModel> _petCollection;

        private readonly IBreedDomain _breedDomain;
        private readonly IFurColorDomain _furColorDomain;
        private readonly ICoatDomain _coatDomain;


        public PetDomain(IHelpetDBSettings dbSettings, IBreedDomain breedDomain, IFurColorDomain furColorDomain, ICoatDomain coatDomain)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

			_petCollection = database.GetCollection<PetModel>(dbSettings.PetsCollectionName);

            _breedDomain = breedDomain;
            _furColorDomain = furColorDomain;
            _coatDomain = coatDomain;
        }

        public DTOResponse<DTOPet> Create(DTOPet createPetInfo)
        {
            if (string.IsNullOrWhiteSpace(createPetInfo.UserId))
            {
                return new DTOResponse<DTOPet>()
                {
                    Code = 400,
                    Message = "User Id is required!"
                };
            }

            var petModel = createPetInfo.ToModel();

            _petCollection.InsertOne(petModel);

            return new DTOResponse<DTOPet>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOPet>> GetPetsByUserId(string userId)
        {
            var foundPetsModelList = _petCollection.Find<PetModel>(pet => pet.UserId == userId).ToList();

            var foundPetsDTOList = new List<DTOPet>();

            foreach (var petModel in foundPetsModelList)
            {
                var dtoPet = petModel.ToDTO();

                LoadPetInfo(dtoPet);

                foundPetsDTOList.Add(dtoPet);
            }

            return new DTOResponse<List<DTOPet>>()
            {
                Code = 200,
                Data = foundPetsDTOList
            };
        }

        public DTOResponse<List<DTOPet>> GetAllPets()
        {
            var foundPetsModelList = _petCollection.Find<PetModel>(pet => pet.Id != null).ToList();

            var foundPetsDTOList = new List<DTOPet>();

            foreach (var petModel in foundPetsModelList)
            {
                var dtoPet = petModel.ToDTO();

                LoadPetInfo(dtoPet);

                foundPetsDTOList.Add(dtoPet);
            }

            return new DTOResponse<List<DTOPet>>()
            {
                Code = 200,
                Data = foundPetsDTOList,
                Total = foundPetsDTOList.Count
            };
        }

        #region Publics

        #endregion


        #region Privates
        private void LoadPetInfo(DTOPet dtoPet)
        {
            if(dtoPet.Breed != null && !string.IsNullOrWhiteSpace(dtoPet.Breed.Id))
            {
                var getBreedByIdResult = _breedDomain.GetById(dtoPet.Breed.Id);
                if(getBreedByIdResult.Code == 200)
                {
                    dtoPet.Breed = getBreedByIdResult.Data;
                }
            }

            if (dtoPet.FurColor != null && !string.IsNullOrWhiteSpace(dtoPet.FurColor.Id))
            {
                var getFurColorByIdResult = _furColorDomain.GetById(dtoPet.FurColor.Id);
                if (getFurColorByIdResult.Code == 200)
                {
                    dtoPet.FurColor = getFurColorByIdResult.Data;
                }
            }

            if (dtoPet.Coat != null && !string.IsNullOrWhiteSpace(dtoPet.Coat.Id))
            {
                var getCoatByIdResult = _coatDomain.GetById(dtoPet.Coat.Id);
                if (getCoatByIdResult.Code == 200)
                {
                    dtoPet.Coat = getCoatByIdResult.Data;
                }
            }


        }
        #endregion
    }
}
