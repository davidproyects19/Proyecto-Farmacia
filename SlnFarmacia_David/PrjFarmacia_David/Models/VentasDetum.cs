using System;
using System.Collections.Generic;

namespace PrjFarmacia_David.Models
{
    public partial class VentasDetum
    {
        public string NumVta { get; set; } = null!;
        public int CodPro { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public string? Anulado { get; set; }

        public virtual Producto CodProNavigation { get; set; } = null!;
        public virtual VentasCab NumVtaNavigation { get; set; } = null!;
    }
}
