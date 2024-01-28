using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        public Animator animator;

        // private bool isFlipped = false;
        private SpriteRenderer sr;
        private int idleUp = Animator.StringToHash("Player_Idle_Up");
        private int idleDown = Animator.StringToHash("Player_Idle_Down");
        private int moveDown = Animator.StringToHash("Player_Move_Down");
        private int moveUp = Animator.StringToHash("Player_Move_Up");
        private int moveDiagDown = Animator.StringToHash("Player_Move_Diag_Down");
        private int moveDiagUp = Animator.StringToHash("Player_Move_Diag_Up");

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }
            if (dir.x == 0 && dir.y == 0)
            {
                animator.CrossFade(idleDown, 0, 0);
            }
            else if (dir.x == 0 && dir.y == 1)
            {
                animator.CrossFade(moveUp, 0, 0);
            }
            else if (dir.x > 0 && dir.y == 0)
            {
                animator.CrossFade(moveDiagDown, 0, 0);
                sr.flipX = false;
            }
            else if (dir.x < 0 && dir.y == 0)
            {
                animator.CrossFade(moveDiagDown, 0, 0);
                sr.flipX = true;
            }
            else if (dir.x == 0 && dir.y == -1)
            {
                animator.CrossFade(moveDown, 0, 0);
                sr.flipX = false;
            }
            else if (dir.x > 0 && dir.y > 0)
            {
                animator.CrossFade(moveDiagUp, 0, 0);
                sr.flipX = false;
            }
            else if (dir.x < 0 && dir.y > 0)
            {
                animator.CrossFade(moveDiagUp, 0, 0);
                sr.flipX = true;
            }
            else if (dir.x > 0 && dir.y < 0)
            {
                animator.CrossFade(moveDiagDown, 0, 0);
                sr.flipX = false;
            }
            else if (dir.x < 0 && dir.y < 0)
            {
                animator.CrossFade(moveDiagDown, 0, 0);
                sr.flipX = true;
            }
            // if (dir.x != 0 || dir.y != 0)
            // {
            //     animator.SetFloat("Y", dir.y);
            //     animator.SetFloat("X", dir.x);
            //     animator.SetBool("IsMoving", true);
            // }
            // else
            // {
            //     animator.SetBool("IsMoving", false);
            // }
            // if (isFlipped)
            // {
            //     transform.GetComponent<SpriteRenderer>().flipX = true;
            // }
            // else
            // {
            //     transform.GetComponent<SpriteRenderer>().flipX = false;
            // }
            dir.Normalize();
            // if (dir.magnitude == 0)
            // {
            //     animator.CrossFade(idleDown, 0, 0);
            // } else if (dir.x)

            GetComponent<Rigidbody2D>().velocity = speed * dir;
            // GetComponent<Rigidbody2D>()
            // .AddForce(speed * dir);
        }
        // private void SetAnimation(Vector2 ani){
        //         if(ani.magnitude == 0){
        //             animator.CrossFade(idleDown,0,0);
        //         } else if (ani.x == 0 && ani.y == 1){
        //             animator.CrossFade(idleUp)
        //         }
        // }
    }
}
