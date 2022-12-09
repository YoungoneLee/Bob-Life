using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleManager : MonoBehaviour
{
    public GameObject doodlePlatform;
    public GameObject movingPlatform;
    public GameObject reverseMovePlat;
    public Camera cam;
    public float buff = 1f;
    public int platformCount = 500;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height*cam.aspect;
        Vector3 spawnPosition = new Vector3();
        Vector3 spawnPositionR = new Vector3();


        for (int i = 0; i < platformCount; i++) {
            spawnPosition.y += Random.Range(1.0f, 2.0f) + buff;
            spawnPosition.x = Random.Range(-width, width);
            spawnPositionR.x = Random.Range(-width, width);
            spawnPositionR.y += Random.Range(2f, 5f) + buff;
            if(PlayerPrefs.GetInt("jump") < 10) {
                Instantiate(doodlePlatform, spawnPosition, Quaternion.identity);
            } else if (PlayerPrefs.GetInt("jump") > 10 && PlayerPrefs.GetInt("jump") < 20) {
                Instantiate(doodlePlatform, spawnPositionR, Quaternion.identity);
                Instantiate(movingPlatform, spawnPosition, Quaternion.identity);
            } else {
                Instantiate(movingPlatform, spawnPosition, Quaternion.identity);
                Instantiate(reverseMovePlat, spawnPositionR, Quaternion.identity);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
