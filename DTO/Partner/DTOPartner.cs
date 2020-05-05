using System;
using System.Collections.Generic;
using Api.enumerators;

namespace Api.DTO
{
    public class DTOPartner
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<DTOPartnerCategory> Categories { get; set; }

        public DTOAddress Address { get; set; }

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

        public PartnerSubscriptionLevel SubscriptionLevel { get; set; }

        public string Accountable { get; set; }

        public string Description { get; set; }
        public string Notes { get; set; }

        public PartnerInfoStatus InfoStatus { get; set; }

        public DTOCompany CompanyData { get; set; }

        public DTOFile Image { get; set; }

        public DTOPartnerDocumentation Documentation { get; set; }

        public List<string> Openings { get; set; }
    }
}
