using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 10f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 10f;
    [Tooltip("In m")] [SerializeField] float maxXFromCenter = 4.5f;
    [SerializeField] GameObject[] guns;
    
    [Header("Screen-position Based")]
    [Tooltip("In m")] [SerializeField] float yMax = 3.2f;
    [Tooltip("In m")] [SerializeField] float yMin = -3.2f;
	[SerializeField] float positionYawFactor = 8f;
	[SerializeField] float positionPitchFactor = -5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow;
    float yThrow;
	bool controlsDisabled = false;

    // Update is called once per frame
    void Update()
    {
        if (!controlsDisabled)
		{
			UpdateXPos();
			UpdateYPos();
			ProcessRotation();
			ProcessFiring();
		}
        
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            ParticleSystem.EmissionModule particles = gun.GetComponent<ParticleSystem>().emission;
            particles.enabled = isActive;
            
        }
    }

   

    // called by string reference
    private void DisableControls()
	{
		controlsDisabled = true;
	}

    private void ProcessRotation()
    {
        // pitch due to position and control throw
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void UpdateXPos()
    {
        // give value between -1 and 1
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * Time.deltaTime * xSpeed;
        float newPosition = transform.localPosition.x + xOffset;
        float clampedX = Mathf.Clamp(transform.localPosition.x + xOffset, -maxXFromCenter, maxXFromCenter);
        transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
    }

    private void UpdateYPos()
    {
        // give value between -1 and 1
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Time.deltaTime * ySpeed;
		float clampedY = Mathf.Clamp(transform.localPosition.y + yOffset, yMin, yMax);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedY, transform.localPosition.z);
    }
}
