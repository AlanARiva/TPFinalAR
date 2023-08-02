using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class UsuarioCajaDeAhorro
    {
        [Display(Name = "CBU")]
        public int id_caja { get; set; }

        [Display(Name = "CBU")]
        public CajaDeAhorro? caja { get; set; }

        [Display(Name = "DNI")]
        public int id_usuario { get; set; }

        [Display(Name = "DNI")]
        public Usuario? user { get; set; }
        public UsuarioCajaDeAhorro() { }
        public UsuarioCajaDeAhorro(int id_usuario, Usuario user, int id_caja, CajaDeAhorro caja) {
            this.id_caja = id_caja;
            this.caja = caja;
            this.id_usuario = id_usuario;
            this.user = user;
        }
    }
}
