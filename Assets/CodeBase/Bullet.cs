using UnityEngine;

namespace CodeBase
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed;

        private Cube _target;
        private Vector3 _targetPos;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.TryGetComponent<Cube>(out var cube))
                return;

            if (cube.Number == _target.Number)
                cube.StopMovement();
        }

        public void SetTarget(Cube target)
        {
            _target = target;

            // _targetPos = _target.transform.position;
            _targetPos = _target.transform.localPosition;

            Attack();
        }

        private void Attack()
        {
            var attackTarget = _targetPos * speed;

            rigidbody.AddForce(attackTarget);
        }
    }
}