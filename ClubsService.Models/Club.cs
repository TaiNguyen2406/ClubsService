using System.ComponentModel.DataAnnotations;

namespace ClubsService.Models
{
    public class Club
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
