using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility.Recorders
{
    public abstract class GenericRecorder<TEvent> where TEvent : ScriptEvent
    {
        protected const string DEFAULT_RECODER_NAME = "Recorder";
        protected const string RECODER_NAME_FMT = "Recorder {0}";

        public string Name => m_recorderName;

        protected Stack<TEvent> m_events = null;

        protected DateTime m_firstEventTime;
        protected DateTime m_previousEventTime;
        protected readonly Action<string> m_logMethod;
        protected readonly string m_recorderName;

        public GenericRecorder(string recorderName = "", LogType logType = LogType.Log)
        {
            m_recorderName = string.IsNullOrEmpty(recorderName) ? DEFAULT_RECODER_NAME : string.Format(RECODER_NAME_FMT, recorderName);

            m_events = new Stack<TEvent>();
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

        protected virtual void CreateEvent(string eventName, bool logImmediatly = true)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                Debug.LogError(FormatLog("This event name is null or empty, it is not a valid name"));
                return;
            }
            TEvent newEvent = NewEvent
            (
                eventName
            );
            m_events.Push(newEvent);

            if (logImmediatly)
            {
                m_logMethod(newEvent.ToString());
            }

            m_previousEventTime = Now();
        }

        protected void LogEvent(TEvent eventToLog)
        {
            m_logMethod(eventToLog.ToString());
        }

        protected abstract TEvent NewEvent(string eventName);

        public virtual void LogLastEvent()
        {
            if (m_events.Count > 0)
            {
                m_logMethod(m_events.Peek().ToString());
            }
            else
            {
                m_logMethod(FormatLog("No events to log"));
            }
        }

        public virtual void LogAllEvents()
        {
            string log = string.Empty;
            foreach (TEvent e in m_events)
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
            m_firstEventTime = Now();
            m_previousEventTime = m_firstEventTime;
        }

        protected virtual string GetAllEventsTimeSpan()
        {
            return FormatLog($"All the logged events took {(m_previousEventTime - m_firstEventTime).TotalMilliseconds} ms so far");
        }

        protected virtual string FormatLog(string log)
        {
            return $"[{m_recorderName}] {log}";
        }

        protected static DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
