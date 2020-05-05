using System.Collections.Generic;
using Api.enumerators;

namespace Api.DTO
{
    public class DTOPet
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public PetType Type { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public string Birthdate { get; set; }
        public string DistinctiveFeature { get; set; }
        public string MicrochipOrTattoNumber { get; set; }
        public string MicrochipOrTattoApplyDate { get; set; }
        public string GpsColarId { get; set; }

        public Gender Gender { get; set; }
        
        public DTOFile DocumentFrontImage { get; set; }
        
        public DTOFile Image { get; set; }

        public DTOBreed Breed { get; set; }
        public DTOCoat Coat { get; set; }
        public DTOFurColor FurColor { get; set; }

        public List<DTOVaccine> Vaccines { get; set; }
        public List<DTOMedicalPrescription> MedicalPrescriptions { get; set; }
        public List<DTOConsultation> Consultations { get; set; }
        public List<DTOIntervention> Interventions { get; set; }
        public List<DTOExam> Exams { get; set; }
        public List<DTORemedy> Remedies { get; set; }
        
        public List<DTOInsurance> Insurances { get; set; }
    }
}
