using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace _3CapasGen2Q.VO
{
    public class RutaVO
    {
        private int _IdRuta;
        private string _Nombre;
        private string _Licencia;
        private string _FotoCH;
        private string _Matricula;
        private string _FotoCamion;
        private string _Origen;
        private string _Destino;
        private DateTime _Salida;
        private DateTime _Llegada;
        private bool _ATiempo;
        private double _Distancia;

        public int IdRuta
        {
            get
            {
                return _IdRuta;
            }

            set
            {
                _IdRuta = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public string Licencia
        {
            get
            {
                return _Licencia;
            }

            set
            {
                _Licencia = value;
            }
        }

        public string FotoCH
        {
            get
            {
                return _FotoCH;
            }

            set
            {
                _FotoCH = value;
            }
        }

        public string Matricula
        {
            get
            {
                return _Matricula;
            }

            set
            {
                _Matricula = value;
            }
        }

        public string FotoCamion
        {
            get
            {
                return _FotoCamion;
            }

            set
            {
                _FotoCamion = value;
            }
        }

        public string Origen
        {
            get
            {
                return _Origen;
            }

            set
            {
                _Origen = value;
            }
        }

        public string Destino
        {
            get
            {
                return _Destino;
            }

            set
            {
                _Destino = value;
            }
        }

        public DateTime Salida
        {
            get
            {
                return _Salida;
            }

            set
            {
                _Salida = value;
            }
        }

        public DateTime Llegada
        {
            get
            {
                return _Llegada;
            }

            set
            {
                _Llegada = value;
            }
        }

        public bool ATiempo
        {
            get
            {
                return _ATiempo;
            }

            set
            {
                _ATiempo = value;
            }
        }

        public double Distancia
        {
            get
            {
                return _Distancia;
            }

            set
            {
                _Distancia = value;
            }
        }


        public RutaVO()
        {
            IdRuta = 0;
            Nombre = "";
            Licencia = "";
            FotoCH = "";
            Matricula = "";
            FotoCamion = "";
            Origen = "";
            Destino = "";
            Salida = DateTime.Now;
            Llegada = DateTime.Now;
            ATiempo = false;
            Distancia = 0;
        }

        public RutaVO(DataRow dr)
        {
            IdRuta = int.Parse(dr["IdRuta"].ToString());
            Nombre = dr["Nombre"].ToString();
            Licencia = dr["Licencia"].ToString();
            FotoCH = dr["FotoChofer"].ToString();
            Matricula = dr["Matricula"].ToString();
            FotoCamion = dr["FotoCamion"].ToString();
            Origen = dr["Origen"].ToString();
            Destino = dr["Destino"].ToString();
            Salida = DateTime.Parse(dr["FHSalida"].ToString());
            Llegada = DateTime.Parse(dr["FHLlegada"].ToString());
            ATiempo = bool.Parse(dr["Atiempo"].ToString());
            Distancia = double.Parse(dr["Distancia"].ToString());
        }
    }
}