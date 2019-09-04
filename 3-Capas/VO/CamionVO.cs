using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//libreria de estructura de datos!!
using System.Data;

namespace _3_Capas.VO
{
    public class CamionVO
    {
        #region Datos Privados
        private int _IdCamion;
        private string _Matricula;
        private string _TipoCamion;
        private int _Modelo;
        private string _Marca;
        private int _Capacidad;
        private double _Kilometraje;
        private bool _Disponiblidad;
        private string _UrlFoto;
        #endregion

        #region Propiedades del objeto
        public int IdCamion {
            get
            {
                return _IdCamion;
            }
            set
            {
                _IdCamion = value;
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

        public string TipoCamion
        {
            get
            {
                return _TipoCamion;
            }

            set
            {
                _TipoCamion = value;
            }
        }

        public int Modelo
        {
            get
            {
                return _Modelo;
            }

            set
            {
                _Modelo = value;
            }
        }

        public string Marca
        {
            get
            {
                return _Marca;
            }

            set
            {
                _Marca = value;
            }
        }

        public int Capacidad
        {
            get
            {
                return _Capacidad;
            }

            set
            {
                _Capacidad = value;
            }
        }

        public double Kilometraje
        {
            get
            {
                return _Kilometraje;
            }

            set
            {
                _Kilometraje = value;
            }
        }

        public bool Disponiblidad
        {
            get
            {
                return _Disponiblidad;
            }

            set
            {
                _Disponiblidad = value;
            }
        }

        public string UrlFoto
        {
            get
            {
                return _UrlFoto;
            }

            set
            {
                _UrlFoto = value;
            }
        }
        #endregion
        #region Contructores
        //Sobrecarga ("Polifoormiso")
        public CamionVO()
        {
            //Vamos a contruir el contructor cuando el vo los cargue
            IdCamion = 0;
            Matricula = "";
            TipoCamion = "";
            Modelo = 0;
            Marca = "";
            Capacidad = 0;
            Kilometraje = 0;
            Disponiblidad = false;
            UrlFoto = "";
        }
        public CamionVO(DataRow dr)
        {
            IdCamion = int.Parse(dr["IdCamion"].ToString());
            Matricula = dr["Matricula"].ToString();
            TipoCamion = dr["TipoCamion"].ToString();
            Modelo = int.Parse(dr["Modelo"].ToString());
            Marca = dr["Marca"].ToString();
            Capacidad = int.Parse(dr["Capacidad"].ToString());
            Kilometraje = double.Parse(dr["Kilometraje"].ToString());
            Disponiblidad = bool.Parse(dr["Disponiblidad"].ToString());
            UrlFoto = dr["UrlFoto"].ToString();
        }
        #endregion
    }
}