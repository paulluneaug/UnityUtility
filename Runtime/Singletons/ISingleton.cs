namespace UnityUtility.Singletons
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
        static T Instance { get; }
    }
}
