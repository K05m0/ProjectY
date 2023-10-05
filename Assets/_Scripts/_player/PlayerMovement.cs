using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float angleToSlope;
    [SerializeField] private float gravityPower;
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit slopeHit;
    private float verticalVelocity;

    public void Movement(float vertical, float horizontal, float speed, CharacterController cc)
    {
        //gravitation
        Gravity(cc);

        //Movement
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;
        cc.Move(direction * speed * Time.deltaTime);
    }

    private void Gravity(CharacterController cc)
    {
        verticalVelocity += Physics.gravity.y * Time.deltaTime * gravityPower;
        cc.Move(transform.up * verticalVelocity * Time.deltaTime);
    }
}
