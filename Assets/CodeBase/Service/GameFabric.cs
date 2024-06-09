using UnityEngine;

namespace CodeBase.Service
{
    public class GameFabric: MonoBehaviour
    {
        [SerializeField] private Cube cubePrefab;


        public Cube CreateCube(Vector3 position)
        {
            var cube = Instantiate(cubePrefab, position, Quaternion.identity);
            return cube;
        }
        
        
    }
}