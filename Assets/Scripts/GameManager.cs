using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;

        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;
    }


    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
        
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;

    //logic
    public int coins;
    public int experience;

    //save state
    /*
     * int preferedSkin
     * int coins
     * int experience
     * int weaponLevel
     */
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawPoint").transform.position;
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        // change player skin 
        weapon.SetWeaponLevel(int.Parse(data[3]));



    }

    //floadtingtext
    public void ShowText(string msg, int fontSize, Color color, Vector3 posision, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, posision, motion, duration);

    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }

    private void Update()
    {
        GetCurrentLevel();
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUP();
    }

    public void OnLevelUP()
    {
        player.OnLevelUP();
        OnHitpointChange();
    }

    //hitpoint Bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    public void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

}
