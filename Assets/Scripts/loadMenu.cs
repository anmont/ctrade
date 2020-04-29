using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class loadMenu : MonoBehaviour
{
    public GameObject newGameBtn;
    public GameObject saveScroll;
    public GameObject loadSavedBtn;
    public GameObject tutorialBtn;
    public GameObject creditsBtn;
    public GameObject exitBtn;
    public GameObject listView;
    public bool saveMenu = false;
    void Start()
    {
        saveScroll.SetActive(false);

        Button btn = newGameBtn.GetComponent<Button>();
		btn.onClick.AddListener(loadNewGame);

        Button btnLoad = loadSavedBtn.GetComponent<Button>();
		btnLoad.onClick.AddListener(loadSavedGame);

        Button btnExit = exitBtn.GetComponent<Button>();
		btnExit.onClick.AddListener(exitGame);

        //get list of all saved games and preload objects in the list.
        //Application.persistentDataPath
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath.ToString() + "/saves");
        //Debug.Log(Application.persistentDataPath.ToString() + "/saves");

        FileInfo[] info = dir.GetFiles("*.trdr");
        //Debug.Log(info.Length.ToString() + " total files in list");
        foreach (FileInfo save in info)
        {
            GameObject savedGame = (GameObject)Instantiate(Resources.Load("loadUIGameSaveBtn"));
            savedGame.transform.SetParent(listView.transform, false);
            savedGame.GetComponentInChildren<Text>().text = save.Name;
            savedGame.GetComponent<Button>().onClick.AddListener(delegate{loadSave(savedGame);});

        }
    }
    // Start is called before the first frame update
    void loadNewGame()
    {
        StaticClass.saveFileName = "newgame";
        SceneManager.LoadScene("Default", LoadSceneMode.Single);
    }

    void loadSavedGame()
    {
        saveMenu = true;
        saveScroll.SetActive(true);

    }

    void exitGame()
    {
        Application.Quit();
    }

    void OnGUI()
    {
        if (saveMenu)
        {
            if (GUI.Button(new Rect(1070, 650, 50, 30), "Cancel"))
                cancelLoad();
        }

    }

    void cancelLoad()
    {
        saveScroll.SetActive(false);
        saveMenu = false;
    }

    void loadSave(GameObject sender)
    {
        //Debug.Log("File I will load in the loader = " + StaticClass.saveFileName);
        StaticClass.saveFileName = sender.GetComponentInChildren<Text>().text;
        Debug.Log("File I will load in the loader = " + StaticClass.saveFileName);
        saveScroll.SetActive(false);
        saveMenu = false;
        SceneManager.LoadScene("Default", LoadSceneMode.Single);
    }

}

public static class StaticClass 
{
    public static string saveFileName { get; set; }
}