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
        private Rigidbody2D rb;

        // private int idleUp = Animator.StringToHash("Player_Idle_Up");
        // private int idleDown = Animator.StringToHash("Player_Idle_Down");
        // private int moveDown = Animator.StringToHash("Player_Move_Down");
        // private int moveUp = Animator.StringToHash("Player_Move_Up");
        // private int moveDiagDown = Animator.StringToHash("Player_Move_Diag_Down");
        // private int moveDiagUp = Animator.StringToHash("Player_Move_Diag_Up");
        int idle = Animator.StringToHash("Player_Idle");
        int move = Animator.StringToHash("Player_Move");
        Vector2 lastDir = Vector2.right;

        // [SerializeField]
        // private Camera mainCam;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            // Vector2 relativePos = mousePos - transform.position;
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                lastDir = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                lastDir = Vector2.right;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }
            if (dir.magnitude == 0)
            {
                animator.CrossFade(idle, 0, 0);
            }
            else
            {
                animator.CrossFade(move, 0, 0);
            }
            if (lastDir.x <= 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
            // if (dir.x == 0 && dir.y == 0)
            // {
            //     animator.CrossFade(idleDown, 0, 0);
            // }
            // else if (dir.x == 0 && dir.y == 1)
            // {
            //     animator.CrossFade(moveUp, 0, 0);
            // }
            // else if (dir.x > 0 && dir.y == 0)
            // {
            //     animator.CrossFade(moveDiagDown, 0, 0);
            //     sr.flipX = false;
            // }
            // else if (dir.x < 0 && dir.y == 0)
            // {
            //     animator.CrossFade(moveDiagDown, 0, 0);
            //     sr.flipX = true;
            // }
            // else if (dir.x == 0 && dir.y == -1)
            // {
            //     animator.CrossFade(moveDown, 0, 0);
            //     sr.flipX = false;
            // }
            // else if (dir.x > 0 && dir.y > 0)
            // {
            //     animator.CrossFade(moveDiagUp, 0, 0);
            //     sr.flipX = false;
            // }
            // else if (dir.x < 0 && dir.y > 0)
            // {
            //     animator.CrossFade(moveDiagUp, 0, 0);
            //     sr.flipX = true;
            // }
            // else if (dir.x > 0 && dir.y < 0)
            // {
            //     animator.CrossFade(moveDiagDown, 0, 0);
            //     sr.flipX = false;
            // }
            // else if (dir.x < 0 && dir.y < 0)
            // {
            //     animator.CrossFade(moveDiagDown, 0, 0);
            //     sr.flipX = true;
            // }
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
            var modifier = GetComponent<StatModifiers>().MoveSpeedMultiplier;
            Vector3 movement = speed * modifier * dir;
            // rb.MovePosition(transform.position + movement);
            rb.velocity = movement;
            // GetComponent<Rigidbody2D>()
            // .AddForce(speed * dir);
        }
    }
}

// private void SetAnimation(Vector2 ani){
//         if(ani.magnitude == 0){
//             animator.CrossFade(idleDown,0,0);
//         } else if (ani.x == 0 && ani.y == 1){
//             animator.CrossFade(idleUp)
//         }
// }
