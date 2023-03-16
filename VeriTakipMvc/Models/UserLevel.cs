using System.ComponentModel.DataAnnotations;

namespace VeriTakipMvc.Models
{
    public class UserLevel
    {
        [Key]
        public int? UserLevelId { get; set; } = null;
        public string? UserLevelType { get; set; } = null!;



        public virtual ICollection<UserOfLevel> UserOfLevels { get; set; }
    }
}
