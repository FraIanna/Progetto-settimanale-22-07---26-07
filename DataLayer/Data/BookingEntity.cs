using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class BookingEntity
    {
        public int BookingId { get; set; }

        [Required]
        [StringLength(16)]
        public string ClientFiscalCode { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int NumeroProgressivoAnno { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Caparra { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfStay { get; set; }
    }
}
