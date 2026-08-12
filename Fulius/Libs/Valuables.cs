using Photon.Voice;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Fulius.Libs
{
    internal static class Valuables
    {
        internal static Collection<ValuableObject> GetAll()
        {
            var coll = new Collection<ValuableObject>();
            foreach(ValuableObject obj in UnityEngine.Object.FindObjectsByType<ValuableObject>(UnityEngine.FindObjectsSortMode.None))
            {
                coll.Add(obj);
            }
            return coll;
        }
        internal static void BreakAll()
        {
            TeleportAll(new Vector3(0, -2000, 0), Quaternion.identity);
        }
        internal static void TeleportAll()
        {
            TeleportAll(GameCamera.position + GameCamera.forward*2, Quaternion.identity);
        }
        internal static void TeleportAll(Vector3 position, Quaternion rotation)
        {
            foreach (ValuableObject obj in GetAll())
            {
                obj.GetComponent<PhysGrabObject>().Teleport(position, rotation);
            }
        }
        internal static void TeleportAll(Vector3 position)
        {
            foreach (ValuableObject obj in GetAll())
            {
                obj.GetComponent<PhysGrabObject>().Teleport(position, obj.transform.rotation);
            }
        }
        internal static void TeleportAllToExtraction()
        {
            GameObject go = null;
            if(Objects.GetObject("extraction", ref go))
            {
                TeleportAll(go.transform.position + new Vector3(0,4,0), go.transform.rotation);
            }
            
        }
    }
}
