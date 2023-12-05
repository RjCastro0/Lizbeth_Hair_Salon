using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class Venta
{
    public int TicketId { get; set; }

    public int ServicioId { get; set; }

    public decimal Precio { get; set; }

    public virtual Menu Servicio { get; set; } = null!;

    public int TicketDeVentaId { get; set; }

    public virtual TicketDeVenta Ticket { get; set; } = null!;

    
}
