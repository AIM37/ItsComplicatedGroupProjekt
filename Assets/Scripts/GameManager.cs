using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ressourcen")]
    [SerializeField] private int money = 0;
    [SerializeField] private int toys = 0;
    [SerializeField] private int food = 0;

    [Header("Beziehungen und Stress")]
    [SerializeField] [Range(0, 100)] private int stress = 50;
    [SerializeField] [Range(0, 100)] private int child1 = 80;
    [SerializeField] [Range(0, 100)] private int child2 = 80;
    [SerializeField] [Range(0, 100)] private int partner = 60;

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

    public void Collect(string resource)
    {
        switch (resource)
        {
            case "Money":
                money += 10;
                break;
            case "Heart":
                AddStress(-10);
                break;
            case "Toy":
                toys++;
                break;
            case "Food":
                food++;
                break;
            default:
                break;

        }

        Debug.Log("Spielzeug: " + toys +
                  " | Essen: " + food);
    }

    public bool GiveToChild(int childNumber)
    {
        if (childNumber == 1 && toys > 0)
        {
            toys--;
            child1 = Mathf.Min(100, child1 + 15);
            
            return true;
        }

        if (childNumber == 2 && food > 0)
        {
            food--;
            child2 = Mathf.Min(100, child2 + 15);
            
            return true;
        }

        return false;
    }

    public void LoseChildRelationship(int childNumber)
    {
        if (childNumber == 1)
            child1 = Mathf.Max(0, child1 - 5);

        if (childNumber == 2)
            child2 = Mathf.Max(0, child2 - 5);
    }

    public void AddStress(int amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
    }

    public void ChangeRelationship(
        int child1Change,
        int child2Change,
        int partnerChange)
    {
        child1 = Mathf.Clamp(child1 + child1Change, 0, 100);
        child2 = Mathf.Clamp(child2 + child2Change, 0, 100);
        partner = Mathf.Clamp(partner + partnerChange, 0, 100);
    }

    private void UpdateUI()
    {
        stressSlider.value = stress;
        child1Slider.value = child1;
        child2Slider.value = child2;
        partnerSlider.value = partner;
        moneyText.text = "MONEY: " + money + " €";
    }

    public int ReturnStress()
    {
        return stress;
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
