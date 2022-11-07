using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform SnakeConteiner;
    Vector3 initialCameraPos;

    private void Start()
    {
        initialCameraPos = transform.position;
    }

    private void Update()
    {
        if (SnakeConteiner.childCount > 0)
        {
            transform.position = Vector3.Slerp (transform.position, (initialCameraPos + new Vector3(0, SnakeConteiner.GetChild(0).position.y - Camera.main.orthographicSize/2,0)), 0.1f);
        }
    }
}
