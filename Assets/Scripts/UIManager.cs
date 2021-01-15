using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private GameObject eventPanel = null;  

    [SerializeField]
    private GameObject actionsPanel= null;

    [SerializeField]
    public GameObject endTurnButton = null;

    [SerializeField]
    public GameObject winButton = null;
    [SerializeField]
    public GameObject loseButton = null;

    [SerializeField]
    private GameObject optionButtonsPanel = null;

    [SerializeField]
    private GameObject optionButtonPrefab = null;

    public Image fadeOutUIImage;


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
        if (option.outcomeEvent) {
            Game.Instance.PlayEvent(option.outcomeEvent);
        } else {
            Debug.Log("no option event for option: " + option.copy);
            Game.Instance.Endturn();
        }
    }

    public void RefreshUI()
    {
        // enable normal 'continue' button
        endTurnButton.SetActive(true);

        // delete options
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("option")) {
            Destroy(go);
        }


    }

    public IEnumerator NightFade()
    {
        eventPanel.SetActive(false);
        yield return StartCoroutine(Fade(false, .5f));
        eventPanel.SetActive(true);

        yield return StartCoroutine(Fade(true, .5f));

    }


    private IEnumerator Fade(bool fadeIn, float secs)
    {
        Debug.Log ("Start Fade: " + fadeIn);
        float alpha = (fadeIn == true) ? 1 : 0;
        float fadeEndValue = (fadeIn == true) ? 0 : 1;
        if (fadeIn == true)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeIn, secs);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeIn, secs);
                yield return null;
            }
        }
    }
    private void SetColorImage(ref float alpha, bool fadeIn, float fadeSpeed)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeIn == true) ? -1 : 1);
    }
    


}
