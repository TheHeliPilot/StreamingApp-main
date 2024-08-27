using System;
using System.Collections.Generic;
using System.Linq;
using MPLATFORMLib;

namespace Streamstar;

public struct SoundDouble
{
    public SoundDouble(double @default, double mixed)
    {
        defaultValue = @default;
        mixedValue = mixed;
    }

    public double defaultValue;
    public double mixedValue;
}

public class SoundMixer
{
    private double _mixerGain;
    public double _gainMod = 1;
    private Dictionary<object, SoundDouble> sources;
    private List<SoundMixer> controllingMixers;

    public SoundMixer()
    {
        sources = new Dictionary<object, SoundDouble>();
    }

    public SoundMixer(List<MFileClass> mfiles)
    {
        foreach (MFileClass mFileClass in mfiles)
        {
            SoundDouble sd = new SoundDouble(mFileClass.s_GetGain(), 0);
            sources.Add(mFileClass, sd);
        }
    }
    
    public SoundMixer(List<MMixerClass> mmixers)
    {
        foreach (MMixerClass mMixerClass in mmixers)
        {
            SoundDouble sd = new SoundDouble(mMixerClass.s_GetGain(), 0);
            sources.Add(mMixerClass, sd);
        }
    }
    
    public SoundMixer(List<MFileClass> mfiles, List<MMixerClass> mmixers)
    {
        foreach (MFileClass mFileClass in mfiles)
        {
            SoundDouble sd = new SoundDouble(mFileClass.s_GetGain(), 0);
            sources.Add(mFileClass, sd);
        }
        foreach (MMixerClass mMixerClass in mmixers)
        {
            SoundDouble sd = new SoundDouble(mMixerClass.s_GetGain(), 0);
            sources.Add(mMixerClass, sd);
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
    public void AddSource(MMixerClass source)
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
    public void RemoveSource(MMixerClass source)
    {
        sources.Remove(source);
    }
    public void AddSoundMixer(SoundMixer mixer)
    {
        controllingMixers.Add(mixer);
    }
    public void RemoveSoundMixer(SoundMixer mixer)
    {
        controllingMixers.Remove(mixer);
    }
    
    public void SetSourceGain(MFileClass source, double value)
    {
        sources[source] = new SoundDouble(value, 0);
        UpdateGains();
    }
    public void SetSourceGain(MMixerClass source, double value)
    {
        sources[source] = new SoundDouble(value, 0);
        UpdateGains();
    }
    public void SetSourceGain(SoundMixer source, double value)
    {
        source._gainMod = value;
        source.UpdateGains();
    }
    public void ToggleSourceGain(MFileClass source)
    {
        sources[source] = new SoundDouble(sources[source].defaultValue < 0 ? 0 : -99, 0);
        UpdateGains();
    }
    public void ToggleSourceGain(MMixerClass source)
    {
        sources[source] = new SoundDouble(sources[source].defaultValue < 0 ? 0 : -99, 0);
        UpdateGains();
    }

    public void ToggleSound()
    {
        _mixerGain = _mixerGain < 0 ? 0 : -99;
        UpdateGains();
    }
    
    public void UpdateGains()
    {
        foreach (object sourcesKey in sources.Keys.ToList())
        {
            sources[sourcesKey] = new SoundDouble(sources[sourcesKey].defaultValue,
                sources[sourcesKey].defaultValue + _mixerGain * _gainMod);
            try
            {
                MFileClass mf = (MFileClass)sourcesKey;
                mf.s_SetGain(sources[sourcesKey].mixedValue);
            }
            catch
            {
                MMixerClass mm = (MMixerClass)sourcesKey;
                mm.s_SetGain(sources[sourcesKey].mixedValue);
            }
        }
    }
}