using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Vector3 offset;
    public float smoothTime = 1f;
    public float maxSplitDistance = 10f;

    private Camera cam;
    private GameObject splitCameraObj;
    private Vector3 velocity;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        // Check if both players are within the split distance
        if (player1 != null && player2 != null && Vector3.Distance(player1.position, player2.position) <= maxSplitDistance)
        {
            Vector3 centerPoint = (player1.position + player2.position) / 2f;

            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

            if (splitCameraObj != null)
            {
                //splitCameraObj.GetComponent<Camera>().enabled = false;
                cam.rect = new Rect(0f, 0f, 1f, 1f);
                Destroy(splitCameraObj);
            }

        }
        else
        {
            // Split the camera if players are too far apart
            if (splitCameraObj == null)
            {
                SplitCamera();
            }

        }
    }

    void SplitCamera()
    {
        // Calculate the positions for the split cameras
        Vector3 player1Pos = player1 != null ? player1.position + offset : transform.position;
        Vector3 player2Pos = player2 != null ? player2.position + offset : transform.position;

        // Set the positions of the cameras to each player's position
        transform.position = player1Pos;
        cam.rect = new Rect(0f, 0f, 0.5f, 1f);

        splitCameraObj = new GameObject("SplitCamera");
        Camera splitCamera = splitCameraObj.AddComponent<Camera>();

        splitCamera.transform.position = player2Pos;
        splitCamera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
    }
}