using Fulius.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Fulius
{
    internal class FreeCameraController : MonoBehaviour
    {
        internal static FreeCameraController instance;
        internal static float rotX = 0;
        internal static float rotY = 0;
        internal static bool wasEnabled = false;
        void Awake(){instance = this;}
        void Update()
        {
            if(!wasEnabled && Funcs.World.FreeCamera)
            {
                rotX = 0;
                rotY = 0;
            }
            wasEnabled = Funcs.World.FreeCamera;
            if(!Funcs.World.FreeCamera)
            {
                return;
            }
            if(Chat.IsOpen())
            {
                return;
            }
            //rotation
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            rotY += x * Config.cameraSens.Value;
            rotX -= y * Config.cameraSens.Value;

            rotX = math.clamp(rotX, -90f, 90f);

            GameCamera.rotation = Quaternion.Euler(rotX, rotY, 0);

            var ct = CameraAim.Instance.transform;

            Vector3 move = Vector3.zero + ((Input.GetKey(Config.cameraForward.Value)) ? (ct.forward) : Vector3.zero) + ((Input.GetKey(Config.cameraBackward.Value)) ? (-ct.forward) : Vector3.zero) + ((Input.GetKey(Config.cameraRight.Value)) ? (ct.right) : Vector3.zero) + ((Input.GetKey(Config.cameraLeft.Value)) ? (-ct.right) : Vector3.zero) + ((Input.GetKey(Config.cameraUp.Value)) ? (Vector3.up) : Vector3.zero) + ((Input.GetKey(Config.cameraDown.Value)) ? (-Vector3.up) : Vector3.zero);

            GameCamera.position += move * Config.cameraSpeed.Value * (Input.GetKey(Config.cameraFaster.Value)?2:1);
        }
    }
}
