﻿using System;
using UnityEngine;

namespace CodeBase
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float speed;

        private bool _isAttack;

        private Cube _target;
        private Vector3 _targetPos;

        public Action OnHitTarget;


        private void Update()
        {
            if (!_isAttack)
                return;

            _targetPos = _target.transform.position;

            var direction = _targetPos - transform.position;
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.TryGetComponent<Cube>(out var cube))
                return;

            if (cube.Number == _target.Number)
            {
                Debug.Log("[Bullet] Target completed");
                cube.StopMovement();
                _isAttack = false;
                OnHitTarget?.Invoke();
                Destroy(gameObject);
            }
        }

        public void SetTarget(Cube target)
        {
            _target = target;

            _targetPos = _target.transform.position;
            Attack();
        }

        private void Attack()
        {
            _isAttack = true;
        }
    }
}