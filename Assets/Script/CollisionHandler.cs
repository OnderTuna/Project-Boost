using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource myAudio;
    [SerializeField] float delay = 2.0f;
    [SerializeField] AudioClip crushSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crushEffect;
    [SerializeField] ParticleSystem successEffect;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hello mate!");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        crushEffect.Play();
        myAudio.PlayOneShot(crushSound);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), delay);
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        successEffect.Play();
        myAudio.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
