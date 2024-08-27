using System;
using System.Diagnostics;

namespace Streamstar.U;

public static class PerfLogger
{
    public static void LogCpu(this PerformanceCounter _pc)
    {
        Console.WriteLine("Current CPU usage: " + CpuInfo.GetCpu() + "\n");
    }
    public static void LogMem(this PerformanceCounter _pc)
    {
        Console.WriteLine("Current memory usage: " + MemoryInfo.GetMemUsed() + "\n");
        Console.WriteLine("Total memory available: " + MemoryInfo.GetTotalMemoryAvailable() + "\n");
    }
    public static void LogGpu(this PerformanceCounter _pc)
    {
        Console.WriteLine("Dedicated gpu usage: " + GpuInfo.GetGpuDedicated() + "\n" + 
                          "Shared gpu usage: " + GpuInfo.GetGpuShared() + "\n");
    }
    public static void LogNetwork(this PerformanceCounter _pc)
    {
        Console.WriteLine("Network usage");
        Console.WriteLine("    kb Recieved: " + NetworkInfo.GetNetworkIn());
        Console.WriteLine();
        Console.WriteLine("    kb Sent: " + NetworkInfo.GetNetworkOut());
    }

    public static void LogFull(this PerformanceCounter _pc)
    {
        Console.WriteLine("------------------------------------------------------\nFull performance log:\n\n");
        _pc.LogCpu();
        _pc.LogMem();
        _pc.LogGpu();
        _pc.LogNetwork();
        Console.WriteLine("------------------------------------------------------\n");
    }
}