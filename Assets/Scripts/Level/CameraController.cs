using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset, originalCameraPosition;
    private float originalOrthographicSize;
    bool onof = false;
    public Camera c;

    void Start()
    {
        //c.enabled = true;
        //c.orthographic = true;
        originalCameraPosition = c.transform.position;
        originalOrthographicSize = c.orthographicSize;
    }

    void Update()
    {
        // Switch between camera modes
        if (Input.GetKeyDown(KeyCode.Z)) {
            onof = !onof;
            if(!onof) {
                // Camera returns to original position
                c.transform.position = originalCameraPosition;
                c.orthographicSize = originalOrthographicSize;
            }
        }

        if (onof) {
            // Camera follows the player with specified offset position
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); 
            c.orthographicSize = 7.5f;
        }
    }
}
