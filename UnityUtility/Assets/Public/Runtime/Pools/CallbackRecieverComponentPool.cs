using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace UnityUtility.Pools
{
    /// <summary>
    /// A <see cref="ComponentPool{TComponent}"/> for <see cref="IPoolOperationCallbackReciever"/>
    /// </summary>
    /// <typeparam name="TComponent">Pooled component type</typeparam>
    public class CallbackRecieverComponentPool<TComponent> : ComponentPool<TComponent> 
        where TComponent : Component, IPoolOperationCallbackReciever
    {
        public override PooledObject<TComponent> Request()
        {
            PooledObject<TComponent> requestedObj = base.Request();
            requestedObj.Object.OnObjectRequested();
            return requestedObj;
        }

        public override void Release(TComponent releasedObject)
        {
            releasedObject.OnObjectReleased();
            base.Release(releasedObject);
        }
    }
}
