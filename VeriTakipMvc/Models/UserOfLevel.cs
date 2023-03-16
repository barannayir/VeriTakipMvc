using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeriTakipMvc.Models
{
    public class UserOfLevel
    {
        [Key]
        public int UserOfLevelID { get; set; }



        [ForeignKey("AppUser")]
        public int Id { get; set; }
        public AppUser AppUser { get; set; }


        [ForeignKey("UserLevel")]
        public int UserLevelId { get; set; }
        public UserLevel UserLevel { get; set; }

    }
}
