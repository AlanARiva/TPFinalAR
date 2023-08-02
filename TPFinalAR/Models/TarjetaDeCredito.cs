using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class TarjetaDeCredito
	{

		public TarjetaDeCredito() { }

        public TarjetaDeCredito(string numero, int codigoV, double limite, double consumos)
        {
            _id_tarjeta = id;
            _id_usuario = id_usuario;
            _numero = numero;
            _codigoV = codigoV;
            _limite = limite;
            _consumos = consumos;
        }

        public TarjetaDeCredito(int id, int id_usuario, string numero, int codigoV, double limite, double consumos)
		{
            _id_tarjeta = id;
			_id_usuario = id_usuario;
			_numero = numero;
			_codigoV = codigoV;
			_limite = limite;
			_consumos = consumos;
		}

		private int id = 0;

        [Display(Name = "ID Tarjeta")]
        public int _id_tarjeta
		{
			get { return id; }
			set { id = value; }
		}

		private Usuario titular;

        [Display(Name = "Titular")]
        public Usuario? _titular
		{
			get { return titular; }
			set { titular = value; }
		}

		private int id_usuario;

        [Display(Name = "ID Usuario")]
        public int _id_usuario
		{
			get { return id_usuario; }
			set { id_usuario = value; }
		}


		private string numero;

        [Display(Name = "Nro. Tarjeta")]
        public string? _numero
		{
			get { return numero; }
			set { numero = value; }
		}

		private int codigoV;

        [Display(Name = "Cod. Ver.")]
        public int _codigoV
		{
			get { return codigoV; }
			set { codigoV = value; }
		}

		private double limite;

        [Display(Name = "Límite")]
        public double _limite
		{
			get { return limite; }
			set { limite = value; }
		}

		private double consumos;

        [Display(Name = "Consumos")]
        public double _consumos
		{
			get { return consumos; }
			set { consumos = value; }
		}


	}
}
