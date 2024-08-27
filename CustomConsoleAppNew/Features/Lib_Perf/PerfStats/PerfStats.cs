using System.Diagnostics;
// ReSharper disable InconsistentNaming

namespace Streamstar.U;

public static class PerfStats
{
    public static HistoryLogger CpuLog;
    public static HistoryLogger GpuDedicatedLog;
    public static HistoryLogger GpuSharedLog;
    public static HistoryLogger MemLogTotal;
    public static HistoryLogger MemLogUsed;
    public static HistoryLogger NetworkInLog;
    public static HistoryLogger NetworkOutLog;
    
    public static void Start(PerformanceCounter pc)
    {
        CpuInfo.performanceCounter = pc;
        MemoryInfo.performanceCounter = pc;
        CpuLog = new HistoryLogger(CpuInfo.GetCpu);
        GpuDedicatedLog = new HistoryLogger(GpuInfo.GetGpuDedicated);
        GpuSharedLog = new HistoryLogger(GpuInfo.GetGpuShared);
        MemLogTotal = new HistoryLogger(MemoryInfo.GetTotalMemoryAvailable);
        MemLogUsed = new HistoryLogger(MemoryInfo.GetMemUsed);
        NetworkInLog = new HistoryLogger(NetworkInfo.GetNetworkIn);
        NetworkOutLog = new HistoryLogger(NetworkInfo.GetNetworkOut);
    }
}