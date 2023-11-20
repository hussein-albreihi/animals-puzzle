using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("Sound effects")]
    public AudioSource m_musicAudioSource;
    public AudioSource m_pickupSFX;

    private int m_totalPickedUp = 0;
    public bool m_openExit = false;

    private GameObject[] m_pickUps;

    private readonly string PICKUP_TAG = "Pickup";

    // Start is called before the first frame update
    void Start()
    {
        m_pickUps = GameObject.FindGameObjectsWithTag(PICKUP_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_totalPickedUp >= m_pickUps.Length) {
            m_openExit = true;
        }
    }

    public void PickUpFruit() {
        m_totalPickedUp++;
        if (m_pickupSFX != null) {
            m_pickupSFX.Play();
        } else {
            Debug.LogError("No AudioSource set for collecting all pickups!");
        }
    }

    public bool GetOpenExit() {
        return m_openExit;
    }

    public void PauseMusic() {
        m_musicAudioSource.Pause();
    }

    public void StopMusic() {
        m_musicAudioSource.Stop();
    }

    public void PlayMusic() {
        m_musicAudioSource.Play();
    }
}
