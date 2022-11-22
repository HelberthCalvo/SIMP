﻿using SIMP.Datos;
using SIMP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica
{
    public class BitacoraLogica
    {
        public static List<BitacoraEntidad> GetBitacoras(BitacoraEntidad bitacora)
        {
            return BitacoraDatos.GetBitacoras(bitacora);
        }
    }
}