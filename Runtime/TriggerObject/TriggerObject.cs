using System;

using UnityEngine;

namespace UnityUtility.TriggerObject
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObject : MonoBehaviour
    {
        public event Action<Collider> OnEnter;
        public event Action<Collider> OnExit;
        public event Action<Collider> OnStay;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other);
        }
        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke(other);
        }
    }
}
