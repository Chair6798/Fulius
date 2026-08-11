using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Libs
{
    internal static class GameCamera
    {
        internal static Vector3 position
        {
            get
            {
                if(!CameraPositionAvaiable())
                {
                    Logger.Warn("Camera position unavaiable to get! return zero");
                    return Vector3.zero;
                }
                return GetPosition();
            }
            set
            {
                SetPosition(value);
            }
        }
        internal static bool PositionActive()
        {
            return CameraPosition.instance.enabled;
        }
        internal static void SetPositionActive(bool active)
        {
            if(CameraPosition.instance)
            CameraPosition.instance.enabled = active;
        }
        internal static CameraPosition GetCameraPositionComponent()
        {
            return CameraPosition.instance;
        }
        internal static GameObject GetCameraPositionObject()
        {
            return GetCameraPositionComponent().gameObject;
        }
        static Vector3 GetPosition()
        {
            return GetCameraPositionObject().transform.position;
        }
        internal static void SetPosition(Vector3 position)
        {
            if (!CameraPositionAvaiable())
            {
                Logger.Warn("Unable to set position! Component not found! Ignoring!");
                return;
            }
            if (PositionActive())
            {
                Logger.Warn("Unable to set position! Component active! Ignoring!");
                return;
            }
            GetCameraPositionObject().transform.position = position;
        }
        internal static bool CameraPositionAvaiable()
        {
            return CameraPosition.instance != null;
        }
        /////////////////////////////////////////////////////////////////////////
        internal static Quaternion rotation
        {
            get
            {
                if (!CameraAimAvaiable())
                {
                    Logger.Warn("Camera Rotation unavaiable to get! return identity");
                    return Quaternion.identity;
                }
                return GetRotation();
            }
            set
            {
                SetRotation(value);
            }
        }

        internal static Vector3 forward
        {
            get
            {
                if(!CameraAimAvaiable())
                {
                    return Vector3.forward;
                }
                return GetCameraAimObject().transform.forward;
            }
        }
        internal static Vector3 right
        {
            get
            {
                if (!CameraAimAvaiable())
                {
                    return Vector3.right;
                }
                return GetCameraAimObject().transform.right;
            }
        }

        internal static bool RotationActive()
        {
            return CameraAim.Instance.enabled;
        }
        internal static void SetAimActive(bool active)
        {
            if (CameraAim.Instance)
                CameraAim.Instance.enabled = active;
        }
        internal static CameraAim GetCameraAimComponent()
        {
            return CameraAim.Instance;
        }
        internal static GameObject GetCameraAimObject()
        {
            return GetCameraAimComponent().gameObject;
        }
        internal static Quaternion GetRotation()
        {
            return CameraAimAvaiable() ? GetCameraAimObject().transform.rotation : Quaternion.identity;
        }
        internal static void SetRotation(Quaternion rotation)
        {
            if (!CameraAimAvaiable())
            {
                Logger.Warn("Unable to set Rotation! Component not found! Ignoring!");
                return;
            }
            if (RotationActive())
            {
                Logger.Warn("Unable to set Rotation! Component active! Ignoring!");
                return;
            }
            GetCameraAimObject().transform.rotation = rotation;
        }
        internal static bool CameraAimAvaiable()
        {
            return CameraAim.Instance != null;
        }
        
    }
}
