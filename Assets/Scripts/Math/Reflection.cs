using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    public Transform cam;

    private Transform _origin;
    
    private void OnDrawGizmos()
    {
        _origin = cam.transform;
        Ray ray = new Ray(_origin.position, Vector3.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            Debug.DrawRay(_origin.position, hit.point- _origin.position);
        }
        else
        {
            Debug.DrawRay(_origin.position, Vector3.forward * 100f);
        }
    }
}
