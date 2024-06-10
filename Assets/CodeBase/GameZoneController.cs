using System.Collections.Generic;
using CodeBase.GlobalData;
using CodeBase.Service;
using UnityEngine;

namespace CodeBase
{
    public class GameZoneController : MonoBehaviour
    {
        [SerializeField] private ButtonsController buttonsController;
        [SerializeField] private GameFabric gameFabric;
        [SerializeField] private Transform cubeContainer;


        private readonly List<Cube> _cubes = new();
        public List<Cube> Cubes => _cubes;

        private void Awake()
        {
            buttonsController.OnGenerateButtonClicked += GenerateCubes;
            buttonsController.OnMoveButtonClicked += StartMovingCubes;
        }
        
        
        private void StartMovingCubes()
        {
            foreach (var cube in _cubes)
            {
                cube.StartMoving();
            }
        }

        private void GenerateCubes()
        {
            var count = Random.Range(GameConstants.MIN_CUBE_COUNT, GameConstants.MAX_CUBE_COUNT);

            for (var i = 0; i < count; i++)
            {
                var pos = Randomizer.GetRandomPosition();
                var color = Randomizer.GetRandomColor();

                var cube = gameFabric.CreateCube(pos, cubeContainer);
                cube.SetColor(color);

                var number = i + 1;
                cube.SetNumber(number);

                _cubes.Add(cube);
            }
        }
    }
}