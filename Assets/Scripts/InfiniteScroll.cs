using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{

    [SerializeField]
    private int backgroundPosition;
    private int currentPosition;

    [SerializeField]
    private Camera Camera;

    private float spriteWidth;

    private void Awake()
    {
        if (this.Camera != null)
        {
            this.currentPosition = this.backgroundPosition;
            this.spriteWidth = this.GetComponent<SpriteRenderer>().bounds.size.x;
            SetBackgroundPosition();
        }
    }

    private void SetBackgroundPosition()
    {
        var newPositionX = this.spriteWidth * this.currentPosition;
        var newPositionY = this.transform.localPosition.y;
        var newPositionZ = this.transform.localPosition.z;
        var newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        this.transform.localPosition = newPosition;
    }

    void Update()
    {
        float distance = this.Camera.gameObject.transform.position.x - this.transform.position.x;
        if (distance > (this.spriteWidth * 1.5))
        {
            this.currentPosition += 3;
            SetBackgroundPosition();
        }

        if (distance < -(this.spriteWidth * 1.5))
        {
            this.currentPosition -= 3;
            SetBackgroundPosition();
        }

    }
}
