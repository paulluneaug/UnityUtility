using UnityEngine;

using UnityUtility.Attributes;

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
    /// <seealso cref="ScriptableSingleton{T}"/><br/>
    /// 
    /// </summary>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, ISingleton<T>
        where T : MonoBehaviourSingleton<T>
    {
        [Title("Singleton settings")]
        [SerializeField] private bool m_dontDestroyOnLoad = true;


        protected static T s_instance;

        private static readonly object s_lock = new object();

        public static T Instance
        {
            get
            {
                if (s_applicationIsQuitting)
                {
                    return null;
                }

                lock (s_lock)
                {
                    if (s_instance != null)
                    {
                        return s_instance;
                    }

                    s_instance = FindFirstObjectByType<T>();

                    if (s_instance != null)
                    {
                        return s_instance;
                    }


                    GameObject singleton = new GameObject();
                    s_instance = singleton.AddComponent<T>();
                    singleton.name = $"[Singleton] {typeof(T).Name}";

                    return s_instance;
                }
            }
        }

        private static bool s_applicationIsQuitting = false;

        public static bool ApplicationIsQuitting { get => s_applicationIsQuitting; set => s_applicationIsQuitting = value; }

        protected virtual void Awake()
        {
            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Initialize();
            }
        }

        protected virtual void Start()
        {
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
                s_applicationIsQuitting = true;
            }
        }

        public virtual void Initialize()
        {
            if (m_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
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
