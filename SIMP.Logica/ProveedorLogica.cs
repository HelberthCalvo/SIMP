using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZonaFranca.Entidades;
using ZonaFRanca.Datos;

namespace ZonaFranca.Logica
{
    public class ProveedorLogica
    {
        public List<Proveedores> SeleccionarProveedores(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedores(a);
        }
        public List<Proveedores> SeleccionarProveedores2(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedores2(a);
        }
        public List<Proveedores> SeleccionarProveedores1(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedores1(a);
        }
        public List<Proveedores> SeleccionarProveedoresDepartamento(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedoresDepartamento(a);
        }

        public List<Proveedores> SeleccionarProveedoresDepartamento1(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedoresDepartamento1(a);
        }
        public void InsertarDepartamentoProveedor(Proveedores a)
        {
            ProveedorDatos.InsertarDepartamentoProveedor(a);
        }
        public void EliminarDepartamentoProveedor(Proveedores a)
        {
            ProveedorDatos.EliminarDepartamentoProveedor(a);
        }

        public List<Proveedores> SeleccionarProveedoresArticulo(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedoresArticulo(a);
        }
        public void InsertarProveedorArticulo(Proveedores a)
        {
            ProveedorDatos.InsertarProveedorArticulo(a);
        }
        public void EliminarProveedorArticulo(Proveedores a)
        {
            ProveedorDatos.EliminarProveedorArticulo(a);
        }

        public List<Proveedores> SeleccionarArticulos(Proveedores a)
        {
            return ProveedorDatos.SeleccionarArticulos(a);
        }
        public List<Proveedores> SeleccionarArticulos1(Proveedores a)
        {
            return ProveedorDatos.SeleccionarArticulos1(a);
        }

        public List<Proveedores> SeleccionarDatosProveedorSP()
        {
            return ProveedorDatos.SeleccionarDatosProveedorSP();
        }

        public Proveedores SeleccionarDatosProveedorSPFiltro(string proveedor)
        {
            return ProveedorDatos.SeleccionarDatosProveedorSPFiltro(proveedor);
        }

        public List<Proveedores> SeleccionarProveedoresArticulo1(Proveedores a)
        {
            return ProveedorDatos.SeleccionarProveedoresArticulo1(a);
        }
    }
}
