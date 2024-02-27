using System;
using UnityEngine;

namespace UnityUtility.Filters
{
    /// <summary>
    /// An implementation of the 1€ filter to filter a noisy input <br/>
    /// Original work : <see href="https://gery.casiez.net/1euro/"/>
    /// </summary>
    public class OneEuroFilter
    {
        public float MinCutoff
        {
            get { return m_minCutoff; }
            set { m_minCutoff = value; }
        }

        public float Beta
        {
            get { return m_beta; }
            set { m_beta = value; }
        }

        protected bool m_firstTime;
        protected float m_minCutoff;
        protected float m_beta;
        protected float m_dcutoff;

        protected LowpassFilter m_xFilt;
        protected LowpassFilter m_dxFilt;

        public OneEuroFilter(float minCutoff, float beta)
        {
            m_firstTime = true;
            m_minCutoff = minCutoff;
            m_beta = beta;

            m_xFilt = new LowpassFilter();
            m_dxFilt = new LowpassFilter();
            m_dcutoff = 1;
        }
        /// <summary>
        /// Filters the value <paramref name="x"/>
        /// </summary>
        /// <param name="x">The value to filter</param>
        /// <returns>The filtered value of <paramref name="x"/></returns>

        public float Filter(float x, float deltaTime)
        {
            float rate = 1.0f / deltaTime;
            float dx = m_firstTime ? 0 : (x - m_xFilt.Last()) * rate;
            if (m_firstTime)
            {
                m_firstTime = false;
            }

            float edx = m_dxFilt.Filter(dx, Alpha(rate, m_dcutoff));
            float cutoff = m_minCutoff + m_beta * Math.Abs(edx);

            return m_xFilt.Filter(x, Alpha(rate, cutoff));
        }

        /// <summary>
        /// Filters the value <paramref name="x"/>
        /// </summary>
        /// <remarks>Does the same as Filter(x, <see cref="Time.deltaTime"/>)</remarks>
        /// 
        /// <param name="x">The value to filter</param>
        /// <returns>The filtered value of <paramref name="x"/></returns>
        public float Filter(float x)
        {
            return Filter(x, Time.deltaTime);
        }

        protected float Alpha(float rate, float cutoff)
        {
            float tau = 1.0f / (2 * Mathf.PI * cutoff);
            float te = 1.0f / rate;
            return 1.0f / (1.0f + tau / te);
        }
    }
}