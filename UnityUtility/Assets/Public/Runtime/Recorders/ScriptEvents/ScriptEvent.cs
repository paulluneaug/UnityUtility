using UnityEngine;

namespace UnityUtility.Recorders
{
    public class ScriptEvent
    {
        protected readonly string m_eventName;
        protected readonly string m_recorderName;
        protected double m_eventDuration;
        protected int m_frame;

        public ScriptEvent(string eventName, double eventDuration, string recorderName)
        {
            m_eventName = eventName;
            m_eventDuration = eventDuration;
            m_frame = Time.frameCount;
            m_recorderName = recorderName;
        }

        public override string ToString()
        {
            return FormatEvent($"Event \"{m_eventName}\" took {m_eventDuration} ms");
        }

        protected virtual string FormatEvent(string message)
        {
            return $"[{m_recorderName}][Frame {m_frame}] {message}";
        }
    }
}