using UnityEngine;

/// <summary>
/// Used to store global variables client-side for multiple use
/// </summary>
public class SceneOverlord : MonoBehaviour
{
    private bool _isReceivingInputs;  //Used to disable inputs when bringing PlayerMenuPanel up

    public bool isReceivingInputs
    {
        get
        {
            return _isReceivingInputs;
        }

        set
        {
            _isReceivingInputs = value;
        }
    }
}
