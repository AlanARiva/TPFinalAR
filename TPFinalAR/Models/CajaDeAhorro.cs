﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class CajaDeAhorro
    {
        /*
        private List<int> titulares = new List<int>();
        */
        private List<Movimiento> movimientos { get; set; } = new List<Movimiento>();
        
        public ICollection<Usuario> titulares { get; } = new List<Usuario>();

        public List<UsuarioCajaDeAhorro>? usuarioCajas { get; set; }

        public CajaDeAhorro() { }

        public CajaDeAhorro(string cbu, double saldo)
        {
            _cbu = cbu;
            //titulares.Add(usuario);
            _saldo = saldo;

        }

        public CajaDeAhorro(int id,string cbu, int usuario,double saldo)
        {
            _id_caja = id;
            _cbu = cbu;
            //_titulares.Add(usuario);
			_saldo = saldo;

        }

        public CajaDeAhorro(int id, string cbu, double saldo)
        {
            _id_caja = id;
            _cbu = cbu;
            _saldo = saldo;

        }



        private int id = 0;

		public int _id_caja
		{
			get { return id; }
			set { id = value; }
		}

		private string cbu;

        [Display(Name = "CBU")]
        public string _cbu
		{
			get { return cbu; }
			set { cbu = value; }
		}

        
		public List<Movimiento> _movimientos
		{
			get { return movimientos; }
			set { movimientos =value; }
		}

        public List<PlazoFijo>? plazosFijos { get; set; } = new List<PlazoFijo>();

        private double saldo;

        [Display(Name = "Saldo")]
        public double _saldo
		{
			get { return saldo; }
			set { saldo = value; }
		}

        public override string ToString()
        {
            return "Id: " + _id_caja + " CBU: " + _cbu + " Saldo: " + _saldo+ " Movimientos:" + _movimientos.Count();
        }



    }
}
