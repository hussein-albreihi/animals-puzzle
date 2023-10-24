using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Sprite settings")]
    public SpriteRenderer m_sprite;
    
    [Header("Jump animation")]
    public AnimationCurve m_jumpCurve;
    public ParticleSystem m_jumpParticles;

    [Header("Jump settings")]
    public float m_jumpHeight = 0f;
    public float m_jumpDelay = 0.15f;

    private bool m_isJumping = false;
    private float m_jumpDelayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_sprite == null) {
            m_sprite = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_jumpDelayTimer += Time.deltaTime;
        Jump(0.3f, 0.0f);
        MovePlayer();
    }

    private void MovePlayer() {
        if (Input.GetMouseButton(0) && m_isJumping) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPos = new(transform.position.x, transform.position.y, 0f);
            Vector3 newPos = new(mousePosition.x, mousePosition.y, 0);
            Vector3 moveTowards = Vector3.Lerp(playerPos, newPos, 0.3f);
            transform.position = moveTowards;
        }
    }

    private void Jump(float jumpScale, float jumpPushScale) {
        if (Input.GetMouseButton(0) && !m_isJumping && m_jumpDelayTimer > m_jumpDelay) {
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
            m_sprite.transform.localScale = new Vector3(scale.x, scale.y, 1);

            if (jumpCompletedPercentage == 1f) {
                break;
            }

            yield return null;
        }

        m_isJumping = false;
        m_jumpDelayTimer = 0f;
        m_sprite.transform.localScale = Vector3.one;
    }
}
