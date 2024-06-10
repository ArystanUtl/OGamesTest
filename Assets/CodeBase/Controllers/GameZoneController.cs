using System.Collections.Generic;
using System.Linq;
using CodeBase.CubeModules;
using CodeBase.GlobalData;
using CodeBase.Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Controllers
{
    public class GameZoneController : MonoBehaviour
    {
        [SerializeField] private ButtonsController buttonsController;
        [SerializeField] private GameFabric gameFabric;
        [SerializeField] private Transform cubeContainer;
        [SerializeField] private GameObject floor;

        public List<Cube> AllCubes { get; } = new();

        public List<Cube> MovingCubes
        {
            get
            {
                var cubes = AllCubes.Where(x => x.IsMoved).ToList();
                return cubes;
            }
        }

        public bool IsFloor(GameObject go)
        {
            return go == floor;
        }

        private void Awake()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            buttonsController.OnGenerateButtonClicked += GenerateCubes;
            buttonsController.OnMoveButtonClicked += StartMovingCubes;
            buttonsController.OnCubeAttackModeButtonClicked += StartCubeAttackerMode;
        }

        public void RemoveCube(Cube cube)
        {
            AllCubes.Remove(cube);
        }

        private void StartCubeAttackerMode()
        {
            StartMovingCubes();

            var randomIndex = Random.Range(0, AllCubes.Count);
            var randomCube = AllCubes[randomIndex];

            randomCube.ChangeCubeToAttacker();
        }

        private void StartMovingCubes()
        {
            foreach (var cube in AllCubes)
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

                AllCubes.Add(cube);
            }
        }
    }
}