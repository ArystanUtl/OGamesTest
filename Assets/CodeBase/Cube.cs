using System.Collections.Generic;
using CodeBase;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private List<TMP_Text> textElements;

    public void SetText(string text)
    {
        if (!text.IsCorrect())
            return;

        foreach (var textElement in textElements)
            textElement.text = text;
    }

    public void SetColor(Color color)
    {
        if (!meshRenderer)
            return;

        if (meshRenderer.material)
            meshRenderer.material.color = color;
    }
}