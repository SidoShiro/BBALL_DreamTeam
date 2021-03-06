﻿using UnityEngine;

/// <summary>
/// This script is used to control rotation with mouse.
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Header("References(Player)")]
    [SerializeField]
    private PlayerMenu playerMenu;
    [SerializeField]
    private GameObject targetObject;         //Object to rotate

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
    /// Trigerred every frame
    /// </summary>
    void Update()
    {
        if (playerMenu.isOff)
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