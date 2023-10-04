using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravityStrength;
    private float verticalVelocity;

    public void Gravity()
    {
        verticalVelocity += Physics.gravity.y * Time.deltaTime * gravityStrength;
    }
    public void Movement(float horizontal, float vertical, float speed, CharacterController controller)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection.Normalize();

        controller.Move(moveDirection * Time.deltaTime);
        controller.Move(transform.up * verticalVelocity * Time.deltaTime);
    }
}
