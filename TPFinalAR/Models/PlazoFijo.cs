﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalAR.Models
{
    public class PlazoFijo
    {
		public PlazoFijo() { }
        public PlazoFijo(double monto, DateTime fechaFin, double tasa)
        {
			_monto = monto;
			_fechaIni= DateTime.Now;
            _fechaFin = fechaFin;
			_tasa = tasa;
			_pagado = false;
        }
		
		

        public PlazoFijo(int id, int id_usuario, double monto, DateTime fechaFin, double tasa, bool pagado)
        {
            _id_plazoFijo = id;
            _id_usuario = id_usuario;
            _monto = monto;
            _fechaIni = DateTime.Now;
            _fechaFin = fechaFin;
            _tasa = tasa;
            _pagado = pagado;
        }
        private int id;

        [Display(Name = "ID Plazo Fijo")]
        public int _id_plazoFijo
		{
			get { return id; }
			set { id = value; }
		}

		private int id_usuario;

        [Display(Name = "ID Usuario")]
        public int _id_usuario
		{
			get { return id_usuario; }
			set { id_usuario = value; }
		} 


		private Usuario titular;

        [Display(Name = "Titular")]
        public Usuario? _titular
		{
			get { return titular; }
			set { titular = value; }
		}

		private double monto;

        [Display(Name = "Importe")]
        public double _monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private DateTime fechaIni;

        [Display(Name = "Fecha inicio")]
        public DateTime _fechaIni
		{
			get { return fechaIni; }
			set { fechaIni = value; }
		}

		private DateTime fechaFin;

        [Display(Name = "Fecha fin")]
        public DateTime _fechaFin
		{
			get { return fechaFin; }
			set { fechaFin = value; }
		}

		private double tasa;

        [Display(Name = "Tasa aplicada")]
        public double _tasa
		{
			get { return tasa; }
			set { tasa = value; }
		}

		private bool pagado;

        [Display(Name = "Pagado")]
        public bool _pagado
		{
			get { return pagado; }
			set { pagado = value; }
		}

	}
}
