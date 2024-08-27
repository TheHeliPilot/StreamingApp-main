using System.Collections.Generic;
using System.Linq;
using MPLATFORMLib;

namespace Streamstar;

public struct SoundDouble
{
    public SoundDouble(double def, double chang)
    {
        defaultValue = def;
        mixedValue = chang;
    }

    public double defaultValue;
    public double mixedValue;
}

public class CustomMixer
{
    private double _mixerGain;
    public Dictionary<MFileClass, SoundDouble> sources;

    public CustomMixer()
    {
        sources = new Dictionary<MFileClass, SoundDouble>();
    }

    public CustomMixer(Dictionary<MFileClass, SoundDouble> sources)
    {
        this.sources = sources;
    }

    public CustomMixer(MFileClass[] sources)
    {
        this.sources = new Dictionary<MFileClass, SoundDouble>();
        foreach (MFileClass mFileClass in sources)
            this.sources.Add(mFileClass, new SoundDouble(mFileClass.s_GetGain(), mFileClass.s_GetGain()));
    }

    public void AddSource(MFileClass source)
    {
        try
        {
            sources.Add(source, new SoundDouble(source.s_GetGain(), source.s_GetGain()));
        }
        catch
        {
            sources.Add(source, new SoundDouble(0, 0));
        }
    }

    public void RemoveSource(MFileClass source)
    {
        sources.Remove(source);
    }

    public void AddMixer(CustomMixer mixer)
    {
        foreach (KeyValuePair<MFileClass, SoundDouble> keyValuePair in mixer.sources)
            sources.Add(keyValuePair.Key, keyValuePair.Value);
    }

    public void RemoveMixer(CustomMixer mixer)
    {
        foreach (KeyValuePair<MFileClass, SoundDouble> keyValuePair in mixer.sources)
            sources.Remove(keyValuePair.Key);
    }

    public void SetMixerGain(double value)
    {
        _mixerGain = value;
        _updateGains();
    }

    public void SetSourceGain(MFileClass source, double value)
    {
        sources[source] = new SoundDouble(value, 0);
        _updateGains();
    }

    public void ToggleSourceGain(MFileClass source)
    {
        sources[source] = new SoundDouble(sources[source].defaultValue < 0 ? 0 : -99, 0);
        _updateGains();
    }

    public void ToggleSound()
    {
        _mixerGain = _mixerGain < 0 ? 0 : -99;
        _updateGains();
    }

    private void _updateGains()
    {
        foreach (MFileClass sourcesKey in sources.Keys.ToList())
        {
            sources[sourcesKey] = new SoundDouble(sources[sourcesKey].defaultValue,
                sources[sourcesKey].defaultValue + _mixerGain);
            sourcesKey.s_SetGain(sources[sourcesKey].mixedValue);
        }
    }
}