using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject obstacle1, obstacle2, obstacle3;
    [HideInInspector]
    public float obstacleSpawnInterval = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObstacle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>().isGameOver)
        {
            StopCoroutine("SpawnObstacle");
        }
    }

    private void SpawnObstacles()
    {
        int random = Random.Range(1, 4);

        if(random == 1)
        {
            Instantiate(obstacle1, new Vector3(transform.position.x, -0.5f, 0), Quaternion.identity);
        }
        else if (random == 2)
        {
            Instantiate(obstacle2, new Vector3(transform.position.x, -0.5f, 0), Quaternion.identity);
        }
        else if (random == 3)
        {
            Instantiate(obstacle3, new Vector3(transform.position.x, -0.5f, 0), Quaternion.identity);
        } 
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            SpawnObstacles();
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
}
