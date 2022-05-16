using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2.0f;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
