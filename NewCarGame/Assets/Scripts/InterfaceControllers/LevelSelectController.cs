using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    public List<LevelMeta> loadedLevels;

    public GameObject templateLevelButton;
    public GameObject LevelButtonContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void updateLoadedLevels(List<LevelMeta> _levels)
    {
        //TODO: remove previous loaded levels
        foreach (Transform child in LevelButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        loadedLevels = _levels;
        foreach (LevelMeta _season in loadedLevels)
        {
            GameObject newSeasonButton = Instantiate(templateLevelButton, LevelButtonContainer.transform);
            LevelButton levelButton = newSeasonButton.GetComponent<LevelButton>();
            levelButton.SetTargetLevel(_season);
        }
    }

    

}
