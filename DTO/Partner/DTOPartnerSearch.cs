using System;
using Api.enumerators;

namespace Api.DTO
{
    public class DTOPartnerSearch
    {
        public PartnerSearchColumn Column { get; set; }
        public string Term { get; set; }
        public int Offset { get; set; } = 0;
        public int Size { get; set; } = 50;
    }
}
