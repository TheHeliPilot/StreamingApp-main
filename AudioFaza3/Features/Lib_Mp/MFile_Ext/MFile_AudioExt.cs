using System;
using System.Collections.Generic;
using MPLATFORMLib;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Streamstar;

public static class MFile_AudioExt
{
    public static void s_SetGain(this MFileClass mf, double value)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack audioTrack);
        if (audioTrack == null) return;
        
        audioTrack.TrackChannelsGet(out _, out _, out int trackNum);
        for (int i = 0; i < trackNum; i++) audioTrack.TrackGainSet(i, value, 0);
        Console.WriteLine(mf.s_GetGain());
    }

    public static double s_GetGain(this MFileClass mf)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack audioTrack);
        if (audioTrack == null) return -1;

        audioTrack.TrackGainGet(0, out double r);
        return r;
    }
    
    public static List<IMAudioTrack> s_GetAudioTracks(this MFileClass mm)
    {
        mm.AudioTracksGetCount(out int trackCount);
        List<IMAudioTrack> tracks = new();

        for (int i = 0; i < trackCount; i++)
        {
            mm.AudioTrackGetByIndex(i, out _, out IMAudioTrack track);
            tracks.Add(track);
        }

        return tracks;
    }
    
    public static double[] s_GetAudioTrackGains(this MFileClass mm, int channel = 0)
    {
        mm.AudioTracksGetCount(out int trackCount);
        List<double> trackGains = new();

        for (int i = 0; i < trackCount; i++)
        {
            mm.AudioTrackGetByIndex(i, out _, out IMAudioTrack track);
            track.TrackGainGet(channel, out double trackGain);
            trackGains.Add(trackGain);
        }

        return trackGains.ToArray();
    }
}