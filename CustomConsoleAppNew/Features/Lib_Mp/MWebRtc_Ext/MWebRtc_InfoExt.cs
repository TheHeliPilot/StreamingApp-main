using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MWebRtc_InfoExt
{
    public static void s_PrintInfo(this MWebRTC_PluginClass mWebrtc)
    {
        Console.WriteLine("\n\n PrintProps");

        //Get signaling server address
        mWebrtc.PropsGet("signaling_server", out string _strSigServer);
        Console.WriteLine("signaling_server: " + _strSigServer);

        //Get additional info about connections status
        mWebrtc.PropsGet("logged_in", out string _strSigConnected); //Check if we are connected to signaling server
        Console.WriteLine("logged_in (connected/disconnected): " + _strSigConnected);

        string _strNumber;
        string _strName;
        string _strNames = string.Empty;
        mWebrtc.PropsOptionGetCount("connected_peers", out int _nCount); //Get list of the connected peers
        for (int i = 0; i < _nCount; i++)
        {
            mWebrtc.PropsOptionGetByIndex("connected_peers", i, out _strNumber, out _strName);
            _strNames += _strName;
            if (i != _nCount - 1)
                _strNames += " ,";
        }
        Console.WriteLine("connected_peers NAMES:" + _strNames);

        mWebrtc.PropsOptionGetCount("", out _nCount);
        Console.WriteLine($"\n PropsOption count: {_nCount}");
        for (int i = 0; i < _nCount; i++)
        {
            mWebrtc.PropsOptionGetByIndex("", i, out _strNumber, out _strName);
            Console.WriteLine($"PropsOption ({_strNumber}): " + _strName);
        }

        mWebrtc.s_PrintProps();
        Console.WriteLine("\n");
    }

    public static void s_PrintProps(this MWebRTC_PluginClass mWebrtc)
    {
        MWebRTC_PluginClass m = mWebrtc;
        Console.WriteLine("\n MWebRTCClass PrintProps");

        m.PropsGetCount("", out int nCount);

        for (int i = 0; i < nCount; i++)
        {
            m.PropsGetByIndex("", i, out string k, out string v, out int id);
            Console.WriteLine("(" + id + ") " + k + ": \t\t" + v);
        }
    }
}