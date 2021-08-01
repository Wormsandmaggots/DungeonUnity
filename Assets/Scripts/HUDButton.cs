using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDButton : MonoBehaviour
{
    public MovesMenu MovesMenu;
    public Character_menu Menu;
    private Button HudButton;
    public Sprite[] ButtonSprites;
    private void Start()
    {
        HudButton =GetComponent<Button>();
    }
    public void HideMovesMenu()
    {
        MovesMenu.AnimCounter = false;
    }

    public void ChangeSpriteWhenMenu()
    {
        if (MovesMenu.AnimCounter == false && Menu.MenuShowing == false)
            HudButton.image.sprite = ButtonSprites[0];
        else
            HudButton.image.sprite = ButtonSprites[1];

    }
}
