using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MMixer_AudioChannelExt
{
    public static void s_AddChannelsFromSources(this MMixerClass mm, MFileClass[] mixers)
    {
        for (int j = 1; j < mixers.Length; j++)
        {
            string s = j * 2 + "," + (j * 2 + 1);
            mm.AudioTrackAdd(0, s, "", out _);
            Console.WriteLine(s);
        }
        
        for (int i = 0; i < mixers.Length; i++)
        {
            string mixerPropsString = "";
            for (int j = 0; j < i; j++)
            {
                mixerPropsString += "-1,-1,";
            }

            mixerPropsString += "0,1";
            Console.WriteLine(mixerPropsString);
            (mixers[i] as IMProps).PropsSet("object::audio_channels", mixerPropsString);
            MLiveClass myLive = new();
            myLive.DeviceSet("video", "<None>", "");
            myLive.DeviceSet("ext_audio", "mp://" + mixers[i].s_GetMp(), "");
            myLive.ObjectStart(null);
            mm.StreamsAdd("audioStream " + i, myLive, "", "", out _, 0);
        }
    }

    public static void s_ToggleTracks(this MMixerClass mm, int track)
    {
        string mixerPropsString = "";
        int channels = mm.s_GetAudioTracks().Count * 2;
        Console.WriteLine(channels);
        for (int j = 0; j < channels; j++)
        {
            if(j == track*2-1 || j==track*2-2)
                mixerPropsString += "-99,";
            else
                mixerPropsString += "0,";
        }
        Console.WriteLine(mixerPropsString);
        (mm as IMProps).PropsSet("object::audio_gain", mixerPropsString);
    }
}