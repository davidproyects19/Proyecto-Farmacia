using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrjFarmacia_David.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            VentasCabs = new HashSet<VentasCab>();
        }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? EliCli { get; set; }

        public virtual ICollection<VentasCab> VentasCabs { get; set; }
    }
}
