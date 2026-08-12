using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fulius.Libs
{
    internal static class Cosmetics
    {
        internal static Collection<CosmeticWorldObject> GetAll()
        {
            var coll = new Collection<CosmeticWorldObject>();
            foreach(CosmeticWorldObject obj in UnityEngine.Object.FindObjectsByType<CosmeticWorldObject>(UnityEngine.FindObjectsSortMode.None))
            {
                coll.Add(obj);
            }
            return coll;
        }
    }
}
