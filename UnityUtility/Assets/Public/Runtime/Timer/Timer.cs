using System;
using UnityEngine;

namespace UnityUtility.Timer
{
    [Serializable]
    public class Timer
    {
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

        /// <summary>
        /// Wether the timer is running (Read-Only)
        /// </summary>
        public bool IsRunning => m_isRunning;

        /// <summary>
        /// An event called when the timer's time reaches the timer's duration 
        /// </summary>
        public event Action OnTimerEnds;

        [SerializeField, Min(0.0f)] private float m_duration = 1.0f;
        [SerializeField] private bool m_repeat = false;

        // Cache
        [NonSerialized] private float m_currentTime = 0.0f;
        [NonSerialized] private bool m_isRunning = false;

        public Timer(float duration, bool repeat)
        {
            m_duration = Math.Max(0.0f, duration);
            m_currentTime = 0.0f;
            m_repeat = repeat;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            Pause(false);
        }

        /// <summary>
        /// Stops and resets the timer
        /// </summary>
        public void Stop()
        {
            Pause(true);
            Reset();
        }

        /// <summary>
        /// Pauses or unpauses the timer
        /// </summary>
        /// <param name="pause">Wether to pause the timer</param>
        public void Pause(bool pause)
        {
            m_isRunning = !pause;
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        public void Reset()
        {
            m_currentTime = 0.0f;
        }

        /// <summary>
        /// Updates the timer
        /// </summary>
        /// <param name="deltaTime">Time since the last timer update</param>
        /// <returns>Wether the timer ended</returns>
        public bool Update(float deltaTime)
        {
            if (m_isRunning)
            {
                m_currentTime += deltaTime;
                if (m_currentTime > m_duration)
                {
                    if (m_repeat)
                    {
                        m_currentTime %= m_duration;
                    }
                    else
                    {
                        Stop();
                    }
                    OnTimerEnds?.Invoke();
                    return true;
                }
            }
            return false;
        }
    }
}
