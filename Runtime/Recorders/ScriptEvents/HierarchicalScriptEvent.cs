using System;

using UnityEngine;

namespace UnityUtility.Recorders
{
    public class HierarchicalScriptEvent : ScriptEvent
    {
        public HierarchicalScriptEvent Parent => m_parentEvent;

        protected HierarchicalScriptEvent m_parentEvent = null;

        protected DateTime m_startTime;
        protected bool m_ended = false;

        public HierarchicalScriptEvent(string eventName, string recorderName, HierarchicalScriptEvent parentEvent) : base(eventName, 0, recorderName)
        {
            m_parentEvent = parentEvent;
            m_startTime = DateTime.Now;
            m_ended = false;
        }

        public virtual void EndEvent()
        {
            m_eventDuration = (DateTime.Now - m_startTime).TotalMilliseconds;
            m_frame = Time.frameCount;
            m_ended = true;
        }

        public override string ToString()
        {
            if (m_ended)
            {
                return base.ToString();
            }
            return FormatEvent($"Event \"{m_eventName}\" is not finished");
        }
    }
}