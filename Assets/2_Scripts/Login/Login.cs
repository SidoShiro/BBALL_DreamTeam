using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    public InputField login;
    public InputField pass;
    public Text Wrong_pass;
    public static string[,] check_list_login = new string[5, 2];  // 5 Comptes sont créées pour l'instant.

	// Use this for initialization
	void Start () {
        check_list_login[0, 0] = "Alazred";
        check_list_login[1, 0] = "Hastur";
        check_list_login[2, 0] = "Chaton";
        check_list_login[3, 0] = "Robin";

        for (int i = 0; i < 4; i++)
        {
            check_list_login[i, 1] = "bball_admin";            //Même MDP pour nous 4 pour l'instant.
        }

        check_list_login[4, 0] = "guest";                     //Compte invité.
        check_list_login[4, 1] = "azerty";

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("return"))
        {
            for (int i = 0; i < check_list_login.GetLength(0); i++)
            {
                if (login.text == check_list_login[i,0])
                {
                    if (pass.text == check_list_login[i,1])
                    {
                        SceneManager.LoadScene("_MainMenu");
                    }
                }
            }
            Wrong_pass.text = "Wrong password";
        }
	
	}
}
