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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turnLabel.text = "Turn: " + Game.Instance.turn;
        penguinLabel.text = "Penguins: " + Game.Instance.penguins;
        foodLabel.text = "Food: " + Game.Instance.food;
    }
}
