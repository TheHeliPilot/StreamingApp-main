using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MWebRtc_Ext
{
    private static readonly MFileClass[] _curSources = new MFileClass[2];

    public static void s_Login(this MWebRTC_PluginClass mWebrtc,
        string url = "https://rtc.medialooks.com:8889/test/test")
    {
        //mWebrtc = new MWebRTC_PluginClass();
        mWebrtc.PropsSet("mode", "sender");

        mWebrtc.Login(url, "", out string _);
        Console.WriteLine("\n\n-------------------Running on: " + url);
    }

    public static void s_Logout(this MWebRTC_PluginClass mWebrtc)
    {
        mWebrtc.Logout();
    }

    public static void s_SetSource(this MWebRTC_PluginClass mw, MFileClass newSource, int mWebRtcId = 0)
    {
        //mwebrtc_id si nevsimaj, malo to byt na nieco ine ale nakoniec z toho vzniklo to ze ak by ze mas viac mwebrtc pluginov co neviem na co je ale je to tu, kludne mozem odstranit

        if (newSource == _curSources[mWebRtcId])
            return;

        newSource.s_Add_WebRTC_Plugin(mw);

        if (_curSources[mWebRtcId] == null)
        {
            _curSources[mWebRtcId] = newSource;
            return;
        }

        _curSources[mWebRtcId].s_Remove_WebRTC_Plugin(mw);
        _curSources[mWebRtcId] = newSource;
    }
}