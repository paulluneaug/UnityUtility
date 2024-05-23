using UnityEngine;

namespace UnityUtility.Recorders
{
    public class HierarchicalRecorder : GenericRecorder<HierarchicalScriptEvent>
    {
        protected int m_depth = 0;
        protected HierarchicalScriptEvent m_currentEvent = null;

        public HierarchicalRecorder(string recorderName = "", LogType logType = LogType.Log) : base(recorderName, logType)
        {
            m_depth = 0;
            m_currentEvent = null;
        }

        public virtual void BeginEvent(string eventName, bool logImmediatly = false)
        {
            CreateEvent(eventName, logImmediatly);
            if (string.IsNullOrEmpty(eventName))
            {
                return;
            }
            m_depth++;
        }

        public virtual ScopedEventHandler BeginScopedEvent(string eventName)
        {
            BeginEvent(eventName, false);
            return new ScopedEventHandler(this);
        }

        public virtual void EndEvent(bool log = true)
        {
            if (m_depth == 0)
            {
                Debug.LogError(FormatLog("No event started"));
                return;
            }
            m_depth--;
            m_currentEvent.EndEvent();
            if (log)
            {
                LogEvent(m_currentEvent);
            }
            m_currentEvent = m_currentEvent.Parent;
        }

        public override void Reset()
        {
            base.Reset();
            m_depth = 0;
            m_currentEvent = null;
        }

        protected override HierarchicalScriptEvent NewEvent(string eventName)
        {
            HierarchicalScriptEvent newEvent = new HierarchicalScriptEvent(eventName, m_recorderName, m_currentEvent);
            m_currentEvent = newEvent;
            return newEvent;
        }
    }
}
