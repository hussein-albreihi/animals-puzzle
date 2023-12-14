using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite m_upSprite;
    public Sprite m_rightSprite;
    public Sprite m_downSprite;
    public Sprite m_leftSprite;

    private SpriteRenderer m_renderer;
    private int m_direction;

    enum Direction
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3
    }
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
        CheckDirection();
        UpdateSprite();
    }

    private void CheckDirection() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        
        Vector3 directionToMouse = mouseWorldPosition - transform.position;

        float angleRadians = Mathf.Atan2(directionToMouse.y, directionToMouse.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        angleDegrees = (angleDegrees + 360) % 360;

        if ((angleDegrees > 0 && angleDegrees < 45) || (angleDegrees > 315 && angleDegrees <= 360)) { 
            // Moving right
            m_direction = (int) Direction.RIGHT;
            return;
        }
        else if (angleDegrees < 125 && angleDegrees >= 45) {
            // Moving up
            m_direction = (int) Direction.UP;
            Debug.Log("UP: " + (angleDegrees < 125 && angleDegrees >= 45));
            return;
        }
        else if (angleDegrees >= 125 && angleDegrees < 225) {
            // Moving left
            m_direction = (int) Direction.LEFT;
            Debug.Log("LEFT: " + (angleDegrees < 225 && angleDegrees >= 125));
            return;
        }
        else if (angleDegrees >= 225 && angleDegrees <= 315) {
            // Moving down
            m_direction = (int) Direction.DOWN;
            return;
        }
    }
    
    private void UpdateSprite() {
        switch (m_direction)
        {
            case (int) Direction.RIGHT:
                m_renderer.sprite = m_rightSprite;
                break;
            case (int) Direction.UP:
                m_renderer.sprite = m_upSprite;
                break;
            case (int) Direction.LEFT:
                m_renderer.sprite = m_leftSprite;
                break;
            case (int) Direction.DOWN:
                m_renderer.sprite = m_downSprite;
                break;
            default:
                Debug.LogWarning("Bad direction set");
                break;
        }
    }
}
