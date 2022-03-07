using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;

    private Rigidbody _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void ControlWalkingAnimation(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);    
        }
    }

    private Vector3 GetCurrentRotation(Vector3 direction)
    {
        float singleStep = RotationSpeed * Time.fixedDeltaTime;
        Vector3 rotationDirection = direction;
        Vector3 rotationStep = Vector3.RotateTowards(transform.forward, rotationDirection, singleStep, 0);
        return rotationStep;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 smoothDirection = new Vector3(horizontal, 0, vertical);
        
        float horizontalRaw = Input.GetAxisRaw("Horizontal");
        float verticalRaw = Input.GetAxisRaw("Vertical");

        ControlWalkingAnimation(horizontal, vertical);

        Vector3 currentRotation = GetCurrentRotation(smoothDirection);
        
        if (horizontalRaw != 0 || verticalRaw != 0)
            _rb.rotation = Quaternion.LookRotation(currentRotation);
        
        _rb.velocity = smoothDirection * Speed;
    }
}
