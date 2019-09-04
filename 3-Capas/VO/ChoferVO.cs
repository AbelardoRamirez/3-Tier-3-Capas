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
        private string telefono;
        private int edad;
        private string licencia;
        private bool disponibilidad;
        private string UrlFoto;
        #endregion

        public ChoferVO()
        {
            IdChofer = 0;
            Nombre = "";
            Telefono = "";
            Edad = 0;
            Licencia = "";
            Disponibilidad = false;
            Urlfoto = "";
        }

        public ChoferVO(DataRow row)
        {
            IdChofer = int.Parse(row["IdChofer"].ToString().Trim());
            Nombre = row["Nombre"].ToString().Trim();
            Telefono = row["Telefono"].ToString().Trim();
            Edad = int.Parse(row["Edad"].ToString().Trim());
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

        public int Edad
        {
            get
            {
                return edad;
            }

            set
            {
                edad = value;
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


    }
    #endregion



}