using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//them CharacterController de dieu khien nhan vat
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    Animator animator; //khai bao animator
    CharacterController controller; //controller de quan ly nhan vat di chuyen (thay doi vi tri)
    public ParticleSystem dustRunParticle;
    public ParticleSystem dustJumpParticle;
    float currentSpeed = 0f; //toc do hien tai
    float walkSpeed = 2f; //toc do di bo
    float runSpeed = 8f; //toc do chay
    float acceleration = 5f; //toc do tang/giam
    float epsilon = 0.1f; //muc chenh lech cho phep
    private bool isJumping = false;

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
            isJumping = true;
        }

        if (isJumping && controller.isGrounded)
        {
            dustJumpParticle.Play();
            isJumping = false;
        }

        //chuyen trang thai sang chay khi bam nut W
        //if (Input.GetKey(KeyCode.W))
        //{
        //    animator.SetBool("isRunning", true); //khi bam nut w chuyen trang thai running
        //} else
        //{
        //    animator.SetBool("isRunning", false); //ngc lai thi chuyen ve trang thai idle
        //}

        //nhan vat di chuyen theo cac huong
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.magnitude > 0) {
            //Xoay huong cua nhan vat
            Quaternion toRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);

            float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            // Làm mượt tốc độ
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

            //thay doi vi tri cua nhan vat
            controller.Move(move.normalized * currentSpeed * Time.deltaTime);
            //thay doi animation
            if (Mathf.Abs(currentSpeed - walkSpeed) > epsilon)
            {
                // Cập nhật animation
                animator.SetBool("isRunning", true);
                if (!dustRunParticle.isPlaying)
                {
                    dustRunParticle.Play();
                }
            } else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                if (dustRunParticle.isPlaying)
                {
                    dustRunParticle.Stop();
                }
            }
        } else
        {
            // Khi không di chuyển, reset tốc độ và animation
            currentSpeed = 0f;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }
}
