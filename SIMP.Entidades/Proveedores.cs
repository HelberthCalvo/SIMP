using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZonaFranca.Entidades
{
    public class Proveedores
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Departamento { get; set; }
        public string Articulo { get; set; }
        public string Num { get; set; }




        public string Nombre { get; set; }
        public string Incoterm { get; set; }
        public string DescDepartamento { get; set; }
        public string Comprador { get; set; }
        public string NomComprador { get; set; }
        public string Pais { get; set; }
        public string DescPais { get; set; }
        public int PaisDefault { get; set; }
        public string Moneda { get; set; }
        public string CondicionPago { get; set; }
        public string ModoTranporte { get; set; }
        public string LugarEntrega { get; set; }
        public string RentaTipo { get; set; }
        public string TipoOrden { get; set; }

        public int Duracion { get; set; }
        public string DescDuracion { get; set; }
        public string Tipo { get; set; }
        public string Local { get; set; }
    }
}
