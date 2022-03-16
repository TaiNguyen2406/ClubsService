using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsService.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid? ClubId { get; set; }
    }
}
