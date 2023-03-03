using System.ComponentModel.DataAnnotations;

namespace TaskAPI.DTOs
{
    public class newpasswordDTO
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
    }
}
