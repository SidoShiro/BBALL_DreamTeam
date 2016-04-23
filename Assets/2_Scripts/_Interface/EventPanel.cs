using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EventPanel : NetworkBehaviour
{
    [Header("References(Interface)")]
    [SerializeField]
    private GameObject readyText;
    [SerializeField]
    private GameObject steadyText;
    [SerializeField]
    private GameObject goText;
    [SerializeField]
    private AudioSource tackSound;

    [ClientRpc]
    public void Rpc_Ready()
    {
        StartCoroutine(Tackit(0.2f));
        StartCoroutine(Fadeit(readyText));
    }

    [ClientRpc]
    public void Rpc_Steady()
    {
        StartCoroutine(Tackit(0.2f));
        StartCoroutine(Fadeit(steadyText));
    }

    [ClientRpc]
    public void Rpc_Go()
    {
        tackSound.pitch = 1.0f;
        tackSound.Play();
        StartCoroutine(Fadeit(goText));
    }

    IEnumerator Tackit(float delay)
    {
        tackSound.pitch = 1.0f;
        tackSound.Play();
        yield return new WaitForSeconds(delay);
        tackSound.pitch = 1.5f;
        tackSound.Play();
    }

    IEnumerator Fadeit(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
    }
}


