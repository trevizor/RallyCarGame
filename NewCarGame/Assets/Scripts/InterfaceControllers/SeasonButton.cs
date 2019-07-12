using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonButton : MonoBehaviour
{
    public SeasonMeta targetSeason;
    public LevelSelectController levelSelectController;
    public MainMenuController mainMenuController;


    public Text levelName;

    public void SetTargetSeason(SeasonMeta _season)
    {
        targetSeason = _season;
        levelName.text = targetSeason.title;
    }


    public void LoadLevelSelectForSeason ()
    {

        levelSelectController.updateLoadedLevels(targetSeason.levelList);
        mainMenuController.ShowLevelSelect();
    }
}
