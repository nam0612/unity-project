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
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;

    public List<int> xpTable;

    public Player player;

    //weapon
    public FloatingTextManager floatingTextManager;

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
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
    }

    //floadtingtext
    public void ShowText(string msg, int fontSize, Color color, Vector3 posision, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, posision, motion, duration);

    }

}
