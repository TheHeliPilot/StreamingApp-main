using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Streamstar.U;

public class HistoryLogger
{
    private Timer _timerShort;
    private Timer _timerLong;

    public List<string> historyMinutes = new();
    public List<string> historySeconds = new();

    private Func<string> _retFunc;

    public HistoryLogger(Func<string> retFunc)
    {
        _retFunc = retFunc;
        Start();
    }

    public void Start()
    {
        _timerShort = new Timer(1 * 1000);
        _timerLong = new Timer(60 * 1000);
        
        _timerShort.Elapsed += _timerElapsedSeconds;
        _timerLong.Elapsed += _timerElapsedMinutes;
        _timerShort.AutoReset = true;
        _timerLong.AutoReset = true;
        
        _timerShort.Start();
        _timerLong.Start();
    }
    
    public void Stop()
    {
        _timerShort.Stop();
        _timerLong.Stop();
        _timerShort.Dispose();
        _timerLong.Dispose();
    }
    
    //minutes/seconds rename
    private void _timerElapsedSeconds(object sender, ElapsedEventArgs e)
    {
        string _toAdd = _retFunc + "\n";
        historySeconds.Add(_toAdd);
        if (historySeconds.Count > 60)
        {
            historySeconds.Remove(historySeconds.First());
        }
    }
    private void _timerElapsedMinutes(object sender, ElapsedEventArgs e)
    {
        string _toAdd = _retFunc + "\n";
        historyMinutes.Add(_toAdd);
    }

    public void PrintShort()
    {
        Console.Write("Short history :\n");
        Console.WriteLine(historySeconds.Last() + "\n");
    }
}