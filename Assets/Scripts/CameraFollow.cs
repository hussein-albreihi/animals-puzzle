using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private float smoothTime = 0.3f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(m_player.position.x, m_player.position.y, -10), smoothTime);
    }
}