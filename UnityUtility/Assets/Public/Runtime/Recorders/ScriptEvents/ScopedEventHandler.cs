using System;

namespace UnityUtility.Recorders
{
    public class ScopedEventHandler : IDisposable
    {
        private readonly HierarchicalRecorder m_recorder;
        public ScopedEventHandler(HierarchicalRecorder recorder)
        {
            m_recorder = recorder;
        }

        public void Dispose()
        {
            m_recorder.EndEvent();
        }
    }
}

