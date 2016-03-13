using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class JoinTuto : MonoBehaviour
{
    void Start()
    {

    }

    void TutoJoin(int scene = 3)
    {
        SceneManager.LoadScene(scene); //ça me parait explicite, Attention à bien mettre
                                       // la scene qui va se lancer (sinon lance par defaut login)
    }


}
