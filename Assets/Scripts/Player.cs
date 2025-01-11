using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;
    public BoxCollider2D PlayerBoxCollider;

    // 땅에서 시작 -> true
    private bool isGrounded = true;

    public bool isInvincible = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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

    public void KillPlayer()
    {
        // 애니메이션 멈추기
        PlayerAnimator.enabled = false;
        // 콜라이더 꺼주기 
        PlayerBoxCollider.enabled = false;
        // 슈퍼마리오같이 한 번 점프하게 해줌 (효과)
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives -= 1;
        if(GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }

    void Heal()
    {
        // 3보다 커져도 항상 최소값 3을 반환한다
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    // 게임 시작하자마자 실행됨 (게임 시작시 플레이어가 플랫폼에 떨어지기 때문)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            // 땅이 아닐 경우에만 조건 추가
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            // 무적 상태가 아니라면
            if (!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();
            }

        }
        else if (collider.gameObject.tag == "Food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.tag == "Golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
