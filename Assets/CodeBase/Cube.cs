using System.Collections.Generic;
using CodeBase;
using CodeBase.GlobalData;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private List<TMP_Text> textElements;
    [SerializeField] private float movingSpeed;

    private Vector3 _currentMovementVector;
    private bool _isDirection;

    private bool _isMove;

    private void Update()
    {
        if (!_isMove)
            return;

        var moveDirection = _currentMovementVector * movingSpeed;
        transform.Translate(moveDirection);
        
        
        // rigidBody.velocity = moveDirection;
        //rigidBody.AddForce(moveDirection, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(GameConstants.FLOOR_TAG))
            return;
        
        Debug.Log($"OnCollision: {other.gameObject.name}");
        
        GenerateMovementVector();
    }

    public void StartMoving()
    {
        _isMove = true;
        GenerateMovementVector();
    }

    private void GenerateMovementVector()
    {
        _currentMovementVector = Random.insideUnitSphere.normalized;
        _currentMovementVector = new Vector3(_currentMovementVector.x, 0f, _currentMovementVector.z);
        
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