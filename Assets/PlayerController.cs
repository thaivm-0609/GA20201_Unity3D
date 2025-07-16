using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    Animator animator;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //su kien nhay
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        //su kien nam xuong
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("isKnockdown", true);
        } else
        {
            animator.SetBool("isKnockdown", false);
        }

        //di chuyen 4 huong
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        // xoay huong
        if (moveDir.magnitude > 0.1f)
        {
            transform.forward = moveDir; // Xoay nhan vat 
            controller.Move(moveDir * Speed * Time.deltaTime);
        }

        // Them speed
        animator.SetFloat("Speed", moveDir.magnitude);
    }
}
