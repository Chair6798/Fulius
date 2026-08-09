using Fulius.Libs;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using static ItemCartCannonMain;

namespace Fulius
{
    internal class LocalPlayerController : MonoBehaviour
    {
        internal static LocalPlayerController instance;
        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            var state = Funcs.Yourself.Noclip || Funcs.World.FreeCamera;
            LocalPlayer.SetControllerActive(!state);
            LocalPlayer.SetKinematic(state);
        }
    }
}