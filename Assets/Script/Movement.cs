using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRb;
    AudioSource myAudio;
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem boosterEffect;
    [SerializeField] ParticleSystem leftThrustEffect;
    [SerializeField] ParticleSystem rightThrustEffect;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ProcessLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ProcessRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        rightThrustEffect.Stop();
        leftThrustEffect.Stop();
    }

    private void ProcessRightRotation()
    {
        ApplyRotation(-rotateThrust);
        if (!leftThrustEffect.isPlaying)
        {
            leftThrustEffect.Play();
        }
    }

    private void ProcessLeftRotation()
    {
        ApplyRotation(rotateThrust);
        if (!rightThrustEffect.isPlaying)
        {
            rightThrustEffect.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        myRb.freezeRotation = true;
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        myRb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        myAudio.Stop();
        boosterEffect.Stop();
    }

    void StartThrusting()
    {
        myRb.AddRelativeForce(thrust * Time.deltaTime * Vector3.up);
        if (!boosterEffect.isPlaying)
        {
            boosterEffect.Play();
        }
        if (!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(mainEngine);
        }
    }
}
