using Fulius.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius
{
    internal class FreeCameraController : MonoBehaviour
    {
        internal static FreeCameraController instance;
        void Awake(){instance = this;}
        void Update()
        {
            if(!Funcs.World.FreeCamera)
            {
                return;
            }
            //rotation
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector3 euler = GameCamera.rotation.eulerAngles;
            euler.x += x / 100 * Config.cameraSpeed.Value;
        }
    }
}
