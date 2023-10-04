using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation : MonoBehaviour
{

    public void PlayerNormalRotation(float horizontal, float vertical, float speed)
    {
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();

        if (direction != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        }
    }
    public void PlayerAimRotation(float speed, Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
    }
}
