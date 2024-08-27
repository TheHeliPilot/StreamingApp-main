using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Streamstar.U;

public static class NetworkInfo
{
    public static string GetNetworkIn()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
            return "-1";

        NetworkInterface[] _interfaces 
            = NetworkInterface.GetAllNetworkInterfaces();

        List<long> _arr = _interfaces.Select(_ni => _ni.GetIPv4Statistics().BytesReceived).ToList();

        long _ret = _arr.Sum();

        return _ret.ToString();
    }
    
    public static string GetNetworkOut()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
            return "-1";

        NetworkInterface[] _interfaces 
            = NetworkInterface.GetAllNetworkInterfaces();

        List<long> _arr = _interfaces.Select(_ni => _ni.GetIPv4Statistics().BytesSent).ToList();

        long _ret = _arr.Sum();
        
        return _ret.ToString();
    }
}