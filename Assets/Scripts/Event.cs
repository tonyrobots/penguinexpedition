﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]

public class Event : ScriptableObject

{
    [SerializeField] private string title = "Something happened.";
    [TextArea]
    [SerializeField] private string copy = "You won't believe what happened.";
    [SerializeField] private List<EventEffect> eventEffects = null;
    [SerializeField] private List<Option> options = null;

    // [SerializeField] private Event nextEvent;
    public string id;

    public Sprite illustration;


    [System.Serializable]
    public struct EventEffect {
        public Counters key;
        public int value;
        public int valueMax;
    }

    public string Title => title;
    public string Copy => copy;
    // public Event NextEvent => nextEvent;
    public List<EventEffect> EventEffects => eventEffects;
    public bool repeatable;


    [System.Serializable]
    public struct Option
    {
        public string copy;
        public Event outcomeEvent;
    }



    public List<Option> Options => options;


    public string ApplyEffects()
    {
        string effectsSummary = "";

        foreach (EventEffect effect in eventEffects)
        {
            int value;

            if (effect.valueMax != 0) {
                value = Random.Range(effect.value, effect.valueMax);  
            } else {
                value = effect.value;
            }

            // add temperature effect
            if (effect.key == Counters.MORALE || effect.key == Counters.DISTANCE)
            {
                value = value + Game.Instance.TemperatureModifier();
            }
            // for everything that gets tallied by tally counter
            Game.Instance.TallyCounterForDay(effect.key, value);

            // for goal distance as special case, since negative numbers are good.
            if (effect.key == Counters.GOAL_DISTANCE)
            {
                Game.Instance.goalDistance -= value;
            }
            effectsSummary += FormatEffectSummary(effect.key, value);
        }

        return effectsSummary;
    }

    public string FormatEffectSummary(Counters key, int value)
    {
        // no need to mention unchanged values
        if (value == 0) return "";

        string effectSummary = (value > 0) ? "<color=#074905>" : "<color=\"red\">";

        if (key == Counters.GOAL_DISTANCE) {
            // special case, b/c it's a little goofy
            effectSummary += "Distance to goal ";
            effectSummary += (value > 0) ? " -":" +";
            effectSummary += Mathf.Abs(value);
        } else {
            effectSummary += key.ToString().ToLower();
            // set +/-
            if (value > 0)
            {
                effectSummary += " +";
            }
            else
            {
                effectSummary += " ";

            }

            if (key == Counters.MORALE)
            {
                effectSummary += value * 100/Game.Instance.startingMorale + "%";
            } else {
                effectSummary += value;
            }
        }
        
         
        return effectSummary + "</color> \n";
    }

}

// {
//     string title = "We trudge on...";
//     string copy;
    
//     void Execute() 
//     {
//         int miles = Random.Range(3,7);
//         int eaten = Game.Instance.penguins;
//         string preCopy = $"Our party of intrepid penguins, undaunted by the bitter cold and near hopelessness of the task ahead, trudge on, advancing {miles} miles and consuming {eaten} food.";
//         Game.Instance.food -= eaten;
//         Game.Instance.distance += miles;
//     }
    
// }
