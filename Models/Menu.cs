using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class Menu
{
    public int ServicioId { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string Statuts { get; set; } = null!;

    public virtual ICollection<TicketDeVentum> TicketDeVenta { get; set; } = new List<TicketDeVentum>();
}
