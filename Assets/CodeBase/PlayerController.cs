using System;
using System.Threading;
using CodeBase;
using CodeBase.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ButtonsController buttonsController;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameFabric gameFabric;
    [SerializeField] private GameZoneController gameZoneController;
    [SerializeField] private TextMesh numberText;
    [SerializeField] private Transform bulletContainer;

    private readonly int _attackDelay = 2;


    private CancellationTokenSource _attackCts;
    private Cube _currentTarget;

    private void Awake()
    {
        buttonsController.OnAttackButtonClicked += StartAttackMode;
        _attackCts = new CancellationTokenSource();
    }

    private void StartAttackMode()
    {
        PrepareTarget();

        StartAttackingRecursive().Forget();
    }

    private async UniTask StartAttackingRecursive()
    {
        AttackTarget();

        await UniTask.Delay(TimeSpan.FromSeconds(_attackDelay), cancellationToken: _attackCts.Token);

        StartAttackingRecursive().Forget();
    }

    private void AttackTarget()
    {
        var bullet = gameFabric.CreateBullet(bulletSpawn.transform.position, bulletContainer);
        bullet.SetTarget(_currentTarget);
        bullet.OnHitTarget += ChangeTarget;
    }

    private void ChangeTarget()
    {
        PrepareTarget();
    }

    private void PrepareTarget()
    {
        var cubes = gameZoneController.ActiveCubes;

        if (cubes.Count == 0)
        {
            StopAttackingMode();
            return;
        }

        var randomIndex = Random.Range(0, cubes.Count);

        var randomCube = cubes[randomIndex];
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
        _attackCts?.Cancel();
        _attackCts = new CancellationTokenSource();

        numberText.text = "Stop attacking";
    }
}