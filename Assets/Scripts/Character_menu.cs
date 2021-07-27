using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_menu : MonoBehaviour
{
    public Text levelText, hitpointText, pesosText, upgradeText, xpText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public void onArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;
            if(currentCharacterSelection==GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            onSelectionChange();

        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count-1;
            }
            onSelectionChange();
        }
    }
    private void onSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.swapSprite(currentCharacterSelection);
    }

    //weapon upgrade
    public void onClickUpgrade()
    {
        if(GameManager.instance.tryUpgradeWeapon())
        {
            updateMenu();
        }

    }

    //update character information
    public void updateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel < GameManager.instance.weaponSprites.Count-1)
            upgradeText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        else
            upgradeText.text = "Max";
        //meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.money.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        //xp bar
        if(GameManager.instance.GetCurrentLevel()==GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.exp.ToString() + " total exp points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevtLevelExp = GameManager.instance.GetExpToLevel(GameManager.instance.GetCurrentLevel()-1);
            int currLevelExp = GameManager.instance.GetExpToLevel(GameManager.instance.GetCurrentLevel());
            int diff = currLevelExp - prevtLevelExp;
            int currXpIntoLevel = GameManager.instance.exp - prevtLevelExp;
            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + "/" + diff;
        }
    }
}
