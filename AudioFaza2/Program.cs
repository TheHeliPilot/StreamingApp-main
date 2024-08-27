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

        MMixerClass contentMixer = new();
        contentMixer.s_CreateElement_Main();
        contentMixer.s_SetStream_Main(backgroundFile1);
        contentMixer.s_SetVideoFormat(1920, 1080, 50);
        contentMixer.s_Start();
        
        MMixerClass pgm1 = new();
        pgm1.s_CreateElement_Main();
        pgm1.s_SetStream_Main(contentMixer);
        pgm1.s_SetVideoFormat(1920, 1080, 50);

        pgm1.s_CreateElement_Layer(0, "w=0.4 h=0.4 x=-0.25 y=0.25");
        pgm1.s_SetStream_Layer(0, layer0);
        pgm1.s_CreateElement_Layer(1, "w=0.4 h=0.4 x=0.25 y=0.25");
        pgm1.s_SetStream_Layer(1, layer1);
        pgm1.s_Start();
        
        MMixerClass pgm2 = new();
        pgm2.s_CreateElement_Main();
        pgm2.s_SetStream_Main(contentMixer);
        pgm2.s_SetVideoFormat(1920, 1080, 50);
        pgm2.s_Start();

        MWebRTC_PluginClass mWebRtc = new();
        mWebRtc.s_SetSource(pgm1);
        mWebRtc.s_Login();
        mWebRtc.s_PrintInfo();
        
        string input = "";
        while (input != "x")
        {
            Console.WriteLine("1-pgm1, 2-pgm2");
            switch (input)
            {
                case "1":
                    mWebRtc.s_SetSource(pgm1);
                    break;
                case "2":
                    mWebRtc.s_SetSource(pgm2);
                    break;
            }
            input = Console.ReadLine();
        }

        Console.WriteLine("End");
        Console.ReadKey();
    }
}