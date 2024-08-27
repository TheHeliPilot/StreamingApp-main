using System.Collections;
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
    public Dictionary<MFileClass, SoundDouble> sources;
    private double _mixerGain = 0;

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
        foreach (MFileClass _mFileClass in sources)
        {
            this.sources.Add(_mFileClass, new SoundDouble(_mFileClass.s_GetGain(), _mFileClass.s_GetGain()));
        }
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
        foreach (KeyValuePair<MFileClass,SoundDouble> _keyValuePair in mixer.sources)
        {
            sources.Add(_keyValuePair.Key, _keyValuePair.Value);
        }
    }
    
    public void RemoveMixer(CustomMixer mixer)
    {
        foreach (KeyValuePair<MFileClass,SoundDouble> _keyValuePair in mixer.sources)
        {
            sources.Remove(_keyValuePair.Key);
        }
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
        foreach (MFileClass _sourcesKey in sources.Keys.ToList())
        {
            sources[_sourcesKey] = new SoundDouble(sources[_sourcesKey].defaultValue, sources[_sourcesKey].defaultValue + _mixerGain);
            _sourcesKey.s_SetGain(sources[_sourcesKey].mixedValue);
        }
    }
}