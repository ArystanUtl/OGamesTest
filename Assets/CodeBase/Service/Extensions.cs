using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Service
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> input)
        {
            return input == null || !input.Any();
        }

        public static T GetRandomElement<T>(this IEnumerable<T> input)
        {
            if (input.IsNullOrEmpty())
                return default;

            var randomIndex = Random.Range(0, input.Count());

            var list = new List<T>(input);
            return list[randomIndex];
        }
    }
}