using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Api.Configurations;
using Api.Domain.Interfaces;
using Api.DTO;
using Api.enumerators;
using Api.Extensions;
using Api.Infrastructure;
using Api.Models;
using GoogleApi.Entities.Common.Enums;
using MongoDB.Driver;

namespace Api.Domain
{
    public class PartnerDomain : IPartnerDomain
    {
        private readonly string _entityName;
        private readonly IMongoCollection<PartnerModel> _collection;
        
        private readonly IGooglePlaces _googlePlaces;
        private readonly IPartnerCategoryDomain _partnerCategoryDomain;
        private readonly IFilesDomain _filesDomain;

        public PartnerDomain(IHelpetDBSettings dbSettings, IGooglePlaces googlePlaces, IPartnerCategoryDomain partnerCategoryDomain, IFilesDomain filesDomain)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<PartnerModel>(dbSettings.PartnersCollectionName);
            _entityName = dbSettings.PartnersCollectionName;

            _googlePlaces = googlePlaces;

            _partnerCategoryDomain = partnerCategoryDomain;
            _filesDomain = filesDomain;
        }

        #region Publics

        public DTOResponse<DTOPartner> Create(DTOPartner createInfo)
        {
            var model = createInfo.ToModel();

            _collection.InsertOne(model);

            return new DTOResponse<DTOPartner>()
            {
                Code = 200
            };
        }

        public DTOResponse<List<DTOPartner>> GetAll(DTOPartnerSearch searchQuery)
        {
            var filter = Builders<PartnerModel>.Filter.Ne(x => x.Id, null);

            switch (searchQuery.Column)
            {
                case PartnerSearchColumn.Name:
                    {
                        filter = Builders<PartnerModel>.Filter.Where(model => model.Name.ToLower().Contains(searchQuery.Term.ToLower()));
                        break;
                    }
                case PartnerSearchColumn.City:
                    {
                        filter = Builders<PartnerModel>.Filter.Where(model => model.City.ToLower().Contains(searchQuery.Term.ToLower()));
                        break;
                    }
                case PartnerSearchColumn.Country:
                    {
                        filter = Builders<PartnerModel>.Filter.Where(model => model.Country.ToLower().Contains(searchQuery.Term.ToLower()));
                        break;
                    }
                case PartnerSearchColumn.Email:
                    {
                        filter = Builders<PartnerModel>.Filter.Where(model => model.Email1.ToLower().Contains(searchQuery.Term.ToLower()) || model.Email2.ToLower().Contains(searchQuery.Term.ToLower()));
                        break;
                    }
                case PartnerSearchColumn.Tel:
                    {
                        filter = Builders<PartnerModel>.Filter.Where(model => model.Tel1.ToLower().Contains(searchQuery.Term.ToLower()) || model.Tel2.ToLower().Contains(searchQuery.Term.ToLower()));
                        break;
                    }
                case PartnerSearchColumn.HasNotes:
                    {
                        if(searchQuery.Term == "1")
                        {
                            filter = Builders<PartnerModel>.Filter.Where(model => model.Notes != null && model.Notes != "");
                        }
                        else
                        {
                            filter = Builders<PartnerModel>.Filter.Where(model => model.Notes == null || model.Notes == "");
                        }
                        break;
                    }
                case PartnerSearchColumn.ShowInApp:
                    {
                        var showInApp = searchQuery.Term == "1";
                        filter = Builders<PartnerModel>.Filter.Where(model => model.ShowInApp == showInApp);
                        break;
                    }
                case PartnerSearchColumn.SubscriptionLevel:
                    {
                        var subscriptionLevel = int.Parse(searchQuery.Term);
                        filter = Builders<PartnerModel>.Filter.Where(model => model.SubscriptionLevel == subscriptionLevel);
                        break;
                    }
                case PartnerSearchColumn.InfoStatus:
                    {
                        var infoStatus = int.Parse(searchQuery.Term);
                        filter = Builders<PartnerModel>.Filter.Where(model => model.InfoStatus == infoStatus);
                        break;
                    }
            }

            var foundModelList = _collection.Find<PartnerModel>(filter).Skip(searchQuery.Offset).Limit(searchQuery.Size).SortBy(model => model.Name).ToList();
            var totalDocuments = _collection.Find<PartnerModel>(filter).CountDocuments();

            var foundDTOList = new List<DTOPartner>();

            foreach (var model in foundModelList)
            {
                var dto = model.ToDTO();

                this.LoadPartnerData(dto, model);

                foundDTOList.Add(dto);
            }

            return new DTOResponse<List<DTOPartner>>()
            {
                Code = 200,
                Data = foundDTOList,
                Total = totalDocuments
            };
        }

        public DTOResponse<DTOPartner> GetById(string id)
        {
            var foundModel = _collection.Find<PartnerModel>(model => model.Id == id).FirstOrDefault();

            if (foundModel == null)
            {
                return new DTOResponse<DTOPartner>()
                {
                    Code = 400,
                    Message = $"No {_entityName} was found with this ID"
                };
            }

            var foundDTO = foundModel.ToDTO();

            this.LoadPartnerData(foundDTO, foundModel);

            return new DTOResponse<DTOPartner>()
            {
                Code = 200,
                Data = foundDTO
            };
        }

        public DTOResponse<DTOPartner> Update(DTOPartner updateInfo)
        {
            var model = updateInfo.ToModel();

            _collection.UpdateOne<PartnerModel>(model => model.Id == updateInfo.Id, Builders<PartnerModel>
                .Update
                .Set("Name", model.Name)
                .Set("Categories", model.Categories)
                .Set("Street", model.Street)
                .Set("Number", model.Number)
                .Set("PostalCode", model.PostalCode)
                .Set("City", model.City)
                .Set("Country", model.Country)
                .Set("State", model.State)
                .Set("Latitude", model.Latitude)
                .Set("Longitude", model.Longitude)
                .Set("Email1", model.Email1)
                .Set("Email2", model.Email2)
                .Set("Tel1", model.Tel1)
                .Set("Tel2", model.Tel2)
                .Set("Cel1", model.Cel1)
                .Set("Cel2", model.Cel2)
                .Set("Url", model.Url)
                .Set("FacebookUrl", model.FacebookUrl)
                .Set("InstagramUrl", model.InstagramUrl)
                .Set("IsChecked", model.IsChecked)
                .Set("IsAuthorized", model.IsAuthorized)
                .Set("ShowInApp", model.ShowInApp)
                .Set("SubscriptionLevel", model.SubscriptionLevel)
                .Set("Accountable", model.Accountable)
                .Set("Description", model.Description)
                .Set("Notes", model.Notes)
                .Set("InfoStatus", model.InfoStatus)
                .Set("BusinessName", model.BusinessName)
                .Set("BusinessStreet", model.BusinessStreet)
                .Set("BusinessNumber", model.BusinessNumber)
                .Set("BusinessPostalCode", model.BusinessPostalCode)
                .Set("BusinessCity", model.BusinessCity)
                .Set("BusinessCountry", model.BusinessCountry)
                .Set("BusinessState", model.BusinessState)
                .Set("BusinessPec", model.BusinessPec)
                .Set("BusinessTaxCode", model.BusinessTaxCode)
                .Set("BusinessVatNumber", model.BusinessVatNumber)
                .Set("BusinessTel", model.BusinessTel)
                .Set("BusinessCel1", model.BusinessCel1)
                .Set("BusinessCel2", model.BusinessCel2)
                .Set("BusinessPec", model.BusinessPec)
                .Set("BusinessUniqueCode", model.BusinessUniqueCode)
                .Set("BusinessResponsibleName", model.BusinessResponsibleName)
                .Set("BusinessResponsibleSurname", model.BusinessResponsibleSurname)
                .Set("BusinessResponsibleTel", model.BusinessResponsibleTel)
                .Set("BusinessResponsibleCel", model.BusinessResponsibleCel)
                .Set("ImageId", model.ImageId)
                .Set("ContractId", model.ContractId)
                .Set("ConsensoId", model.ConsensoId)
                .Set("PrivacyId", model.PrivacyId)
                .Set("PagamentoId", model.PagamentoId)
                .Set("Openings", model.Openings)
            );

            return new DTOResponse<DTOPartner>()
            {
                Code = 200
            };
        }

        public DTOResponse<bool> DeleteById(string id)
        {
            var foundModel = _collection.DeleteOne<PartnerModel>(model => model.Id == id);

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

        public DTOResponse<List<DTOPlace>> SearchPlaces(DTOPlaceSearch searchQuery)
        {
            var result = new List<DTOPlace>();

            var countryInformation = new Dictionary<string, Dictionary<string, List<string>>>() {
                {
                    "brazil",
                     new Dictionary<string, List<string>>()
                     {
                        { "são paulo", new List<string>() { "36", "-23.5475", "-46.63611", "br" } },
                        { "rio de janeiro", new List<string>() { "36", "-22.90278", "-43.2075", "br" } }
                     }
                },
                {
                    "italy",
                     new Dictionary<string, List<string>>()
                     {
                        { "rome", new List<string>() { "23", "41.89193", "12.51133", "it" } }
                     }
                }
            };

            var cityInfo = countryInformation[searchQuery.CountryName.ToLower()][searchQuery.CityName.ToLower()];

            var placeSearchResult = _googlePlaces.TextSearch(double.Parse(cityInfo[1]), double.Parse(cityInfo[2]), int.Parse(cityInfo[0]), searchQuery.SearchTerms, 50000, searchQuery.PageToken);

            if (placeSearchResult.Code != 200)
            {
                return new DTOResponse<List<DTOPlace>>()
                {
                    Code = 400,
                    Message = placeSearchResult.Message
                };
            }

            foreach (var searchResult in placeSearchResult.Data.Results)
            {
                result.Add(new DTOPlace()
                {
                    Name = searchResult.Name,
                    FormatedAddress = searchResult.FormattedAddress,
                    PlaceId = searchResult.PlaceId
                });
            }

            return new DTOResponse<List<DTOPlace>>()
            {
                Code = 200,
                Data = result,
                NextPageToken = placeSearchResult.Data.NextPageToken
            };
        }

        public DTOResponse<DTOPlace> GetPlaceDetails(string placeId)
        {
            DTOPlace result;

            var googlePlaceSearchResult = _googlePlaces.GetPlaceDetail(placeId);
            if (googlePlaceSearchResult.Code != 200)
            {
                return new DTOResponse<DTOPlace>()
                {
                    Code = 400,
                    Message = googlePlaceSearchResult.Message
                };
            }

            result = new DTOPlace()
            {
                PlaceId = placeId,
                Name = googlePlaceSearchResult.Data.Result.Name,
                FormatedAddress = googlePlaceSearchResult.Data.Result.FormattedAddress,
                PhoneNumber = googlePlaceSearchResult.Data.Result.FormattedPhoneNumber,
                Website = googlePlaceSearchResult.Data.Result.Website,
                Address = new DTOAddress()
                {
                    Street = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Street_Address) || aC.Types.Contains(AddressComponentType.Route))?.FirstOrDefault().LongName ?? "",
                    City = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Administrative_Area_Level_2))?.FirstOrDefault().ShortName ?? "",
                    Country = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Country))?.FirstOrDefault().LongName ?? "",
                    Number = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Street_Number))?.FirstOrDefault().LongName ?? "",
                    PostalCode = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Postal_Code))?.FirstOrDefault().LongName ?? "",
                    State = googlePlaceSearchResult.Data.Result.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Administrative_Area_Level_1))?.FirstOrDefault().ShortName ?? "",
                    Latitude = googlePlaceSearchResult.Data.Result.Geometry?.Location?.Latitude.ToString() ?? "",
                    Longitude = googlePlaceSearchResult.Data.Result.Geometry?.Location?.Longitude.ToString() ?? "",
                }
            };

            var foundModel = _collection.Find<PartnerModel>(model => model.PostalCode == result.Address.PostalCode && model.Number == result.Address.Number).FirstOrDefault();
            if(foundModel != null)
            {
                return new DTOResponse<DTOPlace>()
                {
                    Code = 400,
                    Message = "Partner already registered!"
                };
            }

            return new DTOResponse<DTOPlace>()
            {
                Code = 200,
                Data = result
            };
        }

        public DTOResponse<DTOAddress> GeocodeAddress(DTOAddress address)
        {
            DTOAddress result;

            var geocodeAddressResult = _googlePlaces.GeocodeAddress(address.Country, address.State, address.City, address.Street, address.Number, address.PostalCode);
            if (geocodeAddressResult.Code != 200)
            {
                return new DTOResponse<DTOAddress>()
                {
                    Code = 400,
                    Message = geocodeAddressResult.Message
                };
            }

            result = new DTOAddress()
            {
                Street = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Street_Address) || aC.Types.Contains(AddressComponentType.Route))?.FirstOrDefault()?.LongName ?? "",
                City = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Administrative_Area_Level_2))?.FirstOrDefault()?.ShortName ?? "",
                Country = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Country))?.FirstOrDefault()?.LongName ?? "",
                Number = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Street_Number))?.FirstOrDefault()?.LongName ?? "",
                PostalCode = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Postal_Code))?.FirstOrDefault()?.LongName ?? "",
                State = geocodeAddressResult.Data.Results.FirstOrDefault()?.AddressComponents?.Where(aC => aC.Types.Contains(AddressComponentType.Administrative_Area_Level_1))?.FirstOrDefault()?.ShortName ?? "",
                Latitude = geocodeAddressResult.Data.Results.FirstOrDefault()?.Geometry?.Location?.Latitude.ToString() ?? "",
                Longitude = geocodeAddressResult.Data.Results.FirstOrDefault()?.Geometry?.Location?.Longitude.ToString() ?? "",
            };

            return new DTOResponse<DTOAddress>()
            {
                Code = 200,
                Data = result
            };
        }

        public DTOResponse<string> Export()
        {
            var csvStringBuilder = new StringBuilder();
            csvStringBuilder.AppendLine("Name,Categories,Description,Street,Number,PostalCode,City,Country,Provincia,Latitude,Longitude,Email1,Email2,Tel1,Tel2,Cel1,Cel2,Url,Facebook,Instagram,IsChecked,IsAuthorized,ShowInApp,Subscription,InfoStatus,Notes,BusinessName,BusinessAddressStreet,BusinessAddressNumber,BusinessAddressPostalCode,BusinessAddressCity,BusinessAddressCountry,BusinessAddressState,BusinessTaxCode,BusinessVatNumber,BusinessTal,BusinessCel1,BusinessCel2,BusinessPec,BusinessUniqueCode,BusinessResponsibleName,BusinessResponsibleSurname,BusinessResponsibleTel,BusinessResponsibleCel");

            var totalDocuments = _collection.Find<PartnerModel>(model => model.Id != null).CountDocuments();
            var foundPartners = GetAll(new DTOPartnerSearch()
            {
                Offset = 0,
                Size = Convert.ToInt32(totalDocuments)
            });

            foreach(var partner in foundPartners.Data)
            {
                var registerLine = $"{partner.Name},";
                
                registerLine += $"{String.Join(';', partner.Categories.Select(category => category.Name).ToArray())},";
                registerLine += $"{partner.Description},";
                registerLine += $"{partner.Address.Street},";
                registerLine += $"{partner.Address.Number},";
                registerLine += $"{partner.Address.PostalCode},";
                registerLine += $"{partner.Address.City},";
                registerLine += $"{partner.Address.Country},";
                registerLine += $"{partner.Address.State},";
                registerLine += $"{partner.Address.Latitude},";
                registerLine += $"{partner.Address.Longitude},";
                registerLine += $"{partner.Email1},";
                registerLine += $"{partner.Email2},";
                registerLine += $"{partner.Tel1},";
                registerLine += $"{partner.Tel2},";
                registerLine += $"{partner.Cel1},";
                registerLine += $"{partner.Cel2},";
                registerLine += $"{partner.Url},";
                registerLine += $"{partner.FacebookUrl},";
                registerLine += $"{partner.InstagramUrl},";
                registerLine += $"{partner.IsChecked.ToString()},";
                registerLine += $"{partner.IsAuthorized.ToString()},";
                registerLine += $"{partner.ShowInApp.ToString()},";
                registerLine += $"{partner.SubscriptionLevel.ToString()},";
                registerLine += $"{partner.InfoStatus.ToString()},";
                registerLine += $"{partner.Notes},";
                registerLine += $"{partner.CompanyData?.BusinessName ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.Street ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.Number ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.PostalCode ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.City ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.Country ?? ""},";
                registerLine += $"{partner.CompanyData?.Address?.State ?? ""},";
                registerLine += $"{partner.CompanyData?.TaxCode ?? ""},";
                registerLine += $"{partner.CompanyData?.VatNumber ?? ""},";
                registerLine += $"{partner.CompanyData?.Tel ?? ""},";
                registerLine += $"{partner.CompanyData?.Cel1 ?? ""},";
                registerLine += $"{partner.CompanyData?.Cel2 ?? ""},";
                registerLine += $"{partner.CompanyData?.Pec ?? ""},";
                registerLine += $"{partner.CompanyData?.UniqueCode ?? ""},";
                registerLine += $"{partner.CompanyData?.Responsible?.Name ?? ""},";
                registerLine += $"{partner.CompanyData?.Responsible?.Surname ?? ""},";
                registerLine += $"{partner.CompanyData?.Responsible?.Tel ?? ""},";
                registerLine += $"{partner.CompanyData?.Responsible?.Cel ?? ""}";

                csvStringBuilder.AppendLine(registerLine);
            }
           
            return new DTOResponse<string>()
            {
                Code = 200,
                Data = csvStringBuilder.ToString()
            };
        }

        #endregion

        #region Privates

        private void LoadPartnerData(DTOPartner dtoPartner, PartnerModel partnerModel)
        {
            if (partnerModel.Categories != null)
            {
                var getCategoriesResult = _partnerCategoryDomain.GetByIdList(partnerModel.Categories);
                if (getCategoriesResult.Code == 200)
                {
                    dtoPartner.Categories = getCategoriesResult.Data;
                }
            }
            
            if (!string.IsNullOrWhiteSpace(partnerModel.ImageId))
            {
                var getImageResult = _filesDomain.GetImageById(partnerModel.ImageId);
                if(getImageResult.Code == 200)
                {
                    dtoPartner.Image = getImageResult.Data;
                }
            }

            var hasContractId = !string.IsNullOrWhiteSpace(partnerModel.ContractId);
            var hasConsensoId = !string.IsNullOrWhiteSpace(partnerModel.ConsensoId);
            var hasPrivacyId = !string.IsNullOrWhiteSpace(partnerModel.PrivacyId);
            var hasPagamentoId = !string.IsNullOrWhiteSpace(partnerModel.PagamentoId);

            if (hasContractId || hasConsensoId || hasPrivacyId || hasPagamentoId)
            {
                dtoPartner.Documentation = new DTOPartnerDocumentation();

                if(hasContractId)
                {
                    var getFileResult = _filesDomain.GetDocumentById(partnerModel.ContractId);
                    if (getFileResult.Code == 200)
                    {
                        dtoPartner.Documentation.Contract = getFileResult.Data;
                    }
                }

                if (hasConsensoId)
                {
                    var getFileResult = _filesDomain.GetDocumentById(partnerModel.ConsensoId);
                    if (getFileResult.Code == 200)
                    {
                        dtoPartner.Documentation.Consenso = getFileResult.Data;
                    }
                }

                if (hasPrivacyId)
                {
                    var getFileResult = _filesDomain.GetDocumentById(partnerModel.PrivacyId);
                    if (getFileResult.Code == 200)
                    {
                        dtoPartner.Documentation.Privacy = getFileResult.Data;
                    }
                }

                if (hasPagamentoId)
                {
                    var getFileResult = _filesDomain.GetDocumentById(partnerModel.PagamentoId);
                    if (getFileResult.Code == 200)
                    {
                        dtoPartner.Documentation.Pagamento = getFileResult.Data;
                    }
                }
            }

        }

        #endregion
    }
}
