using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this IEnumerable<Object> input)
        {
            return input == null || !input.Any();
        }
    }
}