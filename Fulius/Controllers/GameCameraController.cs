using Fulius.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal class GameCameraController : MonoBehaviour
    {
        internal static GameCameraController instance;
        void Awake()
        {
            instance = this;
        }
        void Update()
        {
            GameCamera.SetPositionActive(!Funcs.World.FreeCamera);
            GameCamera.SetAimActive(!Funcs.World.FreeCamera);
        }
    }
}
