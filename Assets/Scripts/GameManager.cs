
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ressourcen")]
    public int money = 0;
    public int toys = 0;
    public int food = 0;

    [Header("Beziehungen und Stress")]
    [Range(0, 100)] public int stress = 50;
    [Range(0, 100)] public int child1 = 80;
    [Range(0, 100)] public int child2 = 80;
    [Range(0, 100)] public int partner = 60;

    [Header("Statusbalken")]
    public Slider stressSlider;
    public Slider child1Slider;
    public Slider child2Slider;
    public Slider partnerSlider;

    [Header("Textanzeigen")]
    public TMP_Text moneyText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void Collect(string resource)
    {
        if (resource == "Money")
            money += 10;

        if (resource == "Heart")
            AddStress(-10);

        if (resource == "Toy")
            toys++;

        if (resource == "Food")
            food++;

        Debug.Log("Spielzeug: " + toys +
                  " | Essen: " + food);

        UpdateUI();
    }

    public bool GiveToChild(int childNumber)
    {
        if (childNumber == 1 && toys > 0)
        {
            toys--;
            child1 = Mathf.Min(100, child1 + 15);
            UpdateUI();
            return true;
        }

        if (childNumber == 2 && food > 0)
        {
            food--;
            child2 = Mathf.Min(100, child2 + 15);
            UpdateUI();
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

        UpdateUI();
    }

    public void AddStress(int amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
        UpdateUI();
    }

    public void ChangeRelationship(
        int child1Change,
        int child2Change,
        int partnerChange)
    {
        child1 = Mathf.Clamp(child1 + child1Change, 0, 100);
        child2 = Mathf.Clamp(child2 + child2Change, 0, 100);
        partner = Mathf.Clamp(partner + partnerChange, 0, 100);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (stressSlider != null)
            stressSlider.value = stress;

        if (child1Slider != null)
            child1Slider.value = child1;

        if (child2Slider != null)
            child2Slider.value = child2;

        if (partnerSlider != null)
            partnerSlider.value = partner;

        if (moneyText != null)
            moneyText.text = "MONEY: " + money + " €";
    }
}
