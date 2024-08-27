using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MMixer_Ext
{
    public static void s_SetVideoFormat(this MMixerClass mm, int w, int h, double fps)
    {
        M_VID_PROPS vidProps = new()
        {
            nWidth = w,
            nHeight = h,
            dblRate = fps,
            eVideoFormat = eMVideoFormat.eMVF_Custom,
            fccType = eMFCC.eMFCC_ARGB32
        };

        mm.FormatVideoSet(eMFormatType.eMFT_Convert, ref vidProps);    // ref as seen in examples
    }
    
    public static void s_Start(this MMixerClass mm)
    {
        mm.ObjectStart(null); // We turn on the mixer with an empty input
        mm.FilePlayStart();     // needed?
    }
    
    public static void s_Add_WebRTC_Plugin(this MMixerClass mf, MWebRTC_PluginClass mWebrtc)
    {
        mf.PluginsAdd(mWebrtc, 0);
    }

    public static void s_Remove_WebRTC_Plugin(this MMixerClass mf, MWebRTC_PluginClass mWebrtc)
    {
        mf.PluginsRemove(mWebrtc);
    }

    public static IMElements s_GetRoot(this MMixerClass mm)   // We get the first scene (default) scene to which we will then add elements
    {
        mm.ElementsGetByIndex(0, out MElement scene);
        IMElements imE = scene as IMElements;
        return imE;
    }
    
    public static void s_CreateElement_Layer(this MMixerClass mm, int idx, string @params)
    {
        string streamId = $"layer_{idx}s";
        string elemId = $"layer_{idx}e";
        string param = "stream_id=" + streamId + " " + @params;

        IMElements root = mm.s_GetRoot();
        root.ElementsAdd(elemId, "video", param, out MElement mElem, 0);

        mElem.ElementReorder(idx);
    }

    public static void s_SetStream_Layer(this MMixerClass mm, int idx, MFileClass mf, string @params = "", double duration = 0)
    {
        string mp = mf.s_GetMp();
        //string mp = MFile_Ext.x_Mp(mf);
        string streamId = $"layer_{idx}s";
        try
        {
            mm.StreamsAdd(streamId, null, mp, @params, out _, duration);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void s_CreateElement_Main(this MMixerClass mm)
    {
        const string streamId = "main_s";
        const string elemId = "main_e";
        const string param = "stream_id=" + streamId + " h=1 w=1 x=0 y=0 show=1";

        IMElements root = mm.s_GetRoot();
        MElement mElem;
        root.ElementsAdd(elemId, "video", param, out mElem, 0);

        mElem.ElementReorder(-999);
    }

    public static void s_SetStream_Main(this MMixerClass mm, MFileClass mf, string @params = "", double duration = 0)
    {
        try
        {
            string mp = mf.s_GetMp();
            const string streamId = "main_s";
            mm.StreamsAdd(streamId, null, mp, @params, out _, duration);
        }
        catch
        {
            Console.WriteLine("File already in use");
        }
        // if you get "System cannot find the file specified, you are missing "Start()" on file
    }
}