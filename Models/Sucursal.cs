﻿using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class Sucursal
{
    public int SucursalesId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<TicketDeVentum> TicketDeVenta { get; set; } = new List<TicketDeVentum>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
