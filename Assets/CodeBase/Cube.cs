using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;


    public void SetColor(Color color)
    {
        if (!meshRenderer)
            return;
        
        if (meshRenderer.material)
            meshRenderer.material.color = color;
    }
}