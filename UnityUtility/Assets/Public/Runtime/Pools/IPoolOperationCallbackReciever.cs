namespace UnityUtility.Pools
{
    /// <summary>
    /// Interface for pooled objects to recieve callbacks when the object is requested or released from/into the pool
    /// 
    /// <para>
    /// Contains :
    /// <br><seealso cref="OnObjectRequested"/></br>
    /// <br><seealso cref="OnObjectReleased"/></br>
    /// </para>
    /// </summary>
    public interface IPoolOperationCallbackReciever
    {
        /// <summary>
        /// Called when the object exits the pool
        /// </summary>
        public void OnObjectRequested();

        /// <summary>
        /// Called when the object gets back in the pool
        /// </summary>
        public void OnObjectReleased();
    }
}
