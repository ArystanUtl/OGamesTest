using System;
using System.Threading;
using CodeBase;
using CodeBase.Service;
using Cysharp.Threading.Tasks;
using TMPro;
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

        StartAttacking().Forget();
    }

    private async UniTask StartAttacking()
    {
        StartAttackAsync();
        await UniTask.Delay(TimeSpan.FromSeconds(_attackDelay), cancellationToken: _attackCts.Token);
        StartAttacking().Forget();
    }

    private void StartAttackAsync()
    {
        var bullet = gameFabric.CreateBullet(bulletSpawn.transform.position, bulletContainer);
        
        
        bullet.SetTarget(_currentTarget);
    }
    
    private readonly int _attackDelay = 2;
    
    private void PrepareTarget()
    {
        var cubes = gameZoneController.Cubes;

        var randomIndex = Random.Range(0, cubes.Count);

        var randomCube = cubes[randomIndex];
        _currentTarget = randomCube;
        
        var number = randomCube.Number;
        numberText.text = number.ToString();
    }


    public void StopAttackingMode()
    {
        _attackCts?.Cancel();
        _attackCts = new CancellationTokenSource();
    }
}
