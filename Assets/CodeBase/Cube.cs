using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CodeBase;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private List<TMP_Text> textElements;
    [SerializeField] private float movingSpeed;

    private bool _isMove;

    public void StartMoving()
    {
        _isMove = true;

        var rnd = Random.insideUnitSphere.normalized;
        rnd = new Vector3(rnd.x, 0f, rnd.z);
        
        rigidBody.velocity = rnd * movingSpeed;
    }


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