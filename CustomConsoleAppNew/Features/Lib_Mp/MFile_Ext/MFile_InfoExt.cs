using System;
using MPLATFORMLib;

namespace Streamstar;

public static class MFile_UtilsExt
{
    public static void s_PrintProps(this MFileClass mFile)
    {
        Console.WriteLine("\n PrintProps");

        mFile.PropsGetCount("", out int _nCount);

        for (int i = 0; i < _nCount; i++)
        {
            mFile.PropsGetByIndex("", i, out string k, out string v, out int id);
            Console.WriteLine("(" + id + ") " + k + ": \t\t" + v);
        }
    }
}