using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class ClienteLogica
    {
        ClienteDatos clienteDatos = new ClienteDatos();
        public List<ClienteEntidad> GetClientes(ClienteEntidad cliente)
        {
            return clienteDatos.GetClientes(cliente);
        }
        public void MantCliente(ClienteEntidad cliente)
        {
            clienteDatos.MantCliente(cliente);
        }
    }
}
