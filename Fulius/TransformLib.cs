using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Libs
{
    internal static class TransformLib
    {
        internal static void Teleport(GameObject obj, Vector3 position)
        {
            PhysGrabObject pgo = obj.GetComponent<PhysGrabObject>();
            if (pgo==null)
            {
                return;
            }
            pgo.Teleport(position, obj.transform.rotation);
        }
        internal static void Teleport(GameObject obj, Quaternion rotation)
        {
            PhysGrabObject pgo = obj.GetComponent<PhysGrabObject>();
            if (pgo == null)
            {
                return;
            }
            pgo.Teleport(obj.transform.position, rotation);
        }
        internal static void Teleport(GameObject obj, Vector3 position, Quaternion rotation)
        {
            PhysGrabObject pgo = obj.GetComponent<PhysGrabObject>();
            if (pgo == null)
            {
                return;
            }
            pgo.Teleport(position, rotation);
        }
    }
}
