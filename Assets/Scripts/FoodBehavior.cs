using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    public int Value;
    public TextMeshPro PointsText;

    void Start()
    {
        PointsText.SetText(Value.ToString());
    }
}
