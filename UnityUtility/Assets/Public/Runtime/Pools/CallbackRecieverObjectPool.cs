using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityUtility.Pools
{
    /// <summary>
    /// An <see cref="ObjectPool{T}"/> for <see cref="IPoolOperationCallbackReciever"/>
    /// </summary>
    /// <typeparam name="T">Pooled object type</typeparam>
    public class CallbackRecieverObjectPool<T> : ObjectPool<T> where T : class, IPoolOperationCallbackReciever, new()
    {
        public CallbackRecieverObjectPool(int initialPoolSize) : base(initialPoolSize)
        {
        }

        public override PooledObject<T> Request()
        {
            PooledObject<T> requestedObj = base.Request();
            requestedObj.Object.OnObjectRequested();
            return requestedObj;
        }

        public override void Release(T releasedObject)
        {
            releasedObject.OnObjectReleased();
            base.Release(releasedObject);
        }
    }
}
