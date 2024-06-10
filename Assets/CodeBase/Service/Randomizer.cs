using UnityEngine;
using static CodeBase.GlobalData.GameConstants;
using static UnityEngine.Random;

namespace CodeBase.Service
{
    public static class Randomizer
    {
        public static Vector3 GetRandomPosition()
        {
            var posX = Range(MIN_CUBE_POSITION, MAX_CUBE_POSITION);
            var posY = Range(MIN_CUBE_POSITION, MAX_CUBE_POSITION);
            var posZ = Range(MIN_CUBE_POSITION, MAX_CUBE_POSITION);

            var result = new Vector3(posX, posY, posZ);

            return result;
        }

        public static Color GetRandomColor()
        {
            var red = Range(0f, 1f);
            var green = Range(0f, 1f);
            var blue = Range(0f, 1f);

            return new Color(red, green, blue);
        }
    }
}