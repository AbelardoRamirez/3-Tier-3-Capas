using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _3_Capas.VO
{
	public class ChoferVO
	{

		#region Campos Privados
		private int idChofer;
		private string nombre;
		private string appaterno;
		private string apmaterno;
		private string telefono;
		private DateTime fechanacimiento;
		private string licencia;
		private bool disponibilidad;
		private string UrlFoto;
		#endregion

		public ChoferVO()
		{
			IdChofer = 0;
			Nombre = "";
			ApPaterno = "";
			ApMaterno = "";
			Telefono = "";
			FechaNacimiento = DateTime.MinValue;
			Licencia = "";
			Disponibilidad = false;
			Urlfoto = "";
		}

		public ChoferVO(DataRow row)
		{
			IdChofer = int.Parse(row["IdChofer"].ToString().Trim());
			Nombre = row["Nombre"].ToString().Trim();
			ApPaterno = row["ApPaterno"].ToString().Trim();
			ApMaterno = row["ApMaterno"].ToString().Trim();
			Telefono = row["Telefono"].ToString().Trim();
			FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"].ToString().Trim());
			Licencia = row["Licencia"].ToString().Trim();
			Disponibilidad = bool.Parse(row["Disponibilidad"].ToString().Trim());
			UrlFoto = row["Urlfoto"].ToString().Trim();
		}

		#region Campos Publicos
		public int IdChofer
		{
			get
			{
				return idChofer;
			}

			set
			{
				idChofer = value;
			}
		}

		public string Nombre
		{
			get
			{
				return nombre;
			}

			set
			{
				nombre = value;
			}
		}

		public string Telefono
		{
			get
			{
				return telefono;
			}

			set
			{
				telefono = value;
			}
		}

		public DateTime FechaNacimiento
		{
			get
			{
				return fechanacimiento;
			}

			set
			{
				fechanacimiento = value;
			}
		}

		public string Licencia
		{
			get
			{
				return licencia;
			}

			set
			{
				licencia = value;
			}
		}
		public string Urlfoto
		{
			get
			{
				return UrlFoto;
			}

			set
			{
				UrlFoto = value;
			}
		}
		public bool Disponibilidad
		{
			get
			{
				return disponibilidad;
			}

			set
			{
				disponibilidad = value;
			}
		}

		public string ApPaterno
		{
			get
			{
				return appaterno;
			}

			set
			{
				appaterno = value;
			}
		}

		public string ApMaterno
		{
			get
			{
				return apmaterno;
			}

			set
			{
				apmaterno = value;
			}
		}
	}
	#endregion



}