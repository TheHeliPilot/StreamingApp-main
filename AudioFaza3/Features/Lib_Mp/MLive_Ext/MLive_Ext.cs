using MPLATFORMLib;

namespace Streamstar;

public static class MLive_Ext
{
    public static string s_GetMp(this MLiveClass mlc)
    {
        mlc.ObjectNameGet(out string name);
        return "mp://" + name;
    }
}