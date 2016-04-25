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

    void OnDisable()
    {
        StopAllCoroutines();
        rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// Called when mouse hovers button
    /// </summary>
    public void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(UpScale());
    }

    /// <summary>
    /// Called when mouse leave button
    /// </summary>
    public void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(DownScale());
    }

    IEnumerator UpScale()
    {
        while (rectTransform.localScale.x < scale - 0.01f)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, new Vector3(scale, scale, scale), 0.2f);
            yield return null;
        }
    }

    IEnumerator DownScale()
    {
        while (rectTransform.localScale.x > 1.0f + 0.01f)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
            yield return null;
        }
    }
}
