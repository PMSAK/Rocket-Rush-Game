using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] float loadDelay = 0.5f;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool iscontrollable = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!iscontrollable)
        {
            return;
        }

        else
        {

            if (collision.gameObject.tag == "Friendly")
            {
                Debug.Log("Started");
            }

            else if (collision.gameObject.tag == "Finish")
            {
                FinishSequence();
            }

            else
            {
                CrashSequence();
            }

        } 
    }

    private void CrashSequence()
    {
        iscontrollable = false;

        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);

        explosionParticles.Play();

        GetComponent<PlayerMovement>().enabled = false;

        Invoke("RestartLevel", loadDelay);
    }

    private void FinishSequence()
    {
        iscontrollable = false;

        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);

        successParticles.Play();

        GetComponent<PlayerMovement>().enabled = false;

        Invoke("NextLevel", loadDelay);
        
    }

    void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void NextLevel()
    {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    int nextScene = currentScene+1;

    SceneManager.LoadScene(nextScene);
    }
}
