using System.Collections.Generic;
using MPLATFORMLib;

namespace Streamstar;

public static class MMixer_AudioTrackExt
{
    public static List<IMAudioTrack> s_GetAudioTracks(this MMixerClass mm)
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
    
    public static void s_SetAudioTracks(this MMixerClass mm, List<IMAudioTrack> tracks)
    {
        mm.AudioTracksGetCount(out int trackCount);

        for (int i = 0; i < trackCount; i++)
        {
            mm.AudioTrackGetByIndex(i, out _, out IMAudioTrack track);
            mm.AudioTrackRemove(track);
        }
    }
    
    public static double[] s_GetAudioTrackGains(this MMixerClass mm, int channel = 0)
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