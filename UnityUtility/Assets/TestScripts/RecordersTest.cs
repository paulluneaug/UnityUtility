using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityUtility.Recorders;

public class RecordersTest : MonoBehaviour
{
    private Recorder r0;
    private HierarchicalRecorder r2;
    private HierarchicalProfilingRecorder r3;
    // Start is called before the first frame update
    void Start()
    {
        r0 = new Recorder("R0", LogType.Warning);
        r2 = new HierarchicalRecorder("R2", LogType.Warning);
        r3 = new HierarchicalProfilingRecorder("R3", LogType.Error);
    }

    // Update is called once per frame
    void Update()
    {
        TestRecorder(r0);
        TestRecorder(r2);
        TestRecorder(r3);
    }

    private void TestRecorder(Recorder recorder)
    {
        int eventindex = 0;
        recorder.Reset();
        Thread.Sleep(45);
        recorder.AddEvent($"Event {eventindex++}");
        Thread.Sleep(0);
        recorder.AddEvent($"Event {eventindex++}");
        Thread.Sleep(1);
        recorder.AddEvent($"Event {eventindex++}");
        recorder.AddEvent($"Event {eventindex++}");
        Thread.Sleep(12);
        Thread.Sleep(16);
        recorder.AddEvent($"Event {eventindex++}");
        recorder.LogLastEvent();
        recorder.LogAllEventsTimeSpan();
    }

    private void TestRecorder(HierarchicalRecorder recorder)
    {
        int eventindex = 0;
        recorder.Reset();
        recorder.BeginEvent(recorder.Name);
        recorder.BeginEvent($"Event {eventindex++}");
        Thread.Sleep(45);
        recorder.EndEvent();
        recorder.BeginEvent($"Event {eventindex++}");
        Thread.Sleep(10);
        recorder.BeginEvent($"Event {eventindex++}");
        Thread.Sleep(1);
        recorder.EndEvent();
        recorder.EndEvent();

        recorder.BeginEvent($"Event {eventindex++}");
        recorder.BeginEvent($"Event {eventindex++}");
        Thread.Sleep(12);
        recorder.EndEvent();
        recorder.BeginEvent($"Event {eventindex++}");
        Thread.Sleep(16);
        recorder.BeginEvent($"Event {eventindex++}");
        recorder.EndEvent();
        recorder.EndEvent();
        recorder.EndEvent();
        recorder.EndEvent();
        recorder.LogLastEvent();
        recorder.LogAllEventsTimeSpan();
        if (recorder is HierarchicalProfilingRecorder profilingRecorder)
        {
            profilingRecorder.StopAllOngoingEvents();
        }
    }
}
