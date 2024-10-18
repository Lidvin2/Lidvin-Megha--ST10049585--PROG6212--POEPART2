using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace PROG6212_POEPART2.Models
{
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimID { get; set; }
        [Required]
        public string ClaimName { get; set; }
        [Required] 
        public string ClaimSurname { get; set; }
        [Required]
        public string ClaimTitle { get; set; }
        [Required]
        public DateTime ClaimDate { get; set; }
        [Required]
        [Range(1, 200)]
        public int HoursWorked { get; set; }
        [Required]
        [Range(1, 200000)]
        public int HourlyRate { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string UploadFilePath { get; set; }
        [Required]
        public string Status {  get; set; }

        public bool IsValid()
        {
            // You can add your own validation logic here.
            return ClaimID > 0 && !string.IsNullOrEmpty(ClaimName) && Amount > 0 && HoursWorked > 0;
        }
    }
}
