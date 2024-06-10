using System;
using CodeBase.CubeModules;
using UnityEngine;

namespace CodeBase.BulletModules
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float speed;
       
        public Action OnHitTarget;

        private bool _isAttack;
        private Cube _target;
        
        private void Update()
        {
            if (!_isAttack)
                return;

            var targetPos = _target.transform.position;
            var direction = targetPos - transform.position;
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.TryGetComponent<Cube>(out var cube))
                return;

            if (cube.Number != _target.Number)
                return;
            
            StopAttack();
            
            cube.StopMoving();
            OnHitTarget?.Invoke();
            
            Destroy(gameObject);
        }

        public void SetTarget(Cube target)
        {
            _target = target;
            StartAttack();
        }

        private void StartAttack()
        {
            _isAttack = true;
        }

        private void StopAttack()
        {
            _isAttack = false;
        }
    }
}