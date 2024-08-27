using System;
using System.IO;
using MPLATFORMLib;

namespace Streamstar;

public static class MFile_Ext
{
    public static string s_GetLoop(this MFileClass mf)
    {
        mf.PropsGet("loop", out string s);
        return s;
    }

    public static void s_Play(this MFileClass mf)
    {
        try
        {
            mf.FilePlayStart();
        }
        catch (Exception e)
        {
            Console.WriteLine("Cant play media.." + e);
        }
    }

    public static void s_Stop(this MFileClass mf)
    {
        mf.FilePlayStop(0);
    }

    public static void s_SeekStart(this MFileClass mf)
    {
        mf.FilePosSet(0, 0);
    }

    public static void s_Loop(this MFileClass mf, bool b = true)
    {
        mf.PropsSet("loop", b ? "true" : "false");
    }

    public static void s_FileNameSet(this MFileClass mf, string path, string options = "")
    {
        if (path.EndsWith(".mp4") && !File.Exists(path))
            throw new Exception("file does not exist -> FilePlayStart will throw 'Invalid Index' / " + path);

        mf.FileNameSet(path, options);
    }

    public static void s_Add_WebRTC_Plugin(this MFileClass mf, MWebRTC_PluginClass mWebrtc)
    {
        mf.PluginsAdd(mWebrtc, 0);
    }

    public static void s_Remove_WebRTC_Plugin(this MFileClass mf, MWebRTC_PluginClass mWebrtc)
    {
        mf.PluginsRemove(mWebrtc);
    }

    public static void s_SetPath_PlayOnGpu(this MFileClass mf, string path)
    {
        mf.FileNameSet(path, "decoder.nvidia=true");
        mf.s_Play();
    }

    public static void s_SetPath_UseGpu(this MFileClass mf, string path)
    {
        mf.FileNameSet(path, "decoder.nvidia=true");
    }
    
    public static string s_GetMp(this MFileClass mfc)
    {
        mfc.ObjectNameGet(out string name);
        return "mp://" + name;
    }
}