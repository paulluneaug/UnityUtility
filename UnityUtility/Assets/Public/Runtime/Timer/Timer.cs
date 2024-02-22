using System;
using UnityEngine;

namespace UnityUtility.Timer
{
    [Serializable]
    public class Timer
    {
        /// <summary>
        /// An event called when the timer's time reaches the timer's duration 
        /// </summary>
        public event Action OnTimerEnds;

        [SerializeField, Min(0.0f)] private float m_duration = 1.0f;
        [NonSerialized] private float m_currentTime = 0.0f;

        public float Duration
        {
            get => m_duration;
            set => m_duration = value;
        }

        public Timer(float duration)
        {
            m_duration = Mathf.Max(0.0f, duration);
            m_currentTime = 0.0f;
        }

        public bool Update(float deltaTime)
        {
            m_currentTime += deltaTime;
            if (m_currentTime > m_duration)
            {
                OnTimerEnds?.Invoke();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            m_currentTime = 0.0f;
        }
    }
}
