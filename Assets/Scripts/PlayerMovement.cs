using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] InputAction dash;

    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] float dashStrength = 10f;
    [SerializeField] float extraGravityParam = 5f;

    [SerializeField] AudioClip mainThruster;
    [SerializeField] AudioClip rocketDash;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightDashParticles;
    [SerializeField] ParticleSystem leftDashParticles;

    bool dashRequqested;
    int maxDashes = 4;

    Rigidbody rb;
    [SerializeField] AudioSource thrustAudioSource;
    [SerializeField] AudioSource dashAudioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        dash.Enable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        DashCheck();
    }

    void FixedUpdate()
    {
        ThrustMovement();
        RotationMovement();
        DashMovement();

        AddExtraGravity();
    }
    
    void ThrustMovement()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!thrustAudioSource.isPlaying)
        {
            thrustAudioSource.PlayOneShot(mainThruster);
        }

        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
        
    }

    void StopThrusting()
    {
        thrustAudioSource.Stop();
        mainThrusterParticles.Stop();
    }

    private void RotationMovement()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            LeftRotation();
        }

        else if (rotationInput > 0)
        {
            RightRotation();
        }

        else
        {
            StopRotation();
        }
    }

    private void LeftRotation()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * 1f * rotationStrength * Time.deltaTime);
        rb.freezeRotation = false;

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
    }

    private void RightRotation()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * -1f * rotationStrength * Time.deltaTime);
        rb.freezeRotation = false;

        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }
    }

    void StopRotation()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void DashCheck()
    {
        if (dash.WasPressedThisFrame())
        {
            dashRequqested = true;
            maxDashes--;
        }
    }

    void DashMovement()
    {
        if (dashRequqested == true && maxDashes>0)
        {
            dashAudioSource.PlayOneShot(rocketDash);

            rightDashParticles.Play();
            leftDashParticles.Play();

            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.AddRelativeForce(Vector3.up * dashStrength * Time.fixedDeltaTime, ForceMode.Impulse);

            dashRequqested = false;
        }
    }

    void AddExtraGravity()
    {
        rb.AddForce(Vector3.down * extraGravityParam);
    }
}
