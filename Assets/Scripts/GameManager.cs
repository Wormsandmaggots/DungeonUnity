using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance!=null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(HUD);
            Destroy(Menu);
            return;

        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject HUD;
    public GameObject Menu;

    public int money;
    public int exp;

    public void ShowText(string msg,int fontSize, Color color, Vector3 position , Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    public bool tryUpgradeWeapon()
    {
        //is the weapon max level
        if(weaponPrices.Count<=weapon.weaponLevel)
        {
            return false;
        }
        if(money>=weaponPrices[weapon.weaponLevel])
        {
            money -= weaponPrices[weapon.weaponLevel];
            weapon.upgradeWeapon();
            return true;
        }
        return false;
    }

    //hp bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector2(1, ratio);
    }
    //exp system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while(exp>=add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)//if max level
                return r;
        }
        return r;
    }
    public int GetExpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while(r<level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();

    }
    //on scene load
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += money.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponLevel.ToString();


        PlayerPrefs.SetString("SaveState",s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //changing player skins
        
        money = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        if(GetCurrentLevel()!=1)
            player.SetLevel(GetCurrentLevel());
        weapon.setWeaponLevel(int.Parse(data[3]));
        //changing the weapon level
    }
}
