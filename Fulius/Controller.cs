using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal class Controller : MonoBehaviour
    {
        internal static Controller instance;
        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            if (PlayerController.instance != null)
            {
                PlayerController.instance.DebugEnergy = Funcs.Yourself.InfinityStamina;
                if(Funcs.Yourself.InfinityStamina)
                {
                    PlayerController.instance.EnergyCurrent = PlayerController.instance.EnergyStart;
                }
            }
            
        }
    }
}
