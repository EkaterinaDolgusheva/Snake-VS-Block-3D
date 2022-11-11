using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int Value;
    public TextMeshPro PointsText;
    Color lerpedColor = Color.white;

    void Start()
    {
        PointsText.SetText(Value.ToString());
        lerpedColor = Color.Lerp(Color.white, Color.red, (float)Value / 20f);
        this.GetComponent<Renderer>().material.color = lerpedColor;
    }
}
