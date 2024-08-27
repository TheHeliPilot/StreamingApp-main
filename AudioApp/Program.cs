using System;
using System.IO;
using MPLATFORMLib;

// ReSharper disable InconsistentNaming

namespace Streamstar;

internal class Program
{
    public static void Main(string[] args)
    {
        CustomMixer soundMixerA = new();

        string path = Path.Combine(U.Path.Assets(), "f1.mp4");
        string path2 = Path.Combine(U.Path.Assets(), "piano.mp4");
        
        string urlHtml = Path.Combine(U.Path.Assets(), "index.html");
        string urlGradient = Path.Combine(U.Path.Assets(), "gradient-1080.jpg");
        string urlTransparent = Path.Combine(U.Path.Assets(), "transparent-1080-1px.png");

        MFileClass source1 = new();
        soundMixerA.AddSource(source1);
        source1.s_FileNameSet(path);
        source1.s_Loop();
        source1.s_Play();
        MFileClass source2 = new();
        soundMixerA.AddSource(source2);
        source2.s_FileNameSet(path2);
        source2.s_Loop();
        source2.s_Play();

        MWebRTC_PluginClass mWebrtc = new();
        mWebrtc.s_Login();
        mWebrtc.s_PrintInfo();

        string selection = "s1";
        while (!string.Equals(selection, "x"))
        {
            Console.WriteLine(
                "s1 - set source 1\ns2 - set source 2\n1 - toggle audio 1\n2 - toggle audio 2\nmixa - toggle audio mix a");
            selection = Console.ReadLine();
            switch (selection)
            {
                case "s1":
                    mWebrtc.s_SetSource(source1);
                    break;
                case "s2":
                    mWebrtc.s_SetSource(source2);
                    break;
                case "1":
                    //_source1.s_SetGain(_source1.s_GetGain() < 0 ? 0 : -99);
                    soundMixerA.ToggleSourceGain(source1);
                    break;
                case "2":
                    //_source2.s_SetGain(_source2.s_GetGain() < 0 ? 0 : -99);
                    soundMixerA.ToggleSourceGain(source2);
                    break;
                case "mixa":
                    soundMixerA.ToggleSound();
                    break;
            }
        }
    }
}