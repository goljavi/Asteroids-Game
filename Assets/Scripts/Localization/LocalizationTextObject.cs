using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationTextObject {
    public string language;
    public string textKey;
    public string textValue;

    public static LocalizationTextObject ToObject(List<object> obj)
    {
        return new LocalizationTextObject() { language = obj[0].ToString(), textKey = obj[1].ToString(), textValue = obj[2].ToString() };
    }
}
