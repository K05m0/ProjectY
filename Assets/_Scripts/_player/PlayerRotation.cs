using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public void PlayerNormalRotation(float vertical, float horizontal , float rotateSpeed)
    {
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,rotateSpeed);
        }
    }

    public void PlayerFreeRotation(float rotateSpeed, Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed);
    }

}
