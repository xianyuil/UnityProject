using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    private struct Timers
    {
        public float time;
        public float endTime;
        public System.Action callback;
    }
    private Dictionary<int, Timers> TimerPools;
    private int timerId = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        TimerPools = new Dictionary<int, Timers>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerPools.Count > 0)
        {
            var timerKeys = new List<int>(TimerPools.Keys);
            foreach (var item in timerKeys)
            {
                Timers timers = TimerPools[item];
                timers.time += Time.deltaTime;
                if (timers.time >= timers.endTime)
                {
                    if (timers.callback != null)
                    {
                        timers.callback();
                    }
                    TimerPools.Remove(item);
                }
                else
                {
                    TimerPools[item] = timers;
                }
            }
        }
    }
    public void AddTimer(float ftime,System.Action callback)
    {
        Timers timer = new Timers();
        timer.time = 0;
        timer.endTime = ftime;
        timer.callback = callback;
        TimerPools.Add(timerId, timer);
        timerId += 1;
    }
}
