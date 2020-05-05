using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class PetModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }
        public string Weight { get; set; }
        public string Birthdate { get; set; }
        public string DistinctiveFeature { get; set; }
        public string MicrochipOrTattoNumber { get; set; }
        public string MicrochipOrTattoApplyDate { get; set; }
        public string GpsColarId { get; set; }

        public int Gender { get; set; }

        public string DocumentFrontImageId { get; set; }
        
        public string ImageId { get; set; }

        public string BreedId { get; set; }
        public string CoatId { get; set; }
        public string FurColorId { get; set; }

        public string UserId { get; set; }

    }
}
