using System;
using System.Collections.Generic;

namespace PrjFarmacia_David.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            VentasCabs = new HashSet<VentasCab>();
        }

        public int CodVen { get; set; }
        public string? NomVen { get; set; }
        public DateTime? FecingVen { get; set; }

        public virtual ICollection<VentasCab> VentasCabs { get; set; }
    }
}
