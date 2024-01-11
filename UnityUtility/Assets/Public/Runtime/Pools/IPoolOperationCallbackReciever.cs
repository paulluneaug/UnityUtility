namespace UnityUtility.Pools
{
    /// <summary>
    /// Interface for pooled objects to recieve callbacks when the object is requested or released from/into the pool
    /// <para>Contains :</para>
    /// <br><seealso cref="OnObjectRequested"/></br>
    /// <br><seealso cref="OnObjectReleased"/></br>
    /// </summary>
    public interface IPoolOperationCallbackReciever
    {
        public void OnObjectRequested();
        public void OnObjectReleased();
    }
}
