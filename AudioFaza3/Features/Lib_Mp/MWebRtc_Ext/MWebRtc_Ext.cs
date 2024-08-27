using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MWebRtc_Ext
{
    private static MMixerClass curSource = new();
    public static SoundMixer CurSoundMixer = new();

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

    public static SoundMixer s_GetCurSoundMixer(this MWebRTC_PluginClass mw)
    {
        return CurSoundMixer;
    }
    
    public static void s_SetSource(this MWebRTC_PluginClass mw, MMixerClass newSource, SoundMixer newSoundMixer = null)
    {
        if (newSource == curSource)
            return;
        
        Console.WriteLine("not same");
        CurSoundMixer = newSoundMixer;
        CurSoundMixer?.UpdateGains();
        newSource.s_Add_WebRTC_Plugin(mw);

        if (curSource == null)
        {
            curSource = newSource;
            return;
        }

        curSource.s_Remove_WebRTC_Plugin(mw);
        curSource = newSource;
        
        /*MRendererClass mr = new();
        M_VID_PROPS vidProps = new()
        {
            eVideoFormat = eMVideoFormat.eMVF_Custom,
            dblRate = 60,
            fccType = eMFCC.eMFCC_RGB32
        };

        mr.FormatVideoSet(eMFormatType.eMFT_Convert, vidProps);
        mr.ObjectStart(newSource);
        mr.PluginsAdd(mw, 0);*/
    }

    public static void s_SetSoundMixer(this MWebRTC_PluginClass mw, SoundMixer sm)
    {
        CurSoundMixer = sm;
        CurSoundMixer?.UpdateGains();
    }
}