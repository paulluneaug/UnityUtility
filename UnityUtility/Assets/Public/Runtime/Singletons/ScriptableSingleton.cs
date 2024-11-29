using UnityEngine;

namespace UnityUtility.Singletons
{
    /// <summary>
    /// Singleton for <see cref="ScriptableObject"/>
    /// 
    /// <para>
    /// In order to prevent a non singleton constructor like : 
    /// "<c><see cref="T"/> invalidSingleton = new <see cref="T"/>()</c>" <br/>
    /// you should add "<c>protected <see cref="T"/>(){}</c>" 
    /// to your singleton class
    /// </para>
    /// 
    /// See also : <br/>
    /// <seealso cref="ISingleton{T}"/><br/>
    /// <seealso cref="Singleton{T}"/><br/>
    /// <seealso cref="MonoBehaviourSingleton{T}"/><br/>
    /// </summary>
    public abstract class ScriptableSingleton<T> : ScriptableObject, ISingleton<T> where T : ScriptableSingleton<T>
    {
        private static T s_instance = null;

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
                s_instance = Resources.Load<T>(typeof(T).Name);
            }
        }
    }
}
