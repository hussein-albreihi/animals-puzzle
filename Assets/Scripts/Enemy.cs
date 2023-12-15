using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Sprite settings")]
    public SpriteRenderer m_sprite;

    [Header("Jump animation")]
    public AnimationCurve m_jumpCurve;
    public ParticleSystem m_jumpParticles;

    [Header("Jump settings")]
    public float m_jumpHeight = 0f;
    public float m_jumpDelay = 0.15f;
    public float m_speed = 1.3f;

    [Header ("Collision settings")]
    public LayerMask m_whatIsWall;
    public GameObject m_collisionCheck;

    private Rigidbody2D m_enemy;
    private bool m_isFacingRight = true;
    private bool m_isJumping = false;
    private float m_jumpDelayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_enemy == null) {
            m_enemy = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_jumpDelayTimer += Time.deltaTime;
        Jump(0.3f, 0.0f);
        Move();
        CheckForWall();
    }

    private void Move() {
        if (m_isJumping) {
            if (m_isFacingRight) {
                m_enemy.velocity = new Vector2(m_speed, 0);
            } else {
                m_enemy.velocity = new Vector2(-m_speed, 0);
            }   
        } else {
            m_enemy.velocity = Vector2.zero;
        }
    }

    private void CheckForWall() {
        RaycastHit2D hit;
        if (m_isFacingRight) {
            hit = Physics2D.Raycast(m_collisionCheck.transform.position, Vector3.right, 0.02f, m_whatIsWall);
        } else {
            hit = Physics2D.Raycast(m_collisionCheck.transform.position, Vector3.left, 0.02f, m_whatIsWall);
        }

        if (hit && hit.rigidbody.tag == GlobalVariables.Tags.WALL) {
            Flip();
        }
    }

    private void Jump(float jumpScale, float jumpPushScale) {
        if (!m_isJumping && m_jumpDelayTimer > m_jumpDelay) {
            StartCoroutine(JumpCo(jumpScale, jumpPushScale));
        }
    }

    private IEnumerator JumpCo(float jumpScale, float jumpPushScale) {
        m_isJumping = true;

        float jumpStartTime = Time.time;
        float jumpDuration = 0.6f;

        while (m_isJumping) {
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);
            Vector3 scale = Vector3.one + Vector3.one * m_jumpCurve.Evaluate(jumpCompletedPercentage) * jumpScale;
            if (m_isFacingRight) {
                m_sprite.transform.localScale = new Vector3(scale.x, scale.y, 1);
            } else {
                m_sprite.transform.localScale = new Vector3(-scale.x, scale.y, 1);
            }

            if (jumpCompletedPercentage == 1f) {
                break;
            }

            yield return null;
        }

        m_isJumping = false;
        m_jumpDelayTimer = 0f;
        
        if (m_isFacingRight) {
            m_sprite.transform.localScale = Vector3.one;
        } else {
            m_sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    private void Flip() {
        if (m_isFacingRight) {
            transform.localScale = new Vector2(-1, 1);
        } else {
            transform.localScale = new Vector2(1, 1);
        }

        m_isFacingRight = !m_isFacingRight;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GlobalVariables.Tags.PLAYER) {
            other.gameObject.GetComponent<PlayerControls>().PlayerHit();
        }
    }
}
