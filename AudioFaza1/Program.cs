using System;
using System.IO;
using MPLATFORMLib;

namespace Streamstar;

internal class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(U.Path.Assets(), "f1.mp4");
        string path2 = Path.Combine(U.Path.Assets(), "piano.mp4");
        //string pathTransparent = Path.Combine(U.Path.Assets(), "gradient.jpg");

        MFileClass backgroundFile1 = new();
        backgroundFile1.s_FileNameSet(path);
        backgroundFile1.s_Play();
        backgroundFile1.s_Loop();

        MFileClass backgroundFile2 = new();
        backgroundFile2.s_FileNameSet(path2);
        backgroundFile2.s_Play();
        backgroundFile2.s_Loop();
        
        MFileClass layer0 = new();
        layer0.s_FileNameSet(path2);
        layer0.s_Play();
        layer0.s_Loop();
        
        MFileClass layer1 = new();
        layer1.s_FileNameSet(path2);
        layer1.s_Play();
        layer1.s_Loop();
        
        /*
        MFOverlayHTMLClass overlay1 = new();
        overlay1.BrowserPageLoad(pathHtml);
        overlay1.PropsSet("transparency", "true");
        leftFile.PluginsAdd(overlay1, 0);
        */

        MMixerClass mixer1 = new();
        mixer1.s_CreateElement_Main();
        mixer1.s_SetStream_Main(backgroundFile1);
        mixer1.s_SetVideoFormat(1920, 1080, 50);
        
        mixer1.s_CreateElement_Layer(0, "w=0.4 h=0.4 x=-0.25 y=0.25");
        mixer1.s_SetStream_Layer(0, layer0);
        mixer1.s_CreateElement_Layer(1, "w=0.4 h=0.4 x=0.25 y=0.25");
        mixer1.s_SetStream_Layer(1, layer1);

        mixer1.s_Start();

        MWebRTC_PluginClass mWebRtc = new();
        mWebRtc.s_SetSource(mixer1);
        mWebRtc.s_Login();
        mWebRtc.s_PrintInfo();
        
        string input = "";
        while (input != "x")
        {
            switch (input)
            {
                case "1":
                    mixer1.s_SetStream_Main(backgroundFile1);
                    break;
                case "2":
                    mixer1.s_SetStream_Main(layer0);
                    break;
            }
            input = Console.ReadLine();
        }

        Console.WriteLine("End");
        Console.ReadKey();
    }
}