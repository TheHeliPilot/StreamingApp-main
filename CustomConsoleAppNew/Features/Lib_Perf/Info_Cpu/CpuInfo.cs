using System.Collections.Generic;
using System.Diagnostics;

namespace Streamstar.U;

public static class CpuInfo
{
    private static float _last = 0;
    public static PerformanceCounter performanceCounter;
    
    //extension je potrebny iba pre history logger, z dovodu ze getgpu je jedina metoda co potrebuje nejaky parameter a
    //do loggeru sa neda poslat parametrova funkcia
    public static string GetCpu()
    {
        performanceCounter.CategoryName = "Processor";
        performanceCounter.CounterName = "% Processor Time";
        performanceCounter.InstanceName = "_Total";

        float _nextVal = performanceCounter.NextValue();
        
        return _nextVal == 0 ? _last + "%" : _nextVal + "%";
    }
}