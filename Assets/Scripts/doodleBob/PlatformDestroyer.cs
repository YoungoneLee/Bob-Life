using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision) {
            if(collision.transform.CompareTag("MovingPlatform"))
                Destroy(collision.transform.parent.gameObject);
            else
                Destroy(collision.gameObject);
    }
}