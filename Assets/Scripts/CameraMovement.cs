using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z - 6;
        transform.position = transformPosition;
    }
}
