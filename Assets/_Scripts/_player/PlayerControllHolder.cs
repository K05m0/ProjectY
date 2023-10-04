using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllHolder : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [SerializeField] private LayerMask groundCollideWithRay;
    public KeyCode sprintKeyCode;
    public KeyCode crouchKeyCode;
    public KeyCode aimKeyCode;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    public (bool succes, Vector3 position) GetMousePosition()
    {
        //ray from camera to mouse pointer;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundCollideWithRay))
            //if ray hit in groun send mouse position
            return (succes: true, position: hitInfo.point);
        else
            //else send vector3(0,0,0)
            return (succes: false, Position: Vector3.zero);
    }
}
