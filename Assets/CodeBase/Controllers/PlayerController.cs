using System;
using System.Threading;
using CodeBase.CubeModules;
using CodeBase.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ButtonsController buttonsController;
        [SerializeField] private GameZoneController gameZoneController;
        [SerializeField] private GameFabric gameFabric;

        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private TextMesh numberText;
        [SerializeField] private Transform bulletContainer;

        [SerializeField] private int attackDelay;

        private Cube _currentTarget;
        private CancellationTokenSource _attackCts = new();

        private void Awake()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            buttonsController.OnPlayerAttackButtonClicked += StartAttackMode;
        }

        private void StartAttackMode()
        {
            PrepareTarget();
            StartAttackingRecursive().Forget();
        }

        private async UniTask StartAttackingRecursive()
        {  
            await UniTask.Delay(TimeSpan.FromSeconds(attackDelay), cancellationToken: _attackCts.Token);
            
            AttackTarget();
            StartAttackingRecursive().Forget();
        }

        private void AttackTarget()
        {
            var bullet = gameFabric.CreateBullet(bulletSpawn.transform.position, bulletContainer);
            bullet.SetTarget(_currentTarget);
            bullet.OnHitTarget += PrepareTarget;
        }
    
        private void PrepareTarget()
        {
            var cubes = gameZoneController.MovingCubes;

            if (cubes.IsNullOrEmpty())
            {
                StopAttackingMode();
                return;
            }
            
            var randomCube = cubes.GetRandomElement();
            _currentTarget = randomCube;

            ShowTargetNumber();
        }

        private void ShowTargetNumber()
        {
            var number = _currentTarget.Number;
            numberText.text = number.ToString();
        }

        private void StopAttackingMode()
        {
            StopCancellationToken();

            numberText.text = "Stop attacking";
        }

        private void StopCancellationToken()
        {
            _attackCts?.Cancel();
            _attackCts = new CancellationTokenSource();
        }
    }
}