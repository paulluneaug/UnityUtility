using UnityEngine;

namespace UnityUtility.Singletons
{
    /// <summary>
    /// Singleton for <see cref="ScriptableObject"/>
    /// 
    /// <para>
    /// In order to prevent a non singleton constructor like : 
    /// "<c><see cref="T"/> invalidSingleton = new <see cref="T"/>()</c>" <br/>
    /// you sould add "<c>protected <see cref="T"/>(){}</c>" 
    /// to your singleton class
    /// </para>
    /// 
    /// See also : <br/>
    /// <seealso cref="ISingleton{T}"/><br/>
    /// <seealso cref="Singleton{T}"/><br/>
    /// <seealso cref="SingletonMonoBehaviour{T}"/><br/>
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
