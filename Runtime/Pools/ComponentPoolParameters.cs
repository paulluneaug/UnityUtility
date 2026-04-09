using System;

using UnityEngine;

using UnityUtility.Inspector;

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
