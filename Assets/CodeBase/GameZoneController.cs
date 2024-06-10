using System.Collections.Generic;
using System.Linq;
using CodeBase.GlobalData;
using CodeBase.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase
{
    public class GameZoneController : MonoBehaviour
    {
        [SerializeField] private ButtonsController buttonsController;
        [SerializeField] private GameFabric gameFabric;
        [SerializeField] private Transform cubeContainer;


        private List<Cube> _cubes = new();

        public List<Cube> ActiveCubes
        {
            get
            {
                var cubes = _cubes.Where(x => x && x.gameObject && x.IsMoved).ToList();
                return cubes;
            }
        }

        private void Awake()
        {
            buttonsController.OnGenerateButtonClicked += GenerateCubes;
            buttonsController.OnMoveButtonClicked += StartMovingCubes;
            buttonsController.OnTargetButtonClicked += StartAttackCubes;
        }

        public async UniTask<List<Cube>> GetAllCubesAsync()
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
            _cubes = _cubes.Where(x => x != null && x.gameObject != null).ToList();
            return _cubes;
        }

        private void StartAttackCubes()
        {
            StartMovingCubes();

            var randomIndex = Random.Range(0, _cubes.Count);
            //randomIndex = Math.Clamp(randomIndex, 0, _cubes.Count - 1);

            var randomCube = _cubes[randomIndex];

            randomCube.SetCubeMain().Forget();
        }

        private void StartMovingCubes()
        {
            foreach (var cube in _cubes)
                cube.StartMoving();
        }

        private void GenerateCubes()
        {
            var count = Random.Range(GameConstants.MIN_CUBE_COUNT, GameConstants.MAX_CUBE_COUNT);

            for (var i = 0; i < count; i++)
            {
                var pos = Randomizer.GetRandomPosition();
                var color = Randomizer.GetRandomColor();

                var cube = gameFabric.CreateCube(pos, cubeContainer);
                cube.Init(this);
                cube.SetColor(color);

                var number = i + 1;
                cube.SetNumber(number);

                _cubes.Add(cube);
            }
        }
    }
}