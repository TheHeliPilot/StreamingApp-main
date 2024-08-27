using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Streamstar.U;

public static class GpuInfo
{
    public static string GetGpuDedicated()
    {
        PerformanceCounterCategory _category = new("GPU Adapter Memory");
        string[] _counterNames = _category.GetInstanceNames();
        List<PerformanceCounter> _gpuCountersDedicated = new();
        List<PerformanceCounter> _gpuCountersShared = new();
        float _result = 0f;

        foreach (string _counterName in _counterNames)
        {
            foreach (PerformanceCounter _counter in _category.GetCounters(_counterName))
            {
                switch (_counter.CounterName)
                {
                    case "Dedicated Usage":
                        _gpuCountersDedicated.Add(_counter);
                        break;
                    case "Shared Usage":
                        _gpuCountersShared.Add(_counter);
                        break;
                }
            }
        }
        
        _gpuCountersDedicated.ForEach(x =>
        {
            _result += x.NextValue();
        });

        return _result / 1000000 + "mb";
    }
    
    public static string GetGpuShared()
    {
        PerformanceCounterCategory _category = new("GPU Adapter Memory");
        string[] _counterNames = _category.GetInstanceNames();
        List<PerformanceCounter> _gpuCountersDedicated = new();
        List<PerformanceCounter> _gpuCountersShared = new();
        float _result = 0f;

        foreach (string _counterName in _counterNames)
        {
            foreach (PerformanceCounter _counter in _category.GetCounters(_counterName))
            {
                switch (_counter.CounterName)
                {
                    case "Dedicated Usage":
                        _gpuCountersDedicated.Add(_counter);
                        break;
                    case "Shared Usage":
                        _gpuCountersShared.Add(_counter);
                        break;
                }
            }
        }
        
        _gpuCountersShared.ForEach(x =>
        {
            _result += x.NextValue();
        });

        return _result / 1000000 + "mb";
    }
}