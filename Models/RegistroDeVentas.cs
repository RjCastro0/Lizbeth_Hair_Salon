using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class RegistroDeVentas
{
    public int VentaId { get; set; }

    public int TicketId { get; set; }

    public decimal Total { get; set; }

    public DateTime Fecha { get; set; }

    public virtual TicketDeVenta Ticket { get; set; } = null!;
}
