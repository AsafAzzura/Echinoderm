using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransfrom;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    // Start is called before the first frame update
    private void Start()
    {
        transform.position = playerTransfrom.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransfrom != null)
        {
            //this will set limits to our camera
            float clampedX = Mathf.Clamp(playerTransfrom.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransfrom.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position /* from */, new Vector2(clampedX, clampedY) /* to where */, speed); // lerp moving object from point to point
        }
    }
}
