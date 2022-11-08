using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Entidades
{

    public class GanttValues
    {
        public string from { get; set; }
        public string to { get; set; }
        public string label { get; set; }
        public string customClass { get; set; }
        public string[] dataObj { get; set; }
        public string desc { get; set; }
    }

    public class GanttEntidad
    {
        public string name { get; set; }
        public string desc { get; set; }
        public List<GanttValues> values { get; set; }
    }
}
