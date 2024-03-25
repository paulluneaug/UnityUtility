using UnityEngine;
using UnityEngine.Profiling;

namespace UnityUtility.Recorders
{
    public class HierarchicalProfilingRecorder : HierarchicalRecorder
    {
        public HierarchicalProfilingRecorder(string recorderName = "", LogType logType = LogType.Log) : base(recorderName, logType) { }

        public override void BeginEvent(string eventName, bool logImmediatly = false)
        {
            base.BeginEvent(eventName, logImmediatly);
            Profiler.BeginSample(FormatLog(eventName));
        }

        public override void EndEvent(bool log = true)
        {
            base.EndEvent(log);
            Profiler.EndSample();
        }

        public override void Reset()
        {
            StopAllOngoingEvents();
            base.Reset();
        }

        public virtual void StopAllOngoingEvents()
        {
            while (m_depth > 0)
            {
                Profiler.EndSample();
                m_depth--;
            }
        }
    }
}
