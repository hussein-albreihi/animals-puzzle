using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Cloud settings")]
    public float m_minSpeed = 0.22f;
    public float m_maxSpeed = 0.5f;

    private float m_speed = 0.04f;

    private void Start() {
        m_speed = Random.Range(m_minSpeed, m_maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - (m_speed * Time.deltaTime), transform.position.y);
    }

    private void OnBecameInvisible() {
        Destroy(this);    
    }
}
