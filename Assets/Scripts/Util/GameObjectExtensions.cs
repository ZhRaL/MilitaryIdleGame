using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class GameObjectExtensions
    {
        public static List<Transform> GetAllChildren(this GameObject parent)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform child in parent.transform)
            {
                children.Add(child);
            }

            return children;
        }
    }
}