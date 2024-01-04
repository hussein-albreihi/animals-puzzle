using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header ("Game settings")]
    public Rigidbody2D m_player;
    public GameManager m_manager;
    public LayerMask m_whatIsCollideable;

    [Header("Sprite settings")]
    public SpriteRenderer m_sprite;
    
    [Header("Jump animation")]
    public AnimationCurve m_jumpCurve;
    public ParticleSystem m_jumpParticles;

    [Header("Jump settings")]
    public float m_jumpHeight = 0f;
    public float m_jumpDelay = 0.15f;
    public float m_speed = 1.3f;

    private bool m_isJumping = false;
    private float m_jumpDelayTimer = 0f;
    private bool m_playerLost = false;
    private bool m_displayAd = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_sprite == null) {
            m_sprite = GetComponent<SpriteRenderer>();
        }

        if (m_player == null) {
            m_player = GetComponent<Rigidbody2D>();
        }

        if (m_manager == null) {
            m_manager = FindObjectOfType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_playerLost) {
            m_jumpDelayTimer += Time.deltaTime;
            Jump(0.3f, 0.0f);
            MovePlayer();
            CheckCollision();
        }

        if (m_displayAd) {
            m_manager.DisplayAd();
            m_displayAd = false;
        }
    }

    private void MovePlayer() {
        if (Input.GetMouseButton(0) && m_isJumping) {
            Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance; // This is now the normalized direction.

            m_player.velocity = direction * m_speed;
        }

        if (!Input.GetMouseButton(0)) {
            m_player.velocity = Vector2.zero;
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
    
    private void CheckCollision() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.2f, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (hit.rigidbody.tag == GlobalVariables.Tags.PICKUP) {
                m_manager.PickUpFruit();
                Destroy(hit.rigidbody.gameObject);
            }
        }
    }

    public void PlayerHit() {
        // Debug.LogError("Player hit logic not implemented");
        Debug.Log("Display Ad");
        m_playerLost = true;
        m_displayAd = true;
    }
}
