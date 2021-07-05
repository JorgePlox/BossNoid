using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCursor : MonoBehaviour
{
    SpriteRenderer m_sprite;
    public static InGameCursor sharedInstance;
    bool isSpriteOff = false;

    void Awake()
    {
        if (sharedInstance = null)
        {
            sharedInstance = this;
        }
        Cursor.visible = false;
        m_sprite = this.gameObject.GetComponent<SpriteRenderer>();

    }


    void Update()
    {

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if (GameManager.sharedInstance != null)
        {

            if ((LevelManager.currentLevel == Levels.MainMenu || LevelManager.currentLevel == Levels.LevelMenu) && isSpriteOff)
            {
                SpriteOn();
            }

            else if ((LevelManager.currentLevel != Levels.MainMenu && LevelManager.currentLevel != Levels.LevelMenu))
            {
                if ((GameManager.sharedInstance.isPaused || GameManager.sharedInstance.isGameOver) && isSpriteOff )
                {
                    SpriteOn();
                }
                else if ((!GameManager.sharedInstance.isPaused && !GameManager.sharedInstance.isGameOver) && !isSpriteOff)
                {
                    SpriteOff();
                }
            }
        }
    }

    public void SpriteOff()
    {
        m_sprite.enabled = false;
        isSpriteOff = true;
    }

    public void SpriteOn()
    {
        m_sprite.enabled = true;
        isSpriteOff = false;
    }



}
