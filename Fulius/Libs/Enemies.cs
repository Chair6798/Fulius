using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Fulius.Libs
{
    internal static class Enemies
    {
        internal static Collection<EnemyParent> GetAll()
        {
            var coll = new Collection<EnemyParent>();
            foreach (EnemyParent obj in UnityEngine.Object.FindObjectsByType<EnemyParent>(UnityEngine.FindObjectsSortMode.None))
            {
                coll.Add(obj);
            }
            return coll;
        }
    }
}
