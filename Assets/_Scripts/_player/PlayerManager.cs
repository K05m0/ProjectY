using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerControllHolder controllHolder;
    PlayerRotation rotation;
    CharacterController controller;
    PlayerMovement movement;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;

    [SerializeField] private float normalRotationSpeed;
    [SerializeField] private float aimRotationSpeed;

    private void Awake()
    {
        controllHolder = GetComponent<PlayerControllHolder>();
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
        rotation = GetComponent<PlayerRotation>();
    }

    private void Update()
    {
        movement.Gravity();

        if (Input.GetKey(controllHolder.sprintKeyCode))
        {
            movement.Movement(controllHolder.horizontal, controllHolder.vertical, sprintSpeed, controller);
        }
        else
        {
            movement.Movement(controllHolder.horizontal, controllHolder.vertical, walkSpeed, controller);
        }

        if (Input.GetKey(controllHolder.aimKeyCode))
        {
            if (controllHolder.GetMousePosition().succes)
                rotation.PlayerAimRotation(aimRotationSpeed, controllHolder.GetMousePosition().position);
            else
                rotation.PlayerNormalRotation(controllHolder.horizontal, controllHolder.vertical, normalRotationSpeed);
        }
        else
        {
            rotation.PlayerNormalRotation(controllHolder.horizontal, controllHolder.vertical, normalRotationSpeed);
        }
    }
}
