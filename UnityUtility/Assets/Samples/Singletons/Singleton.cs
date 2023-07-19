using System;

namespace UnityUtility.Singletons
{
    public abstract class Singleton<T> : ISingleton<T> where T : Singleton<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = Activator.CreateInstance<T>();
                }
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
