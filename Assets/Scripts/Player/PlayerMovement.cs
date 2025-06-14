using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 3f;
    [SerializeField] private float m_sprintModifer = 1.5f;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_animator;
    private Vector2 m_moveInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.linearVelocity = m_moveInput * m_moveSpeed;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        m_animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            m_animator.SetBool("isWalking", false);
            m_animator.SetFloat("LastInputX", m_moveInput.x);
            m_animator.SetFloat("LastInputY", m_moveInput.y);
        }
        
        m_moveInput = context.ReadValue<Vector2>();
        m_animator.SetFloat("InputX", m_moveInput.x);
        m_animator.SetFloat("InputY", m_moveInput.y);
        
    }
    
    public void Sprint(InputAction.CallbackContext context)
    {
        float walkSpeed = m_moveSpeed;
        bool isSprinting = context.ReadValueAsButton();

        if (context.canceled)
        {
            m_moveSpeed = 3f;
            Debug.Log("Walking.");
        }

        m_moveSpeed *= m_sprintModifer;
        Debug.Log("Sprinting.");
    }
}
