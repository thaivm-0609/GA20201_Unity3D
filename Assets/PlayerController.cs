using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator; //khai bao animator
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //khi ng dung bam phim space => kich hoat animation jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true); //khi bam nut w chuyen trang thai running
        } else
        {
            animator.SetBool("isRunning", false); //ngc lai thi chuyen ve trang thai idle
        }
    }
}
