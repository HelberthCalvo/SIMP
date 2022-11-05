using System;
using SIMP.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMP.Entidades;

namespace SIMP.Logica
{
    public class ConfiguracionLogica
    {
        private ConfiguracionDatos _configuracionDatos { get; set; }
        public ConfiguracionLogica() => _configuracionDatos = new ConfiguracionDatos();

        public List<ConfiguracionEntidad> ObtieneConfiguraciones()
        {
            return _configuracionDatos.GetConfiguracion("hcalvo", 0, "1", "secure", 0);
        }

        public void MantConfiguracionEstado(ConfiguracionEntidad config)
        {
            _configuracionDatos.MantConfiguracionEstado(config);
        }
    }
}
