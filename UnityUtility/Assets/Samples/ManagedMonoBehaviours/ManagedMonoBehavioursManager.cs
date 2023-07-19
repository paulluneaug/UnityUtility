using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityUtility.Singletons;

namespace UnityUtility.ManagedMonoBehaviours
{
    public class ManagedMonoBehavioursManager : SingletonMonoBehaviour<ManagedMonoBehavioursManager>
    {
        [SerializeField] private int m_managedMonoBehavioursStartCapacity = 1000;

        private List<ManagedMonoBehaviour> m_managedMonoBehaviours;

        private void Awake()
        {
            m_managedMonoBehaviours = new List<ManagedMonoBehaviour>();
        }

        public void AddManagedMonoBehaviour(ManagedMonoBehaviour managedMonoBehaviour)
        {
            m_managedMonoBehaviours.Add(managedMonoBehaviour);
        }

        public void RemoveManagedMonoBehaviour(ManagedMonoBehaviour managedMonoBehaviour)
        {
            m_managedMonoBehaviours.Remove(managedMonoBehaviour);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (ManagedMonoBehaviour m in m_managedMonoBehaviours)
            {
                if (m.enabled && m.gameObject.activeInHierarchy)
                {
                    m.LogicUpdate(deltaTime);
                }
            }
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            foreach (ManagedMonoBehaviour m in m_managedMonoBehaviours)
            {
                if (m.enabled && m.gameObject.activeInHierarchy)
                {
                    m.LogicFixedUpdate(fixedDeltaTime);
                }
            }
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            foreach (ManagedMonoBehaviour m in m_managedMonoBehaviours)
            {
                if (m.enabled && m.gameObject.activeInHierarchy)
                {
                    m.LogicLateUpdate(deltaTime);
                }
            }
        }

        private void OnApplicationQuit()
        {
            foreach (ManagedMonoBehaviour m in m_managedMonoBehaviours)
            {
                if (m.enabled)
                {
                    m.LogicOnApplicationQuit();
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (ManagedMonoBehaviour m in m_managedMonoBehaviours)
            {
                if (m.enabled)
                {
                    m.LogicOnDrawGizmos();
                }
            }
        }
#endif
    }
}
