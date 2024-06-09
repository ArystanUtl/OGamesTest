using UnityEngine;
using static CodeBase.GlobalData.GameConstants;
using Random = System.Random;

namespace CodeBase.Service
{
    public static class Randomizer
    {
        private static readonly Random Rnd = new();

        public static Vector3 GetRandomPosition()
        {
            var posX = Rnd.Next(MIN_CUBE_POSITION, MAX_CUBE_POSITION);
            var posY = Rnd.Next(MIN_CUBE_POSITION, MAX_CUBE_POSITION);
            var posZ = Rnd.Next(MIN_CUBE_POSITION, MAX_CUBE_POSITION);

            var result = new Vector3(posX, posY, posZ);

            return result;
        }

        public static Color GetRandomColor()
        {
            var red = UnityEngine.Random.Range(0f, 1f);
            var green = UnityEngine.Random.Range(0f, 1f);
            var blue = UnityEngine.Random.Range(0f, 1f);

            return new Color(red, green, blue);
        }
    }
}