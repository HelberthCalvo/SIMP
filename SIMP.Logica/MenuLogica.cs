using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class MenuLogica
    {
        MenuDatos obMenuD = new MenuDatos();

        #region  METODOS DE OBTENER 
        public List<MenuEntidad> ObtenerMenu(MenuEntidad pMenu)
        {
            try
            {
                return obMenuD.ObtenerMenu(pMenu);

            }
            catch (Exception ax)
            {
                throw;
            }
        }
        #endregion  METODOS DE OBTENER 
    }
}
