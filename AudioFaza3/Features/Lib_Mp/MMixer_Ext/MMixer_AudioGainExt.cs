using System;
using System.Collections.Generic;
using MPLATFORMLib;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Streamstar;

public static class MMixer_AudioGainExt
{
    public static void s_SetGain(this MMixerClass mf, double value)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack audioTrack);
        if (audioTrack == null) return;
        
        audioTrack.TrackChannelsGet(out _, out _, out int trackNum);
        for (int i = 0; i < trackNum; i++) audioTrack.TrackGainSet(i, value, 0);
        Console.WriteLine(mf.s_GetGain());
    }

    public static double s_GetGain(this MMixerClass mf)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack audioTrack);
        if (audioTrack == null) return -1;

        audioTrack.TrackGainGet(0, out double r);
        return r;
    }
}