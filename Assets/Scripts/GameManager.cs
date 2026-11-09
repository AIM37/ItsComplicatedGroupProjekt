using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ressourcen")]
    private int money = 0;
    private int toys = 0;
    private int food = 0;

    [Header("Beziehungen und Stress")]
    [SerializeField][Range(0, 100)] private int stress = 50;
    [SerializeField][Range(0, 100)] private int child1 = 80;
    [SerializeField][Range(0, 100)] private int child2 = 80;
    [SerializeField][Range(0, 100)] private int partner = 60;

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

    //UpdateUI kann maybe in eine Update Funktion und muss dann nicht immer wieder in jeder funktion aufgerufen werden
    private void UpdateUI()
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

    public int ReturnStress()
    {
        return stress;
    }
}
