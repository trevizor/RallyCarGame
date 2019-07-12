using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public LevelMeta targetLevel;

    public Text levelName;


    public void SetTargetLevel(LevelMeta _level)
    {
        targetLevel = _level;
        levelName.text = targetLevel.title;
    }


    public void LoadLevel()
    {
        SceneManager.LoadScene(targetLevel.scene);
    }
}
