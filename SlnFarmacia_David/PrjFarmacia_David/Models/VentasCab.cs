using System;
using System.Collections.Generic;

namespace PrjFarmacia_David.Models
{
    public partial class VentasCab
    {
        public VentasCab()
        {
            VentasDeta = new HashSet<VentasDetum>();
        }

        public string NumVta { get; set; } = null!;
        public DateTime? FecVta { get; set; }
        public int? CodCli { get; set; }
        public int? CodVen { get; set; }
        public decimal? TotVta { get; set; }
        public string? Anulado { get; set; }

        public virtual Cliente? CodCliNavigation { get; set; }
        public virtual Vendedor? CodVenNavigation { get; set; }
        public virtual ICollection<VentasDetum> VentasDeta { get; set; }
    }
}
