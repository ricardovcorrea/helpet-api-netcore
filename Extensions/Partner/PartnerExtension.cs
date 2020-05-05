using System;
using System.Collections.Generic;
using System.Linq;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class PartnerExtension
    {
        public static DTOPartner ToDTO(this PartnerModel model)
        {
            DTOCompany companyData = null;

            if(!string.IsNullOrWhiteSpace(model.BusinessName))
            {
                companyData = new DTOCompany()
                {
                    BusinessName = model.BusinessName,
                    TaxCode = model.BusinessTaxCode,
                    VatNumber = model.BusinessVatNumber,
                    Tel = model.BusinessTel,
                    Cel1 = model.BusinessCel1,
                    Cel2 = model.BusinessCel2,
                    UniqueCode = model.BusinessUniqueCode,
                    Pec = model.BusinessPec,
                    Address = new DTOAddress()
                    {
                        Street = model.BusinessStreet,
                        Number = model.BusinessNumber,
                        PostalCode = model.BusinessPostalCode,
                        City = model.BusinessCity,
                        Country = model.BusinessCountry,
                        State = model.BusinessState
                    },
                    Responsible = new DTOUser()
                    {
                        Name = model.BusinessResponsibleName,
                        Surname = model.BusinessResponsibleSurname,
                        Cel = model.BusinessResponsibleCel,
                        Tel = model.BusinessResponsibleTel
                    }
                };
            }

            return new DTOPartner()
            {
                Id = model.Id,
                Name = model.Name,
                Email1 = model.Email1,
                Email2 = model.Email2,
                Tel1 = model.Tel1,
                Tel2 = model.Tel2,
                Cel1 = model.Cel1,
                Cel2 = model.Cel2,
                Url = model.Url,
                FacebookUrl = model.FacebookUrl,
                InstagramUrl = model.InstagramUrl,
                IsChecked = model.IsChecked,
                IsAuthorized = model.IsAuthorized,
                ShowInApp = model.ShowInApp,
                Accountable = model.Accountable,
                Description = model.Description,
                Notes = model.Notes,
                SubscriptionLevel = (PartnerSubscriptionLevel)model.SubscriptionLevel,
                InfoStatus = (PartnerInfoStatus)model.InfoStatus,
                Address = new DTOAddress()
                {
                    Street = model.Street,
                    Number = model.Number,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    Country = model.Country,
                    State = model.State,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                },
                CompanyData = companyData,
                Openings = model.Openings
            };
        }

        public static PartnerModel ToModel(this DTOPartner dto)
        {
            int infoStatus = 0;

            if(!string.IsNullOrWhiteSpace(dto.Name) && !string.IsNullOrWhiteSpace(dto.Address.Street) && !string.IsNullOrWhiteSpace(dto.Address.Number) && !string.IsNullOrWhiteSpace(dto.Address.City) && !string.IsNullOrWhiteSpace(dto.Address.State) && dto.IsChecked)
            {
                infoStatus = 2;
            }
            else if (!string.IsNullOrWhiteSpace(dto.Name) && !string.IsNullOrWhiteSpace(dto.Address.Street) && !string.IsNullOrWhiteSpace(dto.Address.Number) && !string.IsNullOrWhiteSpace(dto.Address.City) && !string.IsNullOrWhiteSpace(dto.Address.State) && !dto.IsChecked)
            {
                infoStatus = 1;
            }

            return new PartnerModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Email1 = dto.Email1,
                Email2 = dto.Email2,
                Tel1 = dto.Tel1,
                Tel2 = dto.Tel2,
                Cel1 = dto.Cel1,
                Cel2 = dto.Cel2,
                Url = dto.Url,
                FacebookUrl = dto.FacebookUrl,
                InstagramUrl = dto.InstagramUrl,
                IsChecked = dto.IsChecked,
                IsAuthorized = dto.IsAuthorized,
                ShowInApp = dto.ShowInApp,
                SubscriptionLevel = (int)dto.SubscriptionLevel,
                Accountable = dto.Accountable,
                Street = dto.Address?.Street ?? "",
                Number = dto.Address?.Number ?? "",
                PostalCode = dto.Address?.PostalCode ?? "",
                City = dto.Address?.City ?? "",
                Country = dto.Address?.Country ?? "",
                State = dto.Address?.State ?? "",
                Latitude = dto.Address?.Latitude ?? "",
                Longitude = dto.Address?.Longitude ?? "",
                Categories = dto.Categories?.Select(category => category.Id).ToList() ?? new List<string>(),
                Description = dto.Description,
                Notes = dto.Notes,
                InfoStatus = infoStatus,
                BusinessName = dto.CompanyData?.BusinessName ?? "",
                BusinessStreet = dto.CompanyData?.Address?.Street ?? "",
                BusinessNumber = dto.CompanyData?.Address?.Number ?? "",
                BusinessPostalCode = dto.CompanyData?.Address?.PostalCode ?? "",
                BusinessCity = dto.CompanyData?.Address?.City ?? "",
                BusinessCountry = dto.CompanyData?.Address?.Country ?? "",
                BusinessState = dto.CompanyData?.Address?.State ?? "",
                BusinessPec = dto.CompanyData?.Pec ?? "",
                BusinessTaxCode = dto.CompanyData?.TaxCode ?? "",
                BusinessVatNumber = dto.CompanyData?.VatNumber ?? "",
                BusinessTel = dto.CompanyData?.Tel ?? "",
                BusinessCel1 = dto.CompanyData?.Cel1 ?? "",
                BusinessCel2 = dto.CompanyData?.Cel2 ?? "",
                BusinessUniqueCode = dto.CompanyData?.UniqueCode ?? "",
                BusinessResponsibleName = dto.CompanyData?.Responsible?.Name ?? "",
                BusinessResponsibleSurname = dto.CompanyData?.Responsible?.Surname ?? "",
                BusinessResponsibleTel = dto.CompanyData?.Responsible?.Tel ?? "",
                BusinessResponsibleCel = dto.CompanyData?.Responsible?.Cel ?? "",
                ImageId = dto.Image?.Id,
                ContractId = dto.Documentation?.Contract?.Id ?? "",
                ConsensoId = dto.Documentation?.Consenso?.Id ?? "",
                PrivacyId = dto.Documentation?.Privacy?.Id ?? "",
                PagamentoId = dto.Documentation?.Pagamento?.Id ?? "",
                Openings = dto.Openings
            };
        }
    }
}
