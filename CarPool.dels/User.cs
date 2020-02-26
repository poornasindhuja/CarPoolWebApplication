using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        [Required]
        public string PetName { get; set; }

        public string Password { get; set; }

    }
}
