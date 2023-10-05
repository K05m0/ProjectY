using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllHolder : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public KeyCode runKeyCode;
    public KeyCode freeAimKeyCode;
    [SerializeField] private LayerMask groundCollideWithRay;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
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
