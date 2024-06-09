using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private Button generateButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button targetButton;

    public Action OnGenerateButtonClicked;
    public Action OnMoveButtonClicked;
    public Action OnAttackButtonClicked;
    public Action OnTargetButtonClicked;
    
    
    private void Awake()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        generateButton.onClick.AddListener(GenerateButtonClick);
        moveButton.onClick.AddListener(MoveButtonClick);
        attackButton.onClick.AddListener(AttackButtonClick);
        targetButton.onClick.AddListener(TargetButtonClick);
    }

    private void GenerateButtonClick()
    {
        OnGenerateButtonClicked?.Invoke();
        generateButton.interactable = false;
    }

    private void MoveButtonClick()
    {
        OnMoveButtonClicked?.Invoke();
        moveButton.interactable = false;
    }

    private void AttackButtonClick()
    {
        OnAttackButtonClicked?.Invoke();
        attackButton.interactable = false;
    }

    private void TargetButtonClick()
    {
        OnTargetButtonClicked?.Invoke();
        targetButton.interactable = false;
    }
}
