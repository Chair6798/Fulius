using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Fulius
{
    internal class Controller : MonoBehaviour
    {
        internal static Controller instance;
        internal static Light cameraLight;
        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            if (PlayerController.instance != null)
            {
                PlayerController.instance.DebugEnergy = Funcs.Yourself.InfinityStamina;
                if (Funcs.Yourself.InfinityStamina)
                {
                    PlayerController.instance.EnergyCurrent = PlayerController.instance.EnergyStart;
                }
            }
            if (CameraPosition.instance != null)
            {
                if (cameraLight == null)
                {
                    cameraLight = CameraPosition.instance.AddComponent<Light>();
                    cameraLight.range = 100;
                    cameraLight.intensity = 2;
                }
                cameraLight.enabled = Funcs.World.Fullbright;
            }
        }
    }
}
