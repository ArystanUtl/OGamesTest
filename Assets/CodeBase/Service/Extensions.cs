using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Service
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this IEnumerable<Object> input)
        {
            return input == null || !input.Any();
        }
    }
}