using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public Camera cam;
    public float buff = 1f;
    public int platformCount = 300;
    // Start is called before the first frame update
    void Start()
    {
        // BoxCollider2D collider = platformPrefab.GetComponent<BoxCollider2D>();
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height*cam.aspect;
        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < platformCount; i++) {
            spawnPosition.y += Random.Range(.5f, 1.0f) + buff;
            spawnPosition.x = Random.Range(-width, width);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
