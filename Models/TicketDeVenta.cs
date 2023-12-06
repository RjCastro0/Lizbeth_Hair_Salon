﻿using System;
using System.Collections.Generic;

namespace Lisbeth_Hair_Salon.Models;

public partial class TicketDeVenta
{
    public int TicketId { get; set; }

    public int SurcursalId { get; set; }

    public string Empleada { get; set; } = null!;

    public int ServicioId { get; set; }

    public decimal Precio { get; set; }

    public string ClienteNombre { get; set; } = null!;

    public virtual ICollection<RegistroDeVentas> RegistroDeVenta { get; set; } = new List<RegistroDeVentas>();

    public virtual Menu Servicio { get; set; } = null!;

    public virtual Sucursal Surcursal { get; set; } = null!;

    public ICollection<Venta> Ventas { get; set; }
}