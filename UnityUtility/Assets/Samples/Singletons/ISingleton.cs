using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility.Singletons
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
        public static T Instance { get; }
    }
}
