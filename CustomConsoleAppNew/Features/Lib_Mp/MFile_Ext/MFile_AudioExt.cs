using System;
using MPLATFORMLib;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Streamstar;

public static class MFile_AudioExt
{
    public static void s_SetGain(this MFileClass mf, double value)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack _audioTrack);
        if (_audioTrack == null) return;

        _audioTrack.TrackChannelsGet(out _, out _, out int _trackNum);
        for (int _i = 0; _i < _trackNum; _i++)
        {
            _audioTrack.TrackGainSet(_i, value, 0);
        }
        Console.WriteLine(mf.s_GetGain());
    }
    
    public static double s_GetGain(this MFileClass mf)
    {
        mf.AudioTrackGetByIndex(0, out _, out IMAudioTrack _audioTrack);
        if (_audioTrack == null) return -1;

        _audioTrack.TrackGainGet(0, out double _r);
        return _r;
    }
}