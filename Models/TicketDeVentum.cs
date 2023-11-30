using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class TicketDeVentum
{
    public int TicketId { get; set; }

    public int SurcursalId { get; set; }

    public string Empleada { get; set; } = null!;

    public string ClienteNombre { get; set; } = null!;

    public virtual ICollection<RegistroDeVenta> RegistroDeVenta { get; set; } = new List<RegistroDeVenta>();

    public virtual Sucursal Surcursal { get; set; } = null!;
}
