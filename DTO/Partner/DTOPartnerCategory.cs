using System;
namespace Api.DTO
{
    public class DTOPartnerCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public DTOFile Icon { get; set; }
    }
}
