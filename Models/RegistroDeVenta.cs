using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class RegistroDeVenta
{
    public int VentaId { get; set; }

    public int TicketId { get; set; }

    public int ServicioId { get; set; }

    public decimal Total { get; set; }

    public DateTime Fecha { get; set; }

    public string Status { get; set; } = null!;

    public virtual Menu Servicio { get; set; } = null!;

    public virtual TicketDeVentum Ticket { get; set; } = null!;
}
