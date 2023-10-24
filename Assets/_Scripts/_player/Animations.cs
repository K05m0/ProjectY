using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    private Animator m_Animator;
    private CharacterController controller;
    private float move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        move = controller.velocity.magnitude;
        //Debug.Log(move);
        m_Animator.SetFloat("Speed", move);
        
        if (Input.GetKey(KeyCode.R)) {
            m_Animator.SetBool("Reload", true);
        } else {
            m_Animator.SetBool("Reload", false);
        }
    }
}
