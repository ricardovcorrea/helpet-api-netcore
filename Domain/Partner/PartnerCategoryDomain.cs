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
    public class PartnerCategoryDomain : IPartnerCategoryDomain
    {
        private readonly string _entityName;
        private readonly IMongoCollection<PartnerCategoryModel> _collection;

        public PartnerCategoryDomain(IHelpetDBSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<PartnerCategoryModel>(dbSettings.PartnersCategoryCollectionName);
            _entityName = dbSettings.PartnersCategoryCollectionName;
        }

        public DTOResponse<DTOPartnerCategory> Create(DTOPartnerCategory createInfo)
        {
            var model = createInfo.ToModel();

            _collection.InsertOne(model);

            return new DTOResponse<DTOPartnerCategory>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOPartnerCategory>> GetAll()
        {
            var foundModelList = _collection.Find<PartnerCategoryModel>(model => model.Id != null).ToList();

            var foundDTOList = new List<DTOPartnerCategory>();

            foreach (var model in foundModelList)
            {
                var dto = model.ToDTO();

                foundDTOList.Add(dto);
            }

            return new DTOResponse<List<DTOPartnerCategory>>()
            {
                Code = 200,
                Data = foundDTOList,
                Total = foundDTOList.Count
            };
        }

        public DTOResponse<DTOPartnerCategory> GetById(string id)
        {
            var foundModel = _collection.Find<PartnerCategoryModel>(model => model.Id == id).FirstOrDefault();

            if (foundModel == null)
            {
                return new DTOResponse<DTOPartnerCategory>()
                {
                    Code = 400,
                    Message = $"No {_entityName} was found with this ID"
                };
            }

            return new DTOResponse<DTOPartnerCategory>()
            {
                Code = 200,
                Data = foundModel.ToDTO()
            };
        }

        public DTOResponse<DTOPartnerCategory> Update(DTOPartnerCategory updateInfo)
        {
            var model = updateInfo.ToModel();

            _collection.UpdateOne<PartnerCategoryModel>(model => model.Id == updateInfo.Id, Builders<PartnerCategoryModel>.Update.Set("Name", updateInfo.Name).Set("ParentId", updateInfo.ParentId));

            return new DTOResponse<DTOPartnerCategory>()
            {
                Code = 200
            };
        }

        public DTOResponse<bool> DeleteById(string id)
        {
            var foundModel = _collection.DeleteOne<PartnerCategoryModel>(model => model.Id == id);

            if (foundModel == null)
            {
                return new DTOResponse<bool>()
                {
                    Code = 400,
                    Message = $"No {_entityName} was found with this ID"
                };
            }

            return new DTOResponse<bool>()
            {
                Code = 200,
                Data = true
            };
        }

        public DTOResponse<List<DTOPartnerCategory>> GetByIdList(List<string> idsList)
        {
            var results = new List<DTOPartnerCategory>();

            foreach(var categoryId in idsList)
            {
                var getCategoryByIdResult = this.GetById(categoryId);
                if(getCategoryByIdResult.Code == 200)
                {
                    results.Add(getCategoryByIdResult.Data);
                }
            }

            return new DTOResponse<List<DTOPartnerCategory>>()
            {
                Code = 200,
                Data = results
            };
        }

        #region Publics

        #endregion
    }
}
