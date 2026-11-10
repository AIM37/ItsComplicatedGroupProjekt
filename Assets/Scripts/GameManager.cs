using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerManager playerManager;

    [Header("Beziehungen und Stress")]
    //Keine ahnung wo der Partner rein soll, aber der muss raus
    [SerializeField] [UnityEngine.Range(0, 100)] private int partner = 60;

    [Header("Statusbalken")]
    //maybe diese Slider in eine Liste packen, damit wir uns nicht abhalten müssen
    [SerializeField] private Slider stressSlider;
    [SerializeField] private Slider child1Slider;
    [SerializeField] private Slider child2Slider;
    [SerializeField] private Slider partnerSlider;

    [Header("Textanzeigen")]
    [SerializeField] private TMP_Text moneyText;

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
        UpdateUI();
    }

    public void Collect(GameObject resource)
    {
        playerManager.Collect(resource);
    }

    public void AddStress(int amount)
    {
        playerManager.AddStress(amount);
    }

    public void ChangeRelationship(
        int child1Change,
        int child2Change,
        int partnerChange)
    {
        /*child1 = Mathf.Clamp(child1 + child1Change, 0, 100);
        child2 = Mathf.Clamp(child2 + child2Change, 0, 100);
        partner = Mathf.Clamp(partner + partnerChange, 0, 100);*/
    }

    private void UpdateUI()
    {
        /*stressSlider.value = stress;
        child1Slider.value = child1;
        child2Slider.value = child2;
        partnerSlider.value = partner;
        moneyText.text = "MONEY: " + money + " €";
        */
    }

    public int ReturnStress()
    {
        return playerManager.ReturnStress();
    }

    public Inventory GetInventory()
    {
        return playerManager.GetInventory();
    }

    private void CheckForNullables()
    {
        if (stressSlider == null) throw new InvalidOperationException(nameof(stressSlider) + " fehlt");
        if (child1Slider == null) throw new InvalidOperationException(nameof(child1Slider) + " fehlt");
        if (child2Slider == null) throw new InvalidOperationException(nameof(child2Slider) + " fehlt");
        if (partnerSlider == null) throw new InvalidOperationException(nameof(partnerSlider) + " fehlt");
        if (moneyText == null) throw new InvalidOperationException(nameof(moneyText) + " fehlt");
    }
}
