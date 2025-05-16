using System.Collections.Generic;
using Resources.Code.UI.DataStructures;
using UnityEngine;

namespace Resources.Code
{
    public class Container: MonoBehaviour
    {
        protected List<DataStructureElement> children = new();

        public void Clear()
        {
            foreach (var child in children)
                child.Clear();
        }
    }
}