using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private Canvas mainCanvas;  

    [SerializeField]
    private GameObject actionsPanel;

    [SerializeField]
    private GameObject endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActionsPanelVisibility(bool visible)
    {
        actionsPanel.SetActive(visible);
        endTurnButton.SetActive(!visible);
    }


}
