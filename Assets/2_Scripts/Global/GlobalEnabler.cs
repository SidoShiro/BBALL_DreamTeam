using UnityEngine;

/// <summary>
/// Used to keep GlobalGM loaded trough scenes
/// </summary>
public class GlobalEnabler : MonoBehaviour
{
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

}
