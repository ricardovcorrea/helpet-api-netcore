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
    public class BreedDomain : IBreedDomain
    {
        private readonly IMongoCollection<BreedModel> _collection;

        public BreedDomain(IHelpetDBSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<BreedModel>(dbSettings.BreadCollectionName);
        }

        public DTOResponse<DTOBreed> Create(DTOBreed createInfo)
        {
            var model = createInfo.ToModel();

            _collection.InsertOne(model);

            return new DTOResponse<DTOBreed>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOBreed>> GetAll()
        {
            var foundModelList = _collection.Find<BreedModel>(model => model.Id != null).ToList();

            var foundDTOList = new List<DTOBreed>();

            foreach (var model in foundModelList)
            {
                var dto = model.ToDTO();

                foundDTOList.Add(dto);
            }

            return new DTOResponse<List<DTOBreed>>()
            {
                Code = 200,
                Data = foundDTOList,
                Total = foundDTOList.Count
            };
        }

        public DTOResponse<DTOBreed> GetById(string id)
        {
            var foundModel = _collection.Find<BreedModel>(model => model.Id == id).FirstOrDefault();

            if (foundModel == null)
            {
                return new DTOResponse<DTOBreed>()
                {
                    Code = 400,
                    Message = "No breed was found with this ID"
                };
            }

            return new DTOResponse<DTOBreed>()
            {
                Code = 200,
                Data = foundModel.ToDTO()
            };
        }

        public DTOResponse<DTOBreed> Update(DTOBreed updateInfo)
        {
            var model = updateInfo.ToModel();

            _collection.UpdateOne<BreedModel>(breed => breed.Id == updateInfo.Id, Builders<BreedModel>.Update.Set("Name", updateInfo.Name));

            return new DTOResponse<DTOBreed>()
            {
                Code = 200
            };
        }

        public DTOResponse<bool> DeleteById(string id)
        {
            var foundModel = _collection.DeleteOne<BreedModel>(model => model.Id == id);

            if (foundModel == null)
            {
                return new DTOResponse<bool>()
                {
                    Code = 400,
                    Message = "No Breed was found with this ID"
                };
            }

            return new DTOResponse<bool>()
            {
                Code = 200,
                Data = true
            };
        }

        #region Publics

        #endregion
    }
}
