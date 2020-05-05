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
    public class FurColorDomain : IFurColorDomain
    {
        private readonly IMongoCollection<FurColorModel> _collection;

        public FurColorDomain(IHelpetDBSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<FurColorModel>(dbSettings.FurColorCollectionName);
        }

        public DTOResponse<DTOFurColor> Create(DTOFurColor createInfo)
        {
            var model = createInfo.ToModel();

            _collection.InsertOne(model);

            return new DTOResponse<DTOFurColor>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOFurColor>> GetAll()
        {
            var foundModelList = _collection.Find<FurColorModel>(model => model.Id != null).ToList();

            var foundDTOList = new List<DTOFurColor>();

            foreach (var model in foundModelList)
            {
                var dto = model.ToDTO();

                foundDTOList.Add(dto);
            }

            return new DTOResponse<List<DTOFurColor>>()
            {
                Code = 200,
                Data = foundDTOList,
                Total = foundDTOList.Count
            };
        }

        public DTOResponse<DTOFurColor> GetById(string id)
        {
            var foundModel = _collection.Find<FurColorModel>(model => model.Id == id).FirstOrDefault();

            if (foundModel == null)
            {
                return new DTOResponse<DTOFurColor>()
                {
                    Code = 400,
                    Message = "No Fur Color was found with this ID"
                };
            }

            return new DTOResponse<DTOFurColor>()
            {
                Code = 200,
                Data = foundModel.ToDTO()
            };
        }

        #region Publics

        #endregion
    }
}
