using UnityEngine;
using UnityEngine.Networking;

public class NMHEnabler : MonoBehaviour
{
    public CustomNMHUD customNMH;

    void Start()
    {
        customNMH.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            customNMH.enabled = !customNMH.enabled;
        }
    }
}
