using MPLATFORMLib;

namespace Streamstar;

public static class IMAudioTrack_Ext
{
    public static double s_GetGain(this IMAudioTrack track)
    {
        track.TrackGainGet(0, out double gain);
        return gain;
    }
}