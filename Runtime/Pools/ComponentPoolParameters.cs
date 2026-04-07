using System;

using UnityEngine;

namespace UnityUtility
{
    [Serializable]
    public struct ComponentPoolParameters<TComponent>
        where TComponent : Component
    {
        public Transform PoolParent;
        public int InitialPoolSize;

        [HelpBox("Can be null", UnityEngine.UIElements.HelpBoxMessageType.None)]
        public TComponent Prefab;
    }
}
