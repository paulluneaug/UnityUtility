using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtility.Recorder
{
    public class ScriptExecutionTimeRecorder
    {
        protected readonly struct ScriptEvent
        {
            private readonly string m_eventName;
            private readonly double m_eventDuration;
            private readonly int m_frame;
            private readonly string m_recorderName;

            public ScriptEvent(string eventName, double eventDuration, string recorderName)
            {
                m_eventName = eventName;
                m_eventDuration = eventDuration;
                m_frame = Time.frameCount;
                m_recorderName = recorderName;
            }

            public override readonly string ToString()
            {
                return $"[{m_recorderName}][Frame {m_frame}] Event \"{m_eventName}\" took {m_eventDuration} ms";
            }
        }

        protected const string DEFAULT_RECODER_NAME = "Recorder";
        protected const string RECODER_NAME_FMT = "Recorder {0}";

        protected List<ScriptEvent> m_events = null;

        protected DateTime m_firstEventTime;
        protected DateTime m_previousEventTime;
        protected readonly Action<string> m_logMethod;
        protected readonly string m_recorderName;

        public ScriptExecutionTimeRecorder(string recorderName = "", LogType logType = LogType.Log)
        {
            m_recorderName = string.IsNullOrEmpty(recorderName) ? DEFAULT_RECODER_NAME : string.Format(RECODER_NAME_FMT, recorderName);

            m_events = new List<ScriptEvent>();
            ReinitTimes();

            m_logMethod = logType switch
            {
                LogType.Log => Debug.Log,
                LogType.Warning => Debug.LogWarning,

                LogType.Error => Debug.LogError,
                LogType.Assert => Debug.LogError,
                LogType.Exception => Debug.LogError,

                _ => Debug.Log,
            };
        }

        public virtual void AddEvent(string eventName, bool logImmediatly = true)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                Debug.LogError($"[Recorder] This event name is null or empty, it is not a valid name");
                return;
            }

            DateTime now = DateTime.Now;
            ScriptEvent newEvent = new ScriptEvent
            (
                eventName,
                (now - m_previousEventTime).TotalMilliseconds,
                m_recorderName
            );
            m_events.Add(newEvent);

            if (logImmediatly)
            {
                m_logMethod(newEvent.ToString());
            }

            m_previousEventTime = now;
        }

        public virtual void LogLastEvent()
        {
            if (m_events.Count > 0)
            {
                m_logMethod(m_events.Last().ToString());
            }
            else
            {
                m_logMethod($"[{m_recorderName}] No events to log");
            }
        }

        public virtual void LogAllEvents()
        {
            string log = string.Empty;
            foreach (ScriptEvent e in m_events)
            {
                log += e.ToString() + "\n";
            }
            log += GetAllEventsTimeSpan();

            m_logMethod(log);
        }

        public virtual void LogAllEventsTimeSpan()
        {
            m_logMethod(GetAllEventsTimeSpan());
        }

        public virtual void Reset()
        {
            m_events.Clear();
            ReinitTimes();
        }

        protected virtual void ReinitTimes()
        {
            m_firstEventTime = DateTime.Now;
            m_previousEventTime = m_firstEventTime;
        }

        protected virtual string GetAllEventsTimeSpan()
        {
            return $"[{m_recorderName}] All the logged events took {(m_previousEventTime - m_firstEventTime).TotalMilliseconds} ms so far";
        }
    }
}
