using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private Button generateButton;
        [SerializeField] private Button moveButton;
        [SerializeField] private Button playerAttackButton;
        [SerializeField] private Button cubeAttackModeButton;
    
        public Action OnCubeAttackModeButtonClicked;
        public Action OnGenerateButtonClicked;
        public Action OnMoveButtonClicked;
        public Action OnPlayerAttackButtonClicked;

        private void Awake()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            generateButton.onClick.AddListener(GenerateButtonClick);
            moveButton.onClick.AddListener(MoveButtonClick);
            playerAttackButton.onClick.AddListener(AttackButtonClick);
            cubeAttackModeButton.onClick.AddListener(CubeAttackModeClick);
        }

        private void GenerateButtonClick()
        {
            OnGenerateButtonClicked?.Invoke();
            DeactivateButton(generateButton);
        }

        private void DeactivateButton(Selectable button)
        {
            button.interactable = false;
        }

        private void MoveButtonClick()
        {
            OnMoveButtonClicked?.Invoke();
            DeactivateButton(moveButton);
        }

        private void AttackButtonClick()
        {
            OnPlayerAttackButtonClicked?.Invoke();
            DeactivateButton(playerAttackButton);
        }

        private void CubeAttackModeClick()
        {
            OnCubeAttackModeButtonClicked?.Invoke();
            DeactivateButton(cubeAttackModeButton);
        }
    }
}