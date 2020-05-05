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
    public class CoatDomain : ICoatDomain
    {
        private readonly IMongoCollection<CoatModel> _collection;

        public CoatDomain(IHelpetDBSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<CoatModel>(dbSettings.CoatCollectionName);
        }

        public DTOResponse<DTOCoat> Create(DTOCoat createInfo)
        {
            var model = createInfo.ToModel();

            _collection.InsertOne(model);

            return new DTOResponse<DTOCoat>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOCoat>> GetAll()
        {
            var foundModelList = _collection.Find<CoatModel>(model => model.Id != null).ToList();

            var foundDTOList = new List<DTOCoat>();

            foreach (var model in foundModelList)
            {
                var dto = model.ToDTO();

                foundDTOList.Add(dto);
            }

            return new DTOResponse<List<DTOCoat>>()
            {
                Code = 200,
                Data = foundDTOList,
                Total = foundDTOList.Count
            };
        }

        public DTOResponse<DTOCoat> GetById(string id)
        {
            var foundModel = _collection.Find<CoatModel>(model => model.Id == id).FirstOrDefault();

            if (foundModel == null)
            {
                return new DTOResponse<DTOCoat>()
                {
                    Code = 400,
                    Message = "No Coat was found with this ID"
                };
            }

            return new DTOResponse<DTOCoat>()
            {
                Code = 200,
                Data = foundModel.ToDTO()
            };
        }

        public DTOResponse<DTOCoat> Update(DTOCoat updateInfo)
        {
            var model = updateInfo.ToModel();

             _collection.UpdateOne<CoatModel>(coat => coat.Id == updateInfo.Id, Builders<CoatModel>.Update.Set("Name", updateInfo.Name));

            return new DTOResponse<DTOCoat>()
            {
                Code = 200
            };
        }

        public DTOResponse<bool> DeleteById(string id)
        {
            var foundModel = _collection.DeleteOne<CoatModel>(model => model.Id == id);

            if (foundModel == null)
            {
                return new DTOResponse<bool>()
                {
                    Code = 400,
                    Message = "No Coat was found with this ID"
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
