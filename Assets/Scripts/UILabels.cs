using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILabels : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI turnLabel = null;
    [SerializeField] private TextMeshProUGUI penguinLabel = null;
    [SerializeField] private TextMeshProUGUI foodLabel = null;
    [SerializeField] private TextMeshProUGUI moraleLabel = null;
    [SerializeField] private TextMeshProUGUI distanceLabel = null;

    public Image[] fireImages;
    private int fireLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.turn > 0) 
        {
            turnLabel.text = "March " + Game.Instance.turn + ", 1898";
        } else {
            turnLabel.text = "February 29, 1898";
        }
        penguinLabel.text = Game.Instance.penguins + " penguins";
        foodLabel.text = Game.Instance.food + " food left";
        distanceLabel.text = (Game.Instance.goalDistance - Game.Instance.distance) + " miles to go";
        moraleLabel.text = "morale: " + Game.Instance.morale;
        UpdateFire(Game.Instance.morale);
    }

    public void UpdateFire(int morale)
    {
        float moralePct = 100 * morale/Game.Instance.startingMorale;
        int newFireLevel = Mathf.CeilToInt(moralePct/20f);

        if (newFireLevel == fireLevel) return;

        Debug.Log(moralePct + "%");


        for (int i = 0; i < 5; i++)
        {
            if ((5-newFireLevel) > i) {
                fireImages[i].enabled = false;
            } else {
                fireImages[i].enabled = true;
            }
        }

        fireLevel = newFireLevel;
    }
}
