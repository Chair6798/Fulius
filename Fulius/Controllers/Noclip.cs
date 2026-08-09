using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Fulius;
using Fulius.Libs;
namespace Fulius.Noclip
{
    internal class NoclipController : MonoBehaviour
    {
        void Update()
        {
            if (!LocalPlayer.ControllerObjectActive())
            {
                return;
            }
            
            
            
            if (!Funcs.Yourself.Noclip)
            {
                return;
            }
            Transform t = PlayerController.instance.transform;
            Transform ct = CameraAim.Instance.transform;
            bool chat = !Chat.IsOpen();
            Vector3 move = Vector3.zero + ((Input.GetKey(Config.noclipForward.Value)) ? (ct.forward) : Vector3.zero) + ((Input.GetKey(Config.noclipBackward.Value)) ? (-ct.forward) : Vector3.zero) + ((Input.GetKey(Config.noclipRight.Value)) ? (t.right) : Vector3.zero) + ((Input.GetKey(Config.noclipLeft.Value)) ? (-t.right) : Vector3.zero) + ((Input.GetKey(Config.noclipUp.Value)) ? (t.up) : Vector3.zero) + ((Input.GetKey(Config.noclipDown.Value)) ? (-t.up) : Vector3.zero);
            Rigidbody rb = LocalPlayer.GetController().rb;
            rb.position += move * Config.noclipSpeed.Value * (Input.GetKey(Config.noclipFaster.Value)?2:1) * Time.deltaTime;
            rb.rotation = Quaternion.Euler(0,CameraAim.Instance.transform.rotation.eulerAngles.y,0);
        }
    }
}
