using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header ("Settings")]
    public float m_spikesActiveDelay = 0.8f;
    public float m_spikesDownDelay = 1.5f;

    [Header ("Sprites")]
    public Sprite m_spikesDown;
    public Sprite m_spikesUp;

    [Header ("Player manager")]
    public PlayerControls m_player;

    [Header ("Collision settings")]
    public LayerMask m_whatIsPlayer;
    public float m_raycastRadius = 0.2f;

    private SpriteRenderer m_renderer;
    private bool m_spikesActive = false;

    private float m_spikesTimer = 0f;
    private bool m_disableCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_renderer == null) {
            m_renderer = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_spikesTimer += Time.deltaTime;

        if (m_spikesActive && m_spikesTimer >= m_spikesActiveDelay) {
            m_spikesActive = !m_spikesActive;
            ToggleSpikesActive();
        }

        if (!m_spikesActive && m_spikesTimer >= m_spikesDownDelay) {
            m_spikesActive = !m_spikesActive;
            ToggleSpikesActive();
        }

        if (m_spikesActive) {
            CheckCollision();
        }
    }

    private void ToggleSpikesActive() {
        if (m_spikesActive) {
            m_renderer.sprite = m_spikesUp;
            m_spikesTimer = 0f;
        }

        if (!m_spikesActive) {
            m_renderer.sprite = m_spikesDown;
            m_spikesTimer = 0f;
        }
    }

    private void CheckCollision() {
        if (!m_disableCollision) {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, m_raycastRadius, Vector2.zero, m_whatIsPlayer);

            if (hits.Length > 0) {
                foreach (RaycastHit2D hit in hits) {
                    if (hit.rigidbody.tag == GlobalVariables.Tags.PLAYER && m_spikesActive) {
                        m_player.PlayerHit();
                        m_disableCollision = true;
                    }
                } 
            }
        }
    }
}
