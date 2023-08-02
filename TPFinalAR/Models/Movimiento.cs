using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class Movimiento
    {

        public Movimiento() { }
        public Movimiento(int id_CajaDeAhorro,String detalle, double monto, DateTime fecha)
		{
			//_id_Movimiento = id;
			_id_CajaDeAhorro = id_CajaDeAhorro;
			_detalle = detalle;
			_monto = monto;
			_fecha= fecha;
		}

		private int id_CajaDeAhorro;

        [Display(Name = "CBU")]
        public int _id_CajaDeAhorro
		{
			get { return id_CajaDeAhorro; }
			set { id_CajaDeAhorro = value; }
		}

         private int id;

        [Display(Name = "ID Movimiento")]
        public int _id_Movimiento
        {
			get { return id; }
			set { id = value; }
		}

		private CajaDeAhorro cajaDeAhorro;

        [Display(Name = "CBU")]
        public CajaDeAhorro _cajaDeAhorro
		{
			get { return cajaDeAhorro; }
			set { cajaDeAhorro = value; }
		}

		private string detalle;

        [Display(Name = "Detalle")]
        public string _detalle
		{
			get { return detalle; }
			set { detalle = value; }
		}

		private double monto;

        [Display(Name = "Monto")]
        public double _monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private DateTime fecha;

        [Display(Name = "Fecha")]
        public DateTime _fecha
		{
			get { return fecha; }
			set { fecha = value; }
		}

        public override string ToString()
        {
            return "Id: " + _id_Movimiento + " Id Caja: " + _id_CajaDeAhorro + " Detalle: " + _detalle+ " Monto: "+_monto;
        }


    }
}
