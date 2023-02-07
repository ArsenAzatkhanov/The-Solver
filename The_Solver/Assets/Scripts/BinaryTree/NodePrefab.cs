using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePrefab : MonoBehaviour
{
    [SerializeField] TextMeshPro valueText, layerText;
    public Vector2 shiftValues;

    public void SetValues(int value, int layer)
    {
        layerText.text = "Layer: " + layer;
        valueText.text = "Value: " + value;
    }
}
