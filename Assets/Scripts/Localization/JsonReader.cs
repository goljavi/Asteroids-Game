using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReader {
    string _file;

    public JsonReader(string file)
    {
        _file = file;
    }

    public List<LocalizationTextObject> LoadFromDisk()
    {
        List<LocalizationTextObject> list = new List<LocalizationTextObject>();
        foreach (var item in (List<object>)MiniJSON.Json.Deserialize(File.ReadAllText("Save/" + _file + ".json")))
        {
            list.Add(LocalizationTextObject.ToObject((List<object>)item));
        }
        return list;
    }
}
