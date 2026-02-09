using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Camera : NetworkBehaviour
{
    public Transform playerBody;
    public Camera playerCamera;
    public AudioListener audioListener;

    public float mouseSens = 600f;
    private float yRotation = 0f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            if (playerCamera != null) playerCamera.enabled = false;
            if (audioListener != null) audioListener.enabled = false;
            enabled = false;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerBody == null)
        {
            playerBody = transform.root;
        }
    }
    private void Update()
    {
        if (!IsOwner) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
