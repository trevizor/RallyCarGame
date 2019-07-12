using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class LevelMeta
{

    public string title;
    public string scene;

    public LevelMeta (string _title, string _sceneTitle)
    {
        title = _title;
        scene = _sceneTitle;
    }
    
}
