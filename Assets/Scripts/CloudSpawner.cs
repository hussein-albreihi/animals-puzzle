using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Clouds")]
    public List<GameObject> m_clouds = new();

    // Private variables
    private float m_spawnTimer = 8f;

    // Update is called once per frame
    void Update()
    {
        m_spawnTimer += Time.deltaTime;

        if (m_spawnTimer > 5.6f) {
            SpawnCloud();
            m_spawnTimer = 0f;
        }
    }

    private void SpawnCloud() {
        int rng = Random.Range(0, m_clouds.Count);
        _ = m_clouds[0];
        GameObject cloud = Instantiate(m_clouds[rng]);
        cloud.transform.position = new Vector2(transform.position.x, Random.Range(transform.position.y - 2f, transform.position.y + 5f));
    }
}
