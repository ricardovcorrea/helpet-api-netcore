using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class PartnerModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Categories { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public string Tel1 { get; set; }
        public string Tel2 { get; set; }

        public string Cel1 { get; set; }
        public string Cel2 { get; set; }

        public string Url { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }

        public bool IsChecked { get; set; }
        public bool IsAuthorized { get; set; }
        public bool ShowInApp { get; set; }

        public int SubscriptionLevel { get; set; }

        public string Accountable { get; set; }

        public string Description { get; set; }
        public string Notes { get; set; }

        public int InfoStatus { get; set; }

        public string BusinessName { get; set; }

        public string BusinessStreet { get; set; }
        public string BusinessNumber { get; set; }
        public string BusinessPostalCode { get; set; }
        public string BusinessCity { get; set; }
        public string BusinessCountry { get; set; }
        public string BusinessState { get; set; }

        public string BusinessTaxCode { get; set; }
        public string BusinessVatNumber { get; set; }

        public string BusinessTel { get; set; }

        public string BusinessCel1 { get; set; }
        public string BusinessCel2 { get; set; }

        public string BusinessPec { get; set; }

        public string BusinessUniqueCode { get; set; }
        public string BusinessResponsibleName { get; set; }
        public string BusinessResponsibleSurname { get; set; }

        public string BusinessResponsibleTel { get; set; }
        public string BusinessResponsibleCel { get; set; }

        public string ImageId { get; set; }

        public string ContractId { get; set; }
        public string ConsensoId { get; set; }
        public string PrivacyId { get; set; }
        public string PagamentoId { get; set; }

        public List<string> Openings { get; set; }
    }
}
