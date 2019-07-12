using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSelectController : MonoBehaviour
{
    public List<SeasonMeta> loadedSeasons;

    public GameObject templateSeasonButton;
    public GameObject SeasonButtonContainer;
    public MainMenuController mainMenuController;
    public LevelSelectController levelSelectController;
    public GameObject levelSelectGO;
    // Start is called before the first frame update

    public void updateLoadedSeasons (List<SeasonMeta> _seasons)
    {
        mainMenuController = GameObject.Find("MainMenuController").GetComponent<MainMenuController>();
        levelSelectController = levelSelectGO.GetComponent<LevelSelectController>();
        foreach (Transform child in SeasonButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        loadedSeasons = _seasons;
        foreach (SeasonMeta _season in loadedSeasons)
        {
            GameObject newSeasonButton = Instantiate(templateSeasonButton, SeasonButtonContainer.transform);
            SeasonButton seasonButton = newSeasonButton.GetComponent<SeasonButton>();
            seasonButton.mainMenuController = mainMenuController; //TODO: fazer um setter mas foda-se por enquanto
            seasonButton.levelSelectController = levelSelectController;
            seasonButton.SetTargetSeason(_season);
        }
    }

}
