using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPointScript : MonoBehaviour
{
    /// <summary>
    /// The image of this bullet point
    /// </summary>
    public Image BulletPointImage;

    /// <summary>
    /// The different sprites
    /// </summary>
    public Sprite DoneSprite, CurrentSprite, ToDoSprite;

    /// <summary>
    /// Sets the bullet point image
    /// </summary>
    /// <param name="state">0 for todo, 1 for current and 2 for done</param>
    public void SetBulletPointImage(int state)
    {
        switch (state)
        {
            case 0:
                BulletPointImage.sprite = ToDoSprite;
                break;
            case 1:
                BulletPointImage.sprite = CurrentSprite;
                break;
            case 2:
                BulletPointImage.sprite = DoneSprite;
                break;
        }
    }
}
