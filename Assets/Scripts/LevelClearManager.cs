using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearManager : MonoBehaviour
{
    [Header ("Exit sprites")]
    public Sprite m_openExitSprite;
    public Sprite m_closedExitSprite;

    private GameManager m_manager;
    private AudioSource m_fanFairAudioSource;
    private SpriteRenderer m_renderer;

    private bool m_levelCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_manager == null) {
            m_manager = FindObjectOfType<GameManager>();
        }

        if (m_fanFairAudioSource == null) {
            m_fanFairAudioSource = GetComponent<AudioSource>();
        }

        if (m_renderer == null) {
            m_renderer = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_manager.m_openExit && !m_levelCleared) {
            m_manager.PauseMusic();
            m_fanFairAudioSource.Play();
            m_renderer.sprite = m_openExitSprite;
            m_levelCleared = true;
        }

        // if (m_levelCleared && !m_fanFairAudioSource.isPlaying) {
        //     m_manager.PlayMusic();
        // }
    }
}
