using System.Collections.Generic;

namespace Api.DTO
{
    public class DTOExam
    {
        public string Id { get; set; }

        public string Veterinary { get; set; }
        public string VeterinaryClinic { get; set; }
        public string VeterinaryClinicPhone { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Observations { get; set; }

        public List<DTOFile> Photos { get; set; }
    }
}
