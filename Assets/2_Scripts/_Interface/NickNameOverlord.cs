using UnityEngine;
using UnityEngine.UI;

//TODO : Comment
public class NickNameOverlord : MonoBehaviour
{
    [SerializeField]
    private InputField NickNameInputField;

    void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefProperties.NickName))
        {
            NickNameInputField.text = PlayerPrefs.GetString(PlayerPrefProperties.NickName);
        }
        else
        {
            NickNameInputField.text = "404";
        }
    }

    public void UpdateNickName()
    {
        PlayerPrefs.SetString(PlayerPrefProperties.NickName, NickNameInputField.text);
    }
}
