using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public static class ClienteLogica
    {
        public static List<ClienteEntidad> GetClientes(ClienteEntidad cliente)
        {
            return ClienteDatos.GetClientes(cliente);
        }
        public static void MantCliente(ClienteEntidad cliente)
        {
            ClienteDatos.MantCliente(cliente);
        }
    }
}
