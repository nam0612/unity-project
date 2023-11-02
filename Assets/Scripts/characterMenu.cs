using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class characterMenu : MonoBehaviour
{
    private static characterMenu instance;
    public static characterMenu Instance { get =>  instance; }

    public Text levelText, hitpointText, cointText, upgradeCostText, xpText;
    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    [SerializeField] protected Animator anim;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowMenu()
    {
        anim.SetTrigger("show");
    }
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
            OnSelectionChanged();
        } else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //weapon upgrade
    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //update character info
    public virtual void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else 
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        
        
        //meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString(); ;
        cointText.text =   GameManager.instance.coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //xpBar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;

            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio,1,1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }



}
