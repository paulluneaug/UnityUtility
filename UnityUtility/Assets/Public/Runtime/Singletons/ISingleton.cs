namespace UnityUtility.Singletons
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
        public static T Instance { get; }
    }
}
