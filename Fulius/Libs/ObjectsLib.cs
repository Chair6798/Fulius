using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Fulius.Libs
{
    internal static class Objects
    {

        internal static bool GetObject(string type, ref GameObject obj)
        {
            switch(type)
            {
                case "truck":
                    if(TruckSafetySpawnPoint.instance == null)
                    {
                        return false;
                    }
                    obj = TruckSafetySpawnPoint.instance.gameObject;
                    return true;
                case "extraction":
                    if(RoundDirector.instance== null)
                    {
                        return false;
                    }
                    var points = ((List<GameObject>)Reflection.GetValue(RoundDirector.instance, "extractionPointList"));
                    if (points == null||points.Count<=0)
                    {
                        return false;
                    }
                    ExtractionPoint current = ((ExtractionPoint)Reflection.GetValue(RoundDirector.instance, "extractionPointCurrent"));
                    if(current == null)
                    {
                        foreach(var point in points)
                        {
                            var state = ((ExtractionPoint.State)Reflection.GetValue(point.GetComponent<ExtractionPoint>(), "currentState"));
                            if (state==ExtractionPoint.State.Complete||state==ExtractionPoint.State.Extracting)
                            {
                                continue;
                            }
                            current = point.GetComponent<ExtractionPoint>();
                            break;
                        }
                    }
                    if(current==null)
                    {
                        return false;
                    }else
                    {
                        obj = current.gameObject;
                        return true;
                    }
            }
            return false;
        }
    }
}
