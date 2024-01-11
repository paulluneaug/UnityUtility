using UnityEngine;

namespace UnityUtility.Singletons
{
    /// <summary>
    /// Singleton for <see cref="MonoBehaviour"/>
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
    /// <seealso cref="SingletonScriptable{T}"/><br/>
    /// 
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingleton<T> where T : SingletonMonoBehaviour<T>
    {
        protected static T s_instance;

        private static object m_lock = new object();

        public static T Instance
        {
            get
            {
                if (m_applicationIsQuitting)
                {
                    return null;
                }

                lock (m_lock)
                {
                    if (s_instance == null)
                    {
                        s_instance = FindObjectOfType<T>();

                        if (s_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            s_instance = singleton.AddComponent<T>();
                            singleton.name = $"[Singleton] {nameof(T)}";

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return s_instance;
                }
            }
        }

        private static bool m_applicationIsQuitting = false;

        public static bool ApplicationIsQuitting { get => m_applicationIsQuitting; set => m_applicationIsQuitting = value; }

        protected virtual void Start()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                s_instance = FindObjectOfType<T>();

                Initialize();
            }
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public virtual void OnDestroy()
        {
            if (s_instance != null && s_instance == this)
            {
                m_applicationIsQuitting = true;
            }
        }

        public virtual void Initialize()
        {

        }

        public static bool IsInstanciated()
        {
            return s_instance != null;
        }

        public static void OverrideInstance(T instance)
        {
            s_instance = instance;
        }
    }
}
