using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject playerMenuPanel;
    public Component[] playerInputScripts;

    public bool isShowing;

    void Start()
    {
        playerMenuPanel = GameObject.Find("PlayerMenuPanel");
        isShowing = false;
        HidePlayerMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            if (isShowing)
            {
                ShowPlayerMenu();
            }
            else
            {
                HidePlayerMenu();
            }
            
        }
    }

    void ShowPlayerMenu()
    {
        ChangeScriptsState(false);
        playerMenuPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void HidePlayerMenu()
    {
        ChangeScriptsState(true);
        playerMenuPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ChangeScriptsState(bool newstate)
    {
        foreach(MonoBehaviour monoscript in playerInputScripts)
        {
            monoscript.enabled = newstate;
        }
    }
}
