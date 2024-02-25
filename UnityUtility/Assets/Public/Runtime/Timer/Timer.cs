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

        public bool TimerEnded => m_timedEnded;

        [SerializeField, Min(0.0f)] private float m_duration = 1.0f;

        [NonSerialized] private float m_currentTime = 0.0f;
        [NonSerialized] private bool m_timedEnded = false;

        /// <summary>
        /// The duration of the timer
        /// </summary>
        /// <remarks>Warning : Setting the duration using this property resets the timer</remarks>
        public float Duration
        {
            get => m_duration;
            set
            {
                m_duration = value;
                Reset();
            }
        }

        public Timer(float duration)
        {
            m_duration = Mathf.Max(0.0f, duration);
            m_currentTime = 0.0f;
            m_timedEnded = false;
        }

        /// <summary>
        /// Updates the timer
        /// </summary>
        /// <param name="deltaTime">Time since the last Update</param>
        /// <returns>Wether the timer ended</returns>
        public bool Update(float deltaTime)
        {
            m_currentTime += deltaTime;
            if (m_currentTime > m_duration)
            {
                if (!m_timedEnded)
                {
                    m_timedEnded = true;
                    OnTimerEnds?.Invoke();
                }
                return true;
            }
            return false;
        }

        public void Reset()
        {
            m_currentTime = 0.0f;
            m_timedEnded = false;
        }
    }
}
