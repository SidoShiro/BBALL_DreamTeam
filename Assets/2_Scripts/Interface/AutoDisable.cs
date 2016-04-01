using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public void enableOrDisable(GameObject obj, bool state)
    {
        obj.SetActive(state);
    }
}
