using UnityEngine;
using UnityEngine.UI;

public class UIData : MonoBehaviour
{
    public Text currentInMagazineText;
    public Text totalAmoText;
    public Text healthText;
    public Text armorText;
    public Text pointsText;

    public void Start()
    {

    }

    public void Update()
    {
    }

    public void SetCurrentInMagazineValue(int val)
    {
        currentInMagazineText.text = val.ToString();
    }

    public void SetTotalAmoTextValue(int val)
    {
        totalAmoText.text = val.ToString();
    }

    public void SetHealthValue(int val)
    {
        healthText.text = val.ToString();
    }

    public void SetArmorValue(int val)
    {
        armorText.text = val.ToString();
    }

    public void SetPointsTextValue(int val)
    {
        armorText.text = "POINTS: " + val.ToString();
    }
}