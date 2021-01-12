using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Counters
{   
    FOOD,
    MORALE,
    DISTANCE,
    GOAL_DISTANCE,
    PENGUINS
}


public class Game : Singleton<Game>
{

    public int turn = 0;
    // don't actually want these to be public
    public int food = 100;
    public int penguins = 10;
    public int distance = 0;
    public int morale = 100;
    public int goalDistance = 100;

    private bool planning = true;

    [SerializeField] private GameObject illustrationImageGO;
    private Image illustrationImage;

    [SerializeField] private TextMeshProUGUI mainText = null;
    [SerializeField] private TextMeshProUGUI effectsSummaryText = null;


    // Start is called before the first frame update
    void Start()
    {
        illustrationImage = illustrationImageGO.GetComponent<Image>();
        UIManager.Instance.SetActionsPanelVisibility(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Endturn(int mode=0)
    {

        if (mode !=0)  {
            Debug.Log("action: " + mode);
        }

        if (penguins <= 0) return;

        if (!planning) {
            // hide planning actions
            UIManager.Instance.SetActionsPanelVisibility(false);

            // advance turn
            turn++;

            // Generate event
            GetRandomEvent();  
        } else {

            UIManager.Instance.SetActionsPanelVisibility(true);

            mainText.text = "<u>planning phase</u>\n";

            int todayDistance = 6;
            int todayMorale = -5;
            int todayFood = -penguins;

            // consume food
            food += todayFood;

            // make progress
            distance += todayDistance;

            // morale decays
            morale += todayMorale;

            // if out of food, pengy dies
            if (food <= 0) {
                food = 0;
                penguins--;
            }

            mainText.text += $"Today we traveled {todayDistance} miles, and ate {Mathf.Min(Mathf.Abs(todayFood),penguins)} fish.";

            // if morale is exhausted, game over
            if (morale <= 0)
            {
                morale = 0;
                PlayEvent(EventManager.Instance.FindEventByName("All is lost."));


            }

            // if out of pengys, game over
            if (penguins <= 0)
            {
                penguins = 0;
                PlayEvent(EventManager.Instance.FindEventByName("All is lost."));

                Debug.Log("Game over!");
            }
            // if we have traveled to the goal, game over - winner, winner!
            if (distance >= goalDistance)
            {
                PlayEvent(EventManager.Instance.FindEventByName("You have arrived!"));
                Debug.Log("Game over!");
            }
        }

        // flip day/night
        planning = !planning;
    }

    public void GetRandomEvent()
    {
        Event currentEvent = EventManager.Instance.randomEvents[Random.Range(0,EventManager.Instance.randomEvents.Count)];
        PlayEvent(currentEvent);

    }

    public void PlayEvent(Event e)
    {
        mainText.text = e.Copy;
        // add effects outcome summary text
        effectsSummaryText.text = e.ApplyEffects();

        if (e.illustration != null) 
        {
            illustrationImage.overrideSprite = e.illustration;
            illustrationImage.enabled = true;
            mainText.rectTransform.sizeDelta = new Vector2(291,122.4f);
        } else {
            illustrationImage.enabled = false;
            mainText.rectTransform.sizeDelta = new Vector2(492, 122.4f);

        }
        if (e.NextEvent != null)
        {
            PlayEvent(e.NextEvent);
        }
    }

    // public void GetEvent()
    // {
    //     // load events file (json?)
    //     // randomly choose event, based on current gamestate

    //     event currentEvent = EventManager.Instance.events[0];
    //     // return event

    
    // }
}
