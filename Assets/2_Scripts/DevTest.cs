using UnityEngine;
using UnityEngine.Networking;

public class DevTest : NetworkBehaviour
{
    public GameObject DevCube;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdSpawn();
        }
    }
    
    [Command]
    public void CmdSpawn()
    {
        GameObject Newcube = (GameObject)Instantiate(DevCube, Vector3.up * 50.0f, Quaternion.identity);
        NetworkServer.Spawn(Newcube);
    }
}
