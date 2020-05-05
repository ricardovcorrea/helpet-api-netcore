using System;
using System.Collections.Generic;
using Api.enumerators;

namespace Api.DTO
{
    public class DTOPartnerDocumentation
    {
        public DTOFile Contract { get; set; }
        public DTOFile Consenso { get; set; }
        public DTOFile Privacy { get; set; }
        public DTOFile Pagamento { get; set; }
    }
}
