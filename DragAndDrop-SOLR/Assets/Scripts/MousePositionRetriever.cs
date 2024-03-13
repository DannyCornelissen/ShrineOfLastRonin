using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MousePositionRetriever : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    private Vector3 lastPosition;
    [SerializeField] private LayerMask placementLayerMask;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
