using UnityEngine;

namespace CodeBase.Service
{
    public class GameFabric : MonoBehaviour
    {
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private Bullet bulletPrefab;

        public Cube CreateCube(Vector3 position, Transform parent)
        {
            var cube = Instantiate(cubePrefab, position, Quaternion.identity);
            cube.transform.SetParent(parent);
            return cube;
        }

        public Bullet CreateBullet(Vector3 position, Transform parent)
        {
            var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.transform.SetParent(parent);
            return bullet;
        }
    }
}