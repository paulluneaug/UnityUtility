
using UnityEngine;

namespace UnityUtility.Singletons
{
    /// <summary>
    /// Singleton for <see cref="ScriptableObject"/>
    /// </summary>
    public abstract class SingletonScriptable<T> : ScriptableObject, ISingleton<T> where T : SingletonScriptable<T>
    {
        private static T s_instance = default(T);

        public static T Instance
        {
            get
            {
                Load();
                return s_instance;
            }
        }

        public static void Load()
        {
            if (s_instance == null)
            {
                s_instance = Resources.Load<T>(nameof(T));
            }
        }
    }
}
