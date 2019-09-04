using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// se agrego esta libreria no se para que pero se agrego!
using System.ComponentModel;

namespace _3_Capas
{
    public enum Marca
    {
        Dina,
        Kenworth,
        Volvo,
        Ford,
        //en caso de que sea una palabra de mas de 1 se tiene que agregar esta despcripcion y lo que el usuario ve es mercedes
        [Description("Mercedes benz")]Mercedes
    }

    public enum Tipo
    {
        Trailer,
        Torton,
        [Description("Doble remolque")]Doble_Remolque
            ,Volteo,
            [Description("Semi remolque")]Semi_remolque,
            Redillas
    }
    public enum EstatuC
    {
        [Description("Mercancia OK")]Ok=1,
        [Description("Mercancia Dañada")] Denado=2,
        [Description("MErcancia Extraviada")] Extraviado = 3
    }
}