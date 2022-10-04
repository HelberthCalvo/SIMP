using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMP.Logica.UTILITIES
{
    public class TreeHelpers
    {
        public static IEnumerable<TItem> GetAncestors<TItem>(TItem item, Func<TItem, TItem> getParentFunc)
        {
            if (getParentFunc == null)
            {
                throw new ArgumentNullException("getParentFunc");
            }
            if (ReferenceEquals(item, null)) yield break;
            for (TItem curItem = getParentFunc(item); !ReferenceEquals(curItem, null); curItem = getParentFunc(curItem))
            {
                yield return curItem;
            }
        }

        public static IEnumerable<TItem> obtenerDesendencia<TItem>(TItem item, Func<TItem, IEnumerable<TItem>> getChilds)
        {
            if (getChilds == null)
            {
                throw new ArgumentNullException("getChilds");
            }
            if (ReferenceEquals(item, null)) yield break;

            IEnumerable<TItem> listaHijos = getChilds(item);

            if (listaHijos.Count() > 0)
            {
                foreach (TItem objeto in listaHijos)
                {
                    foreach (var x in obtenerDesendencia(objeto, getChilds))
                    {
                        yield return x;
                    }
                }
            }
            else
            {
                yield return item;
            }

        }
    }
}
