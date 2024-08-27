using System;
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
}