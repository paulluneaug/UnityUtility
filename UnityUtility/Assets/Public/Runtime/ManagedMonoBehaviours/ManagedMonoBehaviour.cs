using UnityEngine;

namespace UnityUtility.ManagedMonoBehaviours
{
    public abstract class ManagedMonoBehaviour : MonoBehaviour
    {
        public new Transform transform
        {
            get
            {
                if (m_transform == null)
                {
                    m_transform = base.transform;
                }
                return m_transform;
            }
        }

        public new GameObject gameObject
        {
            get
            {
                if (m_gameObject == null)
                {
                    m_gameObject = base.gameObject;
                }
                return m_gameObject;
            }
        }

        protected GameObject m_gameObject;
        protected Transform m_transform;

        public virtual void LogicFixedUpdate(float fixedDeltaTime) { }
        public virtual void LogicUpdate(float deltaTime) { }
        public virtual void LogicLateUpdate(float deltaTime) { }
        public virtual void LogicOnApplicationQuit() { }

        protected virtual void Awake()
        {
            ManagedMonoBehavioursManager.Instance.AddManagedMonoBehaviour(this);
        }

        protected virtual void OnDestroy()
        {
            if (ManagedMonoBehavioursManager.ApplicationIsQuitting)
            {
                return;
            }
            ManagedMonoBehavioursManager.Instance.RemoveManagedMonoBehaviour(this);
        }


#if UNITY_EDITOR
        public virtual void LogicOnDrawGizmos() { }
#endif
    }
}
