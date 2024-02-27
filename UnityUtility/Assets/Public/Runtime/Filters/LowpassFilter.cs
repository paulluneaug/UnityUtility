namespace UnityUtility.Filters
{
    public class LowpassFilter
    {
        protected bool m_initialized;
        protected float m_hatXPrev;

        public LowpassFilter()
        {
            m_initialized = true;
        }

        public float Last()
        {
            return m_hatXPrev;
        }

        public float Filter(float x, float alpha)
        {
            float hatX;
            if (m_initialized)
            {
                m_initialized = false;
                hatX = x;
            }
            else
            {
                hatX = alpha * x + (1 - alpha) * m_hatXPrev;
            }

            m_hatXPrev = hatX;

            return hatX;
        }
    }
}
