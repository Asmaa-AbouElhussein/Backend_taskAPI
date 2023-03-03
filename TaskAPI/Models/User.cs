using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class User
    {
        public int id { set; get; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string username { set; get; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { set; get; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string password { set; get; }

        public bool IsDeleted { get; set; } = false;




    }
    public class Username
    {
        public string username { set; get; }
    }

}
