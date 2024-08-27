using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MPLATFORMLib;
using Streamstar.U;
using Path = System.IO.Path;

//using Streamstar.U;
//using Path = System.IO.Path;

namespace Streamstar;

internal class Program
{
    public static void Main(string[] args)
    {
        //funguje
        /*-----------------------------
        MFileClass sf = new();
        sf.s_FileNameSet("f1.mp4");
        sf.s_Loop();
        sf.s_Play();

        MWebRTC_PluginClass sw = new();
        sw.s_Login();
        sf.s_Add_WebRTC_Plugin(sw);
        -------------------------------*/

        //MMixerClass _mixerA = new();

        CustomMixer _mixerA = new();
        
        MFileClass _source1 = new();
        MFileClass _source2 = new();
        
        //_mixerA.s_AddSource(_source1);
        //_mixerA.s_AddSource(_source2);
        _mixerA.AddSource(_source1);
        _mixerA.AddSource(_source2);

        MWebRTC_PluginClass _mWebrtc = new();

        PerformanceCounter _pc = new();
        PerfStats.Start(_pc);

        _source1.s_PrintProps();
        string _path = Path.Combine(U.Path.Assets(), "f1.mp4");
        string _path2 = Path.Combine(U.Path.Assets(), "piano.mp4");
        //Console.WriteLine("Using path => " + _path);
        
        _source1.s_FileNameSet(_path);
        _source1.s_Loop();
        _source1.s_Play();

        _source2.s_FileNameSet(_path2);
        _source2.s_Loop();
        _source2.s_Play();

        _mWebrtc.s_Login();
        
        _mWebrtc.s_PrintInfo();

        string _selection = "s1";
        while (!string.Equals(_selection, "x"))
        {
            Console.WriteLine("s1 - set source 1\ns2 - set source 2\n1 - toggle audio 1\n2 - toggle audio 2\nmixa - toggle audio mix a");
            _selection = Console.ReadLine();
            switch (_selection)
            {
                case "s1":
                    _mWebrtc.s_SetSource(_source1);
                    break;
                case "s2":
                    _mWebrtc.s_SetSource(_source2);
                    break;
                case "1":
                    //_source1.s_SetGain(_source1.s_GetGain() < 0 ? 0 : -99);
                    _mixerA.ToggleSourceGain(_source1);
                    break;
                case "2":
                    //_source2.s_SetGain(_source2.s_GetGain() < 0 ? 0 : -99);
                    _mixerA.ToggleSourceGain(_source2);
                    break;
                case "mixa":
                    _mixerA.ToggleSound();
                    break;
            }
        }
        
        //performance test
        /*
        for (int _j = 0; _j < 100; _j++)
        {
            for (int _i = 0; _i < 1000000; _i++)
            {
                _ = Math.Acos(546541) * Math.Asin(1513);
            }
            
            PerfLogger.LogFull(_pc);
        }
        */
        
        Console.Write("end");
        Console.Read();
    }
}