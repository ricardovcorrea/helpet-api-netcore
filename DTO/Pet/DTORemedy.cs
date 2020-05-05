namespace Api.DTO
{
    public class DTORemedy
    {
        public string Id { get; set; }
        
        public DTOFile Image { get; set; }
        public string Name { get; set; }

        public string Veterinary { get; set; }
        public string Dosage { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Interval { get; set; }

        public string Observations { get; set; }
        
        
    }
}
