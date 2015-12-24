using UnityEngine;

/// <summary>
/// Used to keep GlobalGM loaded trough scenes and enables scene specific components
/// </summary>
public class GlobalEnabler : MonoBehaviour
{
    public GameObject PlayerUI; //Used to not load in main menu

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);   //This gameOject will persist trough scenes
        if (FindObjectsOfType(GetType()).Length > 1)    //This prevent duplicates
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Triggers when a scene is loaded
    /// </summary>
    /// <param name="sceneID">ID of the loaded scene</param>
    void OnLevelWasLoaded(int sceneID)
    {
        //Disables player UI if scene is MainMenu
        if(sceneID == 0)
        {
            PlayerUI.SetActive(false);
        }
        else
        {
            PlayerUI.SetActive(true);
        }
    }

}
