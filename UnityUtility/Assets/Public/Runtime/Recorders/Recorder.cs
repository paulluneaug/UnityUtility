using System;
using UnityEngine;

namespace UnityUtility.Recorders
{
    public class Recorder : GenericRecorder<ScriptEvent>
    {
        public Recorder(string recorderName = "", LogType logType = LogType.Log) : base(recorderName, logType)
        {
        }

        public virtual void AddEvent(string eventName, bool logImmediatly = true)
        {
            CreateEvent(eventName, logImmediatly);
        }

        protected override ScriptEvent NewEvent(string eventName)
        {
            DateTime now = Now();
            ScriptEvent newEvent = new ScriptEvent
            (
                eventName,
                (now - m_previousEventTime).TotalMilliseconds,
                m_recorderName
            );
            return newEvent;
        }
    }
}
