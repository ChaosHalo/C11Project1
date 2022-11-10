using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public Vector2 SpeedTime = new Vector2(1f, 1f);
    private float textUnitSizeX;
    private float textUnitSizeY;
    private float positionZ;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;


        Sprite sprite = transform.GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textUnitSizeY = texture.height / sprite.pixelsPerUnit;
        positionZ = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delataMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(delataMovement.x * SpeedTime.x, delataMovement.y * SpeedTime.y,0);
        lastCameraPosition = cameraTransform.position;
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x)> textUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, positionZ);
        }
        if (Mathf.Abs(cameraTransform.position.y - transform.position.y) > textUnitSizeX)
        {
            float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textUnitSizeY;
            transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPositionY, positionZ);
        }
    }
}
