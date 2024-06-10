using System;
using System.Collections.Generic;
using CodeBase.GlobalData;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private List<TMP_Text> textElements;
        [SerializeField] private float movingSpeed;
        [SerializeField] private float attackingSpeed;

        private Vector3 _currentMovementVector;

        private GameZoneController _gameZoneController;

        private bool _isDirection;

        private CubeMode _mode = CubeMode.Moving;

        private Cube _nearestCube;

        public int Number { get; private set; }
        public bool IsMoved { get; private set; }

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

            if (other.transform.TryGetComponent<Cube>(out var cube))
                if (cube == _nearestCube)
                {
                    Destroy(other.gameObject);
                    _nearestCube = null;


                    AttackNextCube().Forget();
                }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag(GameConstants.FLOOR_TAG))
                return;

            GenerateMovementVector();
        }

        public void Init(GameZoneController gameZoneController)
        {
            _gameZoneController = gameZoneController;
        }

        private async UniTask AttackNextCube()
        {
            var cubes = await _gameZoneController.GetAllCubesAsync();

            if (cubes.Count == 1)
            {
                StopMovement();
                return;
            }

            await FindNearestCube();
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

        public void StopMovement()
        {
            IsMoved = false;
        }


        public async UniTask SetCubeMain()
        {
            transform.localScale *= 2;
            await FindNearestCube();

            _mode = CubeMode.Attacking;
        }

        private async UniTask FindNearestCube()
        {
            var cubes = await _gameZoneController.GetAllCubesAsync();
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