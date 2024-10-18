using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG6212_POEPART2.Models
{
    public class Lecturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LecturerID { get; set; }

        [Required]
        [Range (1, 200)]
        public int HoursWorked { get; set; }

        [Required]
        [Range (1, 20000)]
        public int HourlyRate { get; set; }

        [Required]
        public string AdditionalNotes { get; set; }
    }
}
