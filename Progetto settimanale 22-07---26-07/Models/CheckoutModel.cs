using DataLayer.Data;
using System.ComponentModel.DataAnnotations;

namespace Progetto_settimanale_22_07___26_07.Models
{
    public class CheckoutModel
    {
        public int BookingId { get; set; }

        [Display(Name = "Numero Camera")]
        public int RoomNumber { get; set; }

        [Display(Name = "Data Inizio Soggiorno")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data Fine Soggiorno")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Tariffa")]
        public decimal Tax { get; set; }

        [Display(Name = "Totale")]
        public decimal TotalImport { get; set; }

        [Display(Name = "Servizi")]
        public List<ServiceDto> Services { get; set; }

    }
}
