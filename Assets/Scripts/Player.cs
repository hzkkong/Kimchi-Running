using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;

    // 땅에서 시작 -> true
    private bool isGrounded = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 1
            //PlayerRigidBody.linearVelocityX = 10;
            //PlayerRigidBody.linearVelocityY = 20;


            // 2
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;     
            // 떨어졌을 때 collider끼리 부딪히면 isGrounded를 다시 true로 -> OnCollisionEnter2D
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    // 게임 시작하자마자 실행됨 (게임 시작시 플레이어가 플랫폼에 떨어지기 때문)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            // 땅이 아닐 경우에만 조건 추가
            if(!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
            
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {

        }
        else if(collider.gameObject.tag == "Food")
        {

        }
        else if (collider.gameObject.tag == "Golden")
        {
            
        }
    }
}
