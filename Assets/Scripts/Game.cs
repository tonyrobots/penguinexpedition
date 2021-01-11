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


    [SerializeField] private TextMeshProUGUI mainText = null;

    // Start is called before the first frame update
    void Start()
    {
        
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
        morale -= 1;

        // if out of food, pengy dies
        if (food <= 0) {
            food = 0;
            penguins--;
        }

        // if out of pengys, game over
        if (penguins <= 0)
        {
            penguins = 0;
            Debug.Log("Game over!");
        }
        // if we have traveled to the goal, game over - winner, winner!
        if (distance >= goalDistance)
        {
            penguins = 0;
            Debug.Log("Game over!");
        }
    }

    public void GetRandomEvent()
    {
        Event currentEvent = EventManager.Instance.events[Random.Range(0,EventManager.Instance.events.Count)];
        PlayEvent(currentEvent);

    }

    public void PlayEvent(Event e)
    {
        mainText.text = e.Copy;
        e.ApplyEffects();
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
