
using System.Collections.Generic;
using Api.enumerators;

namespace Api.DTO
{
    public class DTOAvailableInsurance
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool HasAcceptedTermsAndPrivacyPolice { get; set; }
        public bool HasAcceptedEmailMarketing { get; set; }
        
        public List<DTOPet> Pets { get; set; }
        public UserType Type { get; set; }
    }
}
