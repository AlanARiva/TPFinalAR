using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class Usuario
    {
        private int id;
        private int dni;
        private string nombre;
        private string apellido;
        private string mail;
        private string password;
        private int intentosFallidos;
        private bool/*int*/ esUsuarioAdmin;
        private bool bloqueado;

        public ICollection<CajaDeAhorro> cajas { get; } = new List<CajaDeAhorro>();
        public List<UsuarioCajaDeAhorro>? usuarioCajas { get; set; }

        
        public List<PlazoFijo> _plazosFijos = new List<PlazoFijo>();
        public List<TarjetaDeCredito> _tarjetas = new List<TarjetaDeCredito>();
        public List<Pago> _pagos = new List<Pago>();


        public Usuario() { }
        public Usuario(int dni, string nombre, string apellido, string mail, string password, bool bloqueado,bool/*int*/ esUsuarioAdmin,int intentosFallidos)
        {
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _mail = mail;
            _password = password;
            _bloqueado = bloqueado;
            _esUsuarioAdmin = esUsuarioAdmin;
            _intentosFallidos = intentosFallidos;  

        }

        public Usuario(int id, int dni, string nombre, string apellido, string mail, string password, bool bloqueado, bool/*int*/ esUsuarioAdmin, int intentosFallidos)
        {
            _id_usuario = id;
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _mail = mail;
            _password = password;
            _bloqueado = bloqueado;
            _esUsuarioAdmin = esUsuarioAdmin;
            _intentosFallidos = intentosFallidos;

        }

        public int _id_usuario
        {
            get { return id; }
            set { id = value; }
        }

        [Display(Name = "DNI")]
        public int _dni
        {
            get { return dni; }
            set { dni = value; }
        }

        [Display(Name = "Nombre")]
        public string _nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [Display(Name = "Apellido")]
        public string _apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        [Display(Name = "Mail")]
        public string _mail
        {
            get { return mail; }
            set { mail = value; }
        }

        [Display(Name = "Contraseña")]
        public string _password
        {
            get { return password; }
            set { password = value; }
        }

        [Display(Name = "Cant. Fallos")]
        public int _intentosFallidos
        {
            get { return intentosFallidos; }
            set { intentosFallidos = value; }
        }

        [Display(Name = "Admin")]
        public bool _esUsuarioAdmin
        {
            get { return esUsuarioAdmin; }
            set { esUsuarioAdmin = value; }
        }

        [Display(Name = "Bloqueado")]
        public bool _bloqueado
        {
            get { return bloqueado; }
            set { bloqueado = value; }
        }


    }
}
