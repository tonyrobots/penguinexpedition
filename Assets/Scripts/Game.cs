﻿using System.Collections;
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

    [SerializeField] private GameObject illustrationImageGO;
    private Image illustrationImage;

    [SerializeField] private TextMeshProUGUI mainText = null;
    [SerializeField] private TextMeshProUGUI effectsSummaryText = null;


    // Start is called before the first frame update
    void Start()
    {
        illustrationImage = illustrationImageGO.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Endturn()
    {

        if (penguins <= 0) return;

        // Generate event
        GetRandomEvent();

        // advance turn
        turn++;

        // consume food
        food -= penguins;

        // make progress
        distance += 6;

        // morale decays
        morale -= 5;

        // if out of food, pengy dies
        if (food <= 0) {
            food = 0;
            penguins--;
        }

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
        } else {
            illustrationImage.enabled = false;
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
