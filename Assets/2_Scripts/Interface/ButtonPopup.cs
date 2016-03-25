using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script is used to make a button bigger on mouseover, making the UI responsive to inputs
/// </summary>
public class ButtonPopup : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;    //Corresponding transform to scale Up/Down
    [SerializeField]
    private float scale = 1.2f;             //Scale factor

    //Local
    private bool IsPopped = false;

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        PopUp();
    }

    /// <summary>
    /// Makes the button Lerp to target according to IsPopped
    /// </summary>
    private void PopUp()
    {
        Vector3 target;
        if (IsPopped)
        {
            target = new Vector3(scale, scale, scale);
        }
        else
        {
            target = new Vector3(1.0f, 1.0f, 1.0f);
        }
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, target, 0.2f);
    }

    /// <summary>
    /// Called when mouse hovers button
    /// </summary>
    public void OnMouseEnter()
    {
        IsPopped = true;
    }

    /// <summary>
    /// Called when mouse leave button
    /// </summary>
    public void OnMouseExit()
    {
        IsPopped = false;
    }
}
