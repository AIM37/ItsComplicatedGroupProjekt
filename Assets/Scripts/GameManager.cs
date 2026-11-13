using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameobjects")]

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ChildManager childManager;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private PhoneShopController phoneShopController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckForNullables();
    }

    private void Update()
    {
        //GiveAllValues();
    }

    public void Collect(GameObject resource)
    {
        playerManager.Collect(resource);
    }

    public void AddStress(int amount)
    {
        playerManager.AddStress(amount);
    }

    
    /*
    private void GiveAllValues()
    {
        int stress = playerManager.GetStress();
        List<GameObject> inventoryItems = playerManager.GetInventory().GetInventoryList();
        List<Child> allChilds = childManager.GetChildList();

        //Das musst man machen, wenn man die einzelnen Bedürfnisse der Kinder braucht
        foreach (Child child in allChilds)
        {
            List<ChildNeed> allNeedsOfChild = new List<ChildNeed>();
            foreach(ChildNeed need in child.wantedItems)
            {
                allNeedsOfChild.Add(need);
                //Den namen des gewollten Items bekommt man mit "need.neededItem.name;" und den Wert wie glücklich es ist mit "need.currentStat"

            }
        }

        //Das ist nur wichtig für den Debug Display
        string childrenText = string.Join("",
            childManager.GetChildList().Select(child =>
                $"     {child.childName}: " +
                string.Join(", ",
                    child.wantedItems.Select(need =>
                        $"{need.neededItem.name} ({need.currentStat})"))
            ));

        string inventoryText = string.Join("", playerManager.GetInventory().GetInventoryList().Select(item => $"{item.name}, "));
        Debug.Log($"Money: {money}  Stress: {stress} {childrenText} \n Inventar: {inventoryText}");
    }*/

    public void PauseGameplay()
    {
        waveManager.PauseGameplay();
    }

    public void ResumeGameplay()
    {
        waveManager.ResumeGameplay();
    }

    public bool GetWaveRunning()
    {
        return waveManager.GetWaveRunning();
    }

    public int GetStress()
    {
        return playerManager.GetStress();
    }

    public int GetMaxStress()
    {
        return playerManager.GetMaxStress();
    }

    public int GetMoney()
    {
        return playerManager.GetInventory().GetMoney();
    }

    //Muss Hardcoding geändert werden
    public List<ChildNeed> GetChildValue1()
    {
        List<Child> allChilds = childManager.GetChildList();

        //Das musst man machen, wenn man die einzelnen Bedürfnisse der Kinder braucht
        foreach (Child child in allChilds)
        {
            List<ChildNeed> allNeedsOfChild = new List<ChildNeed>();
            foreach (ChildNeed need in child.wantedItems)
            {
                allNeedsOfChild.Add(need);
                //Den namen des gewollten Items bekommt man mit "need.neededItem.name;" und den Wert wie glücklich es ist mit "need.currentStat"

            }
            return allNeedsOfChild;
        }
        return null;
    }

    public Inventory GetInventory()
    {
        return playerManager.GetInventory();
    }

    public bool GetMovementAllowed()
    {
        if (phoneShopController.GetShopActive()) return false;
        return true;
    }

    public bool CheckForSadChild()
    {
        if (childManager.CheckForSadChild()) return true;
        return false;
    }
    public void DeactivateUnimportendUI()
    {
        if (phoneShopController.GetShopActive()) phoneShopController.ToggleShop();
    }

    private void CheckForNullables()
    {
        if (playerManager == null) throw new InvalidOperationException(nameof(playerManager) + " fehlt");
        if (childManager == null) throw new InvalidOperationException(nameof(childManager) + " fehlt");
        /*if (stressSlider == null) throw new InvalidOperationException(nameof(stressSlider) + " fehlt");
        if (child1Slider == null) throw new InvalidOperationException(nameof(child1Slider) + " fehlt");
        if (child2Slider == null) throw new InvalidOperationException(nameof(child2Slider) + " fehlt");
        if (partnerSlider == null) throw new InvalidOperationException(nameof(partnerSlider) + " fehlt");
        if (moneyText == null) throw new InvalidOperationException(nameof(moneyText) + " fehlt");*/
    }
}
