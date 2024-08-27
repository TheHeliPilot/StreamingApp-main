using System.Collections.Generic;
using MPLATFORMLib;

namespace Streamstar;

public static class IMAudioTrack_List_Ext //list som dal za typ ktory extendujeme lebo mi to prislo prehladnejsie ako list_imaudiotrack...
{
    public static void s_SetTrackGains(this List<IMAudioTrack> tracks, double[] gains)
    {
        for (int i = tracks.Count - 1; i >= 0; i--)
        {
            tracks[i].TrackGainSet(0, gains[i], 0);
        }
    }
    
    public static void s_ToggleTrackGains(this List<IMAudioTrack> tracks, bool[] tracksOn)
    {
        foreach (IMAudioTrack iAudioTrack in tracks)
        {
            iAudioTrack.TrackGainSet(0, -99, 0);
        }

        int max;
        if (tracksOn.Length - tracks.Count <= 0)
            max = tracksOn.Length;
        else
            max = tracksOn.Length - (tracksOn.Length - tracks.Count);
        
        for (int i = 0; i < max; i++)
        {
            tracks[i].TrackGainSet(0, tracksOn[i] ? 0 : -99, 0);
        }
    }
    public static void s_ToggleTrackGains(this List<IMAudioTrack> tracks)
    {
        //if (tracks.Count <= 0)
        //return;
        
        bool[] tracksOn = new bool[tracks.Count];
        for (int i = 0; i < tracksOn.Length; i++)
        {
            tracksOn[i] = tracks[i].s_GetGain() < 0;
        }
        
        foreach (IMAudioTrack iAudioTrack in tracks)
        {
            iAudioTrack.TrackChannelsGet(out int a, out _, out int c);
            //Console.WriteLine(a + " " + c);
            for (int i = 0; i < c; i++)
            {
                iAudioTrack.TrackGainSet(i, -99, 0);
            }
        }

        int max;
        if (tracksOn.Length - tracks.Count <= 0)
            max = tracksOn.Length;
        else
            max = tracksOn.Length - (tracksOn.Length - tracks.Count);
        
        for (int i = 0; i < max; i++)
        {
            tracks[i].TrackChannelsGet(out int a, out _, out int c);
            for (int j = 0; j < c; j++)
            {
                tracks[i].TrackGainSet(j, tracksOn[i] ? 0 : -99, 0);
            }
        }
    }
}