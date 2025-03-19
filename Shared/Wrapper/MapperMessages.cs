using Shared.Enums;

namespace Shared.Wrapper;

public class MapperMessages
{

    public static string Map(string arMsg,string enMsg, AvailableLanguage lang=AvailableLanguage.EN)
    {
        if (lang == AvailableLanguage.AR) return arMsg;
        else if (lang == AvailableLanguage.AR) return enMsg;
        else return enMsg;
    }
    


}