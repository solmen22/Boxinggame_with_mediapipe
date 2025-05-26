using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mediapipe.Unity;
using UnityEngine;


public class test : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private PointListAnnotation pointListAnnotation;  // Inspectorから設定

  void Update()
  {
    if (pointListAnnotation != null)
    {
      Vector2 dt = pointListAnnotation.aChildren[11].transform.position - pointListAnnotation.aChildren[23].transform.position;
      Debug.Log("rightpunch:" + Vector3.Distance(pointListAnnotation.aChildren[0].transform.position, pointListAnnotation.aChildren[16].transform.position));
      Debug.Log("leftpunch:" + Vector3.Distance(pointListAnnotation.aChildren[0].transform.position, pointListAnnotation.aChildren[15].transform.position));
      //70
      //60
      //Debug.Log("angle: " + Mathf.Atan2(dt.y, dt.x));
      //1.8, 1.55

      //1.65 ,1.45
    }
    else
    {
      Debug.LogError("PointListAnnotation reference not set!");
    }
  }
}
