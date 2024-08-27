using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Streamstar.U;

public static class MemoryInfo
{
    public static PerformanceCounter performanceCounter;
    public static string GetMemUsed()
    {
        return GC.GetTotalMemory(false)/1000000 + "mb";
    }
    public static string GetTotalMemoryAvailable()
    {
        performanceCounter.CategoryName = "Memory";
        performanceCounter.CounterName = "Available MBytes";
        performanceCounter.InstanceName = "";
        performanceCounter.ReadOnly = true;
        return performanceCounter.NextValue() + "mb";
    }
}