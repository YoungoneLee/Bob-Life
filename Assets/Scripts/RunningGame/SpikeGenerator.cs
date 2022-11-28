using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{

    public GameObject spike;

    [HideInInspector]
    public float MinSpeed = 5;
    [HideInInspector]
    public float MaxSpeed = 20;
    [HideInInspector]
    public float currentSpeed = 0;

    public float SpeedMultiplier;
    void Awake()
    {
        currentSpeed = MinSpeed;
        generateSpike();
    }

    public void GenerateNextSpikeWithGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke("generateSpike", randomWait);
    }

    public void generateSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);

        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    void Update()
    {
        if (currentSpeed < MaxSpeed) currentSpeed += SpeedMultiplier;
    }
}
