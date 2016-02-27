using UnityEngine;
using System.Collections;

public class PlaneTuto : MonoBehaviour
{
    bool toggleGUI;
    public string phrase;
    Rect labelRect;


    void OnTriggerEnter(Collider player) 
    {
        toggleGUI = true;
    }

    void OnTriggerExit(Collider other)
    {
        toggleGUI = false;
    }

    void OnGUI()
    {
        if (toggleGUI)
        {
            labelRect = GUILayoutUtility.GetRect(new GUIContent(phrase), "label");
            GUI.contentColor = Color.black;
            GUIStyle guiStyle = new GUIStyle();
            guiStyle.fontSize = 40;
            GUI.Label(labelRect, phrase, guiStyle);
        }
    }
}
