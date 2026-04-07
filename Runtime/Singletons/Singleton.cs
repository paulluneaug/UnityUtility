using System;

namespace UnityUtility.Singletons
{
    /// <summary>
    /// Base class for a Singletons
    /// <para>
    /// In order to prevent a non singleton constructor like : 
    /// "<c><see cref="T"/> invalidSingleton = new <see cref="T"/>()</c>" <br/>
    /// you should add "<c>protected <see cref="T"/>(){}</c>" 
    /// to your singleton class
    /// </para>
    /// 
    /// See also : <br/>
    /// <seealso cref="ISingleton{T}"/><br/>
    /// <seealso cref="MonoBehaviourSingleton{T}"/><br/>
    /// <seealso cref="ScriptableSingleton{T}"/><br/>
    /// 
    /// </summary>
    public abstract class Singleton<T> : ISingleton<T> where T : Singleton<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                s_instance ??= Activator.CreateInstance<T>();
                return s_instance;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        protected Singleton()
        {
            if (s_instance != null)
            {
                //todo : create specific exception
                throw new Exception("Instance already exists");
            }

            s_instance = this as T;

            if (s_instance == null)
            {
                throw new Exception("Instance creation failed");
            }
        }


        public static void DestroyInstance()
        {
            s_instance = null;
            GC.Collect();
        }
    }
}
