using UnityEngine;

/// <summary>
/// This script is used to control rotation with mouse.
/// </summary>
public class PlayerLook : MonoBehaviour
{
    public GameObject targetObject;
    private SceneOverlord sceneOverlord;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    /// <summary>
    /// Triggered when script is loaded
    /// </summary>
    void Awake()
    {
        sceneOverlord = GameObject.Find("SceneGM").GetComponent<SceneOverlord>();
    }

    void Update()
    {
        if (sceneOverlord.isReceivingInputs)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = targetObject.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                targetObject.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                targetObject.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                targetObject.transform.localEulerAngles = new Vector3(-rotationY, targetObject.transform.localEulerAngles.y, 0);
            }
        }
    }
}