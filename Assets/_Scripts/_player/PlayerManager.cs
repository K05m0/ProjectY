using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerControllHolder controllHolder;
    private CharacterController controller;
    private PlayerRotation rotation;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float freeRotationSpeed;
    [SerializeField] private float normalRotationSpeed;

    private float movementSpeed;
    [SerializeField] private MoveState playerMoveState;
    [SerializeField] private AimState playerAimState;


    private void Awake()
    {
        rotation = GetComponent<PlayerRotation>();
        movement = GetComponent<PlayerMovement>();
        controllHolder = GetComponent<PlayerControllHolder>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        AimStateController();
        MoveStateController();

        if (playerAimState == AimState.aim)
        {
            rotation.PlayerFreeRotation(freeRotationSpeed, controllHolder.GetMousePosition().position);
        }
        else if(playerAimState == AimState.notAim)
        {
            rotation.PlayerNormalRotation(controllHolder.vertical, controllHolder.horizontal, normalRotationSpeed);
        }
    }
    private void FixedUpdate()
    {
        movement.Movement(controllHolder.vertical, controllHolder.horizontal, movementSpeed, controller);
    }

    private void MoveStateController()
    {
        if (Input.GetKey(controllHolder.runKeyCode))
        {
            playerMoveState = MoveState.Run;
            movementSpeed = runSpeed;
        }
        else
        {
            playerMoveState = MoveState.Walk;
            movementSpeed = walkSpeed;
        }
    }
    private void AimStateController()
    {
        if (Input.GetKey(controllHolder.freeAimKeyCode))
        {
            playerAimState = AimState.aim;
        }
        else
        {
            playerAimState = AimState.notAim;
        }
    }
    private enum AimState { aim, notAim }
    private enum MoveState { Run, Walk }
}
