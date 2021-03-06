﻿using System.Collections;
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
    [SerializeField] private TextMeshProUGUI temperatureLabel = null;


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
        distanceLabel.text = Mathf.Max((Game.Instance.goalDistance - Game.Instance.distance),0) + " miles to go";
        moraleLabel.text = "morale: " + Mathf.FloorToInt(Game.Instance.MoralePct()) + "%";
        temperatureLabel.text = Game.Instance.temperature + "C";
        UpdateFire(Game.Instance.morale);
    }

    public void UpdateFire(int morale)
    {
        float moralePct = Game.Instance.MoralePct();
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
