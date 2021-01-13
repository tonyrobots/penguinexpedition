using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private Canvas mainCanvas= null;  

    [SerializeField]
    private GameObject actionsPanel= null;

    [SerializeField]
    private GameObject endTurnButton = null;

    [SerializeField]
    private GameObject optionButtonsPanel = null;

    [SerializeField]
    private GameObject optionButtonPrefab = null;


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

    public void ShowEventOptions(List<Event.Option> options)
    {
        // disable normal 'continue' button
        endTurnButton.SetActive(false);

        // show option buttons
        for (int i = 0; i < options.Count; i++)
        {
            Event.Option option = options[i];
            Debug.Log(option.copy);
            Button optionButton = CreateOptionButton(option.copy).GetComponent<Button>();
            // Tell the button what to do when we press it
            optionButton.onClick.AddListener(delegate { OnClickOptionButton(option); });
        }
    }

    // Creates a button showing the choice text
    GameObject CreateOptionButton(string text)
    {
        // Creates the button from a prefab
        GameObject option = Instantiate(optionButtonPrefab);
        option.transform.SetParent(optionButtonsPanel.transform, false);

        // Gets the text from the button prefab
        TextMeshProUGUI choiceText = option.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = option.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return option;
    }

    // enact that option
    void OnClickOptionButton(Event.Option option)
    {
        // // set option event as next event
        // Game.Instance.nextEvent = option.outcomeEvent;
        RefreshUI();

        // play option event ?
        Game.Instance.PlayEvent(option.outcomeEvent);
    }

    void RefreshUI()
    {
        // enable normal 'continue' button
        endTurnButton.SetActive(true);

        // delete options
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("option")) {
            Destroy(go);
        }


    }

    


}
