using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrjFarmacia_David.Models
{
    public partial class Producto
    {
        public Producto()
        {
            VentasDeta = new HashSet<VentasDetum>();
        }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? EliPro { get; set; }

        public virtual ICollection<VentasDetum> VentasDeta { get; set; }
    }
}
