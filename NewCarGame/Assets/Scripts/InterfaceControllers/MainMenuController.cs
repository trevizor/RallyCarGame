using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public RectTransform MainMenuContainer;
    public RectTransform SeasonSelectContainer;
    public RectTransform LevelSelectContainer;

    private SeasonSelectController seasonSelectController;

    public SeasonsData loadedSeasonsData;

	// Use this for initialization
	void Start () {
        string levelDataPath = Path.Combine(Application.streamingAssetsPath, "level-list.json");
        string loadedJson;

        TextAsset file = Resources.Load("level-list") as TextAsset;
        Debug.Log(file);
        loadedJson = file.ToString();

        SeasonsData jsonData = JsonUtility.FromJson<SeasonsData>(loadedJson);
        loadedSeasonsData = jsonData;

        seasonSelectController = SeasonSelectContainer.GetComponent<SeasonSelectController>();



        ShowMainMenu();
	}
	
	public void ShowMainMenu () {
        MainMenuContainer.gameObject.SetActive(true);
        SeasonSelectContainer.gameObject.SetActive(false);
        LevelSelectContainer.gameObject.SetActive(false);
    }

    public void ShowSeasonSelect ()
    {
        Debug.Log("SHOW SEASON SELECT");
        SeasonSelectContainer.gameObject.SetActive(true);
        seasonSelectController.updateLoadedSeasons(loadedSeasonsData.seasonList);
        MainMenuContainer.gameObject.SetActive(false);
        LevelSelectContainer.gameObject.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        SeasonSelectContainer.gameObject.SetActive(false);
        MainMenuContainer.gameObject.SetActive(false);
        LevelSelectContainer.gameObject.SetActive(true);
    }

}
