using UnityEngine;

public class escMenuHandler : MonoBehaviour
{
    public static GameObject escMenu;
    public static bool escapeMenuVisible;
    // Start is called before the first frame update

    void Start()
    {
        globals.escapeMenu = this.gameObject;
        escMenu = GameObject.FindGameObjectWithTag("escapeMenu");
        escapeMenuVisible = false;
        escMenu.gameObject.SetActive(false);
    }

    public void toggle()
    {
        if (escapeMenuVisible)
        {
            escapeMenuVisible = !escapeMenuVisible;
            escMenu.gameObject.SetActive(false);
        }
        else    
        {
            escapeMenuVisible = !escapeMenuVisible;
            escMenu.gameObject.SetActive(true);
        }
    }

    public void quit ()
    {
        Application.Quit();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle();

        }
    }
}
