using System;
using System.Collections.Generic;
using CodeBase.Controllers;
using CodeBase.GlobalData;
using CodeBase.Service;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.CubeModules
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private List<TMP_Text> textElements;
        
        [SerializeField] private float movingSpeed;
        [SerializeField] private float attackingSpeed;

        public int Number { get; private set; }
        public bool IsMoved { get; private set; }
        
        
        private GameZoneController _gameZoneController;

        private Vector3 _currentMovementVector;
        private CubeMode _mode = CubeMode.Moving;

        private Cube _nearestCube;

        public void Init(GameZoneController gameZoneController)
        {
            _gameZoneController = gameZoneController;
        }

        private void Update()
        {
            switch (_mode)
            {
                case CubeMode.Moving:
                {
                    if (!IsMoved)
                        return;

                    var moveDirection = _currentMovementVector * movingSpeed;
                    transform.Translate(moveDirection);
                    break;
                }

                case CubeMode.Attacking:
                {
                    if (_nearestCube is null || _nearestCube == this)
                        return;

                    var targetPos = _nearestCube.transform.position;

                    var direction = targetPos - transform.position;
                    direction.Normalize();

                    transform.Translate(direction * attackingSpeed * Time.deltaTime, Space.World);

                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void OnCollisionEnter(Collision other)
        {
            if (_mode is CubeMode.Moving)
                return;

            if (!other.transform.TryGetComponent<Cube>(out var cube))
                return;

            if (cube != _nearestCube)
                return;
            
            _gameZoneController.RemoveCube(cube);
            
            Destroy(other.gameObject);
            _nearestCube = null;
            
            AttackNextCube();
        }

        private void OnCollisionStay(Collision other)
        {
            if (_gameZoneController.IsFloor(other.gameObject))
                return;

            GenerateMovementVector();
        }

        private void AttackNextCube()
        {
            var cubes =  _gameZoneController.AllCubes;

            if (cubes.Count == 1)
            {
                StopMoving();
                return;
            }

            FindNearestCube();
        }

        public void StartMoving()
        {
            IsMoved = true;
            _mode = CubeMode.Moving;
            GenerateMovementVector();
        }

        private void GenerateMovementVector()
        {
            _currentMovementVector = Random.insideUnitSphere.normalized;
            _currentMovementVector = new Vector3(_currentMovementVector.x, 0f, _currentMovementVector.z);
        }

        public void SetNumber(int number)
        {
            Number = number;
            gameObject.name = $"Cube {number}";

            foreach (var textElement in textElements)
                textElement.text = number.ToString();
        }

        public void SetColor(Color color)
        {
            if (!meshRenderer)
                return;

            if (meshRenderer.material)
                meshRenderer.material.color = color;
        }

        public void StopMoving()
        {
            IsMoved = false;
        }

        public void ChangeCubeModeToAttacker()
        {
            IncreaseScale();
            
            FindNearestCube();

            _mode = CubeMode.Attacking;
        }

        private void IncreaseScale()
        {
            transform.localScale *= GameConstants.ATTACK_CUBE_INCREASE_COEFFICIENT;
        }

        private void FindNearestCube()
        {
            var cubes = _gameZoneController.AllCubes;
            if (cubes.IsNullOrEmpty())
                return;

            var minDistance = float.MaxValue;

            foreach (var cube in cubes)
            {
                if (cube == this)
                    continue;

                var currentDist = Vector3.Distance(transform.position, cube.transform.position);

                if (!(currentDist < minDistance))
                    continue;

                _nearestCube = cube;
                minDistance = currentDist;
            }
        }
    }
}