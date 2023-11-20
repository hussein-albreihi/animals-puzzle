using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Sprite settings")]
    public SpriteRenderer m_sprite;
    
    [Header("Bounce animation")]
    public AnimationCurve m_bounceCurve;
    public ParticleSystem m_bounceParticles;

    [Header("Bounce settings")]
    public float m_bounceHeight = 0f;
    public float m_bounceDelay = 0.15f;

    private bool m_isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Bounce();
    }

    private void Bounce() {
        StartCoroutine(BounceCo(0.3f, 0f));
    }
    
    private IEnumerator BounceCo(float bounceScale, float bouncePushScale) {
        m_isBouncing = true;

        float bounceStartTime = Time.time;
        float bounceDuration = 0.6f;

        while (m_isBouncing) {
            float bounceCompletedPercentage = (Time.time - bounceStartTime) / bounceDuration;
            bounceCompletedPercentage = Mathf.Clamp01(bounceCompletedPercentage);
            Vector3 scale = Vector3.one + Vector3.one * m_bounceCurve.Evaluate(bounceCompletedPercentage) * bounceScale;
            m_sprite.transform.localScale = new Vector3(scale.x, scale.y, 1);

            if (bounceCompletedPercentage == 1f) {
                break;
            }

            yield return null;
        }

        m_isBouncing = false;
        m_sprite.transform.localScale = Vector3.one;
        // m_bounceParticles.Play();
    }
}
