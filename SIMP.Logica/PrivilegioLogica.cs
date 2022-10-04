using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class PrivilegioLogica
    {
        PrivilegioDatos obPrivilegiosD = new PrivilegioDatos();

        #region  METODOS DE MANTENIMIENTO 
        public string InsertarPermisos(PrivilegioEntidad privilegio)
        {
            try
            {
                return obPrivilegiosD.InsertarPermisos(privilegio);
            }
            catch (Exception ax)
            {
                throw;
            }

        }
        #endregion  METODOS DE MANTENIMIENTO 
    }
}
