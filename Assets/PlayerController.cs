using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    Animator animator; //khai bao animator
    CharacterController controller;

    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //khi ng dung bam phim space => kich hoat animation jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.magnitude > 0)
        {
            animator.SetBool("isRunning", true);

            //xoay huong nhan vat
            Quaternion toRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);

            Speed = animator.GetFloat("Speed");
            controller.Move(move.normalized * Speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
            controller.Move(Vector3.zero);
        }
    }
}
