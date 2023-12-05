using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisbeth_Hair_Salon.Models
{
    public class TicketServicio
    {
        [Key]
        public int TicketServicioId { get; set; }

        [ForeignKey("TicketDeVenta")]
        public int TicketId { get; set; }

        [ForeignKey("Menu")]
        public int ServicioId { get; set; }

        public virtual TicketDeVenta TicketDeVenta { get; set; }

        public virtual Menu NombreServicio { get; set; }
    }
}

