using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class SeasonMeta
{

    public string title;
    public List<LevelMeta> levelList = new List<LevelMeta>();

    public SeasonMeta(string _title)
    {
        title = _title;
    }

}
