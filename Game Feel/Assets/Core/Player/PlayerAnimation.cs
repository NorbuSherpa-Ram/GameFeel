using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer mySR;
    [SerializeField] SpriteRenderer myHatSR;

    [SerializeField] PlayerController player;
    [SerializeField] CinemachineImpulseSource impulsSource;

    [Header("VFX")]
    [SerializeField] ParticleSystem dustParticle;
    [SerializeField] ParticleSystem jumpParticle;

    [Header("Tilt Info")]
    [SerializeField] int  tiltAngle = 20;
    [SerializeField] float tiltSpeed = 5;
    [SerializeField] float hatTiltModifire = 5;

    [Header("Falling Info")]
    [SerializeField] private float yVelocity = 20;
    private Vector2 velocityBefore;

    private void OnEnable()
    {
        PlayerController.OnJump += JumpParticle;
    }
    private void OnDisable()
    {
        PlayerController.OnJump -= JumpParticle;
    }
    private void Update()
    {
        DustParticle();
        Tilt();
    }
    private void FixedUpdate()
    {
        velocityBefore = player.myRb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D other )
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (velocityBefore.y < -yVelocity)
            {
                jumpParticle.Play();
                impulsSource.GenerateImpulse();
            }
        }

    }
    void JumpParticle()
    {
        jumpParticle.Play();
    }
    private void DustParticle()
    {
        if (player.IsGrounded())
        {
            if (!dustParticle.isPlaying)
                dustParticle.Play();
        }
        else
        {
            if (dustParticle.isPlaying)
                dustParticle.Stop();
        }
    }

    private void Tilt()
    {
        int targetTilt;
        if (player.frameInput.Move.x < 0)
            targetTilt = tiltAngle;
        else if (player.frameInput.Move.x > 0)
            targetTilt = -tiltAngle;
        else
            targetTilt = 0;

        Quaternion currentCharacterRotation = mySR.transform.rotation;
        Quaternion newCharacterRotaion = Quaternion.Euler(currentCharacterRotation.eulerAngles.x, currentCharacterRotation.eulerAngles.y, targetTilt);
        mySR.transform.rotation = Quaternion.Lerp(currentCharacterRotation, newCharacterRotaion, tiltSpeed * Time.deltaTime);

        Quaternion currentHatRotaion = myHatSR.transform.rotation;
        Quaternion newHatRotaion = Quaternion.Euler(currentCharacterRotation.eulerAngles.x, currentCharacterRotation.eulerAngles.y, -targetTilt / hatTiltModifire);
        myHatSR.transform.rotation = Quaternion.Lerp(currentHatRotaion, newHatRotaion, tiltSpeed * Time.deltaTime);
    }
}
