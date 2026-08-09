using Fulius.Libs;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Fulius.Libs
{
    internal static class LocalPlayer
    {
        internal static void Teleport(Vector3 position)
        {
            PlayerController c = PlayerController.instance;
            if (c!=null)
            {
                c.rb.position = position;
            }
        }
        internal static bool ControllerObjectActive()
        {
            return PlayerController.instance != null&&PlayerController.instance.gameObject.activeSelf;
        }
        internal static PlayerController GetController()
        {
            return PlayerController.instance;
        }
        internal static void SetControllerActive(bool active)
        {
            PlayerController c = GetController();
            if(c!=null)
            {
                c.enabled = active;
            }
        }
        internal static void SetKinematic(bool kinematic)
        {
            PlayerController c = GetController();
            if (c != null)
            {
                c.rb.isKinematic = kinematic;
            }
        }
    }
}
