using System.ComponentModel.DataAnnotations;

namespace TaskAPI.DTOs
{
    public class emaildataDTO
    {
        [Required]
        public string mailto { get; init; }
        [Required]
        public string subject { get; init; }

        [Required]
        public string code { get; init; }
    }
}
