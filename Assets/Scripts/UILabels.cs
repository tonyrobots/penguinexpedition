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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turnLabel.text = "March " + Game.Instance.turn + ", 1898";
        penguinLabel.text = Game.Instance.penguins + " penguins";
        foodLabel.text = Game.Instance.food + " food left";
        distanceLabel.text = (Game.Instance.goalDistance - Game.Instance.distance) + " miles to go";
        moraleLabel.text = "morale: " + Game.Instance.morale;
    }
}
