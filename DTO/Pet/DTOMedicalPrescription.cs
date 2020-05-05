namespace Api.DTO
{
    public class DTOMedicalPrescription
    {
        public string Id { get; set; }

        public string Number { get; set; }
        public string Veterinary { get; set; }
        public string PIN { get; set; }
        public DTOFile Image { get; set; }
        public string OriginalFileUrl { get; set; }
    }
}
