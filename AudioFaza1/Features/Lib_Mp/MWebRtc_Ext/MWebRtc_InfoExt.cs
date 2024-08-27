using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MWebRtc_InfoExt
{
    public static void s_PrintInfo(this MWebRTC_PluginClass mWebrtc)
    {
        Console.WriteLine("\n\n PrintProps");

        //Get signaling server address
        mWebrtc.PropsGet("signaling_server", out string strSigServer);
        Console.WriteLine("signaling_server: " + strSigServer);

        //Get additional info about connections status
        mWebrtc.PropsGet("logged_in", out string strSigConnected); //Check if we are connected to signaling server
        Console.WriteLine("logged_in (connected/disconnected): " + strSigConnected);

        string strNumber;
        string strName;
        string strNames = string.Empty;
        mWebrtc.PropsOptionGetCount("connected_peers", out int nCount); //Get list of the connected peers
        for (int i = 0; i < nCount; i++)
        {
            mWebrtc.PropsOptionGetByIndex("connected_peers", i, out strNumber, out strName);
            strNames += strName;
            if (i != nCount - 1)
                strNames += " ,";
        }
        Console.WriteLine("connected_peers NAMES:" + strNames);

        mWebrtc.PropsOptionGetCount("", out nCount);
        Console.WriteLine($"\n PropsOption count: {nCount}");
        for (int i = 0; i < nCount; i++)
        {
            mWebrtc.PropsOptionGetByIndex("", i, out strNumber, out strName);
            Console.WriteLine($"PropsOption ({strNumber}): " + strName);
        }

        mWebrtc.s_PrintProps();
        Console.WriteLine("\n");
    }

    public static void s_PrintProps(this MWebRTC_PluginClass mWebrtc)
    {
        Console.WriteLine("\n MWebRTCClass PrintProps");

        mWebrtc.PropsGetCount("", out int nCount);

        for (int i = 0; i < nCount; i++)
        {
            mWebrtc.PropsGetByIndex("", i, out string k, out string v, out int id);
            Console.WriteLine("(" + id + ") " + k + ": \t\t" + v);
        }
    }
}