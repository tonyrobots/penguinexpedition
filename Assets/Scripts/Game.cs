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
    PENGUINS,
    TEMPERATURE
}

public enum Activities
{
    FISH,
    SAFE_TRAVEL,
    RISKY_TRAVEL,
    FROLIC,
    NONE
}


public class Game : Singleton<Game>
{

    public int turn = 0;
    public int startingMorale=60;
    // don't actually want these to be public
    public int food = 100;
    public int penguins = 10;
    public int distance = 0;
    public int morale = 60;
    public int goalDistance = 100;

    private bool planning = true;
    private bool gameOver = false;
    public Activities daysActivity;

    public struct DayTally
    {
        public int DistanceTraveled;
        public int MoraleGained;
        public int FishEaten;
        public int FishCaught;
        public int PenguinsGained;
    }

    public DayTally[] daysTally;

    public Event nextEvent;

    public AudioController musicPlayer;
    public AudioController ambientPlayer;

    [SerializeField] private GameObject illustrationImageGO = null;
    private Image illustrationImage;

    [SerializeField] private TextMeshProUGUI mainText = null;
    [SerializeField] private TextMeshProUGUI effectsSummaryText = null;


    // Start is called before the first frame update
    void Start()
    {
        illustrationImage = illustrationImageGO.GetComponent<Image>();
        UIManager.Instance.SetActionsPanelVisibility(false);
        daysTally = new DayTally[32];

        if (GameObject.FindGameObjectWithTag("music") != null) {
            musicPlayer = GameObject.FindGameObjectWithTag("music").GetComponent<AudioController>();
        }
        if (GameObject.FindGameObjectWithTag("ambient") != null) {
            ambientPlayer = GameObject.FindGameObjectWithTag("ambient").GetComponent<AudioController>();
        }


        // temp
        if (musicPlayer != null)
        {
            musicPlayer.PlayClip(0);
            musicPlayer.gameObject.AddComponent<ScoreController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Core game loop:
    //
    // 1. Plan - set activity for next day
    // 2. display outcome - could be a special event, or could be normal outcome
    // 3. back to planning, where we also show net results

    public void SetActivity(int activity_int)
    {
        Activities activity;
        // this is hacky, used because can't set enum as parameter in unity inspector for buttons
        switch (activity_int)
        {
            case 1:
                activity = Activities.FISH;
                break;
            case 2:
                activity = Activities.SAFE_TRAVEL;
                break;
            case 3:
                activity = Activities.RISKY_TRAVEL;
                break;
            case 4:
                activity = Activities.FROLIC;
                break;
            default:
                activity = Activities.NONE;
                break;
        }
        daysActivity = activity;
        StartCoroutine(UIManager.Instance.NightFade());
        Endturn();
    }

    public void Continue()
    {
        Endturn();
    }

    // Should clean this up and split out different functions, it's doing way too much.
    public void Endturn()
    {

        // if (activity !=0)  {
        //     Debug.Log("action: " + activity);
        // }
        if (penguins <= 0 || gameOver) return;

        if (!planning) {
            // hide planning actions
            UIManager.Instance.SetActionsPanelVisibility(false);

            // advance turn
            turn++;

            // Generate event
            PlayEvent(EventManager.Instance.GetRandomEvent(daysActivity));

            // // Should we have a random event? Or standard?
            // float diceRoll = Random.Range(0,1f);

            // if (diceRoll < .40) {
            //     // Generate event
            //     PlayEvent(EventManager.Instance.GetRandomEvent(daysActivity));
            // } else {
            //     // ShowOutcome(daysActivity);
            // }
        } else {

            // Night/Planning phase
            

            UIManager.Instance.SetActionsPanelVisibility(true);

            mainText.text = "";

            // DETERMINE EFFECTS OF DAY'S ACTIVITY

            // switch (daysActivity)
            // {
            //     // should make these all events? YES
            //     case Activities.FISH:
            //         // stop and fish
            //         //daysTally[turn].FishCaught += Random.Range(5, 25);
            //         // daysTally[turn].MoraleGained = Mathf.RoundToInt(daysTally[turn].FishCaught /5);
            //         //daysTally[turn].DistanceTraveled += 0;
            //         break;
            //     case Activities.SAFE_TRAVEL:
            //         // safe route
            //         //daysTally[turn].MoraleGained = Random.Range(-10, 2);
            //         //daysTally[turn].DistanceTraveled += Random.Range(5,10);
            //         break;
            //     case Activities.RISKY_TRAVEL:
            //         // aggressive route
            //         // daysTally[turn].MoraleGained = Random.Range(-20, 5);
            //         // daysTally[turn].DistanceTraveled += Random.Range(10, 20);
            //         break;

            //     case Activities.FROLIC:
            //         // rest and frolic!
            //         // daysTally[turn].MoraleGained = Random.Range(5, 15);
            //         // daysTally[turn].DistanceTraveled += 0;
            //         break;
                
            //     default:
            //         break;

            // }

            // Apply fish eaten to day's tally
            daysTally[turn].FishEaten += penguins;

            // APPLY EFFECTS/TALLY
            ApplyTally();



            // clear effects display from special events
            effectsSummaryText.text = "";

            // if out of food, pengy dies
            if (food <= 0)
            {
                food = 0;
                //PlayEvent(EventManager.Instance.FindEventByID("no_food"));
                penguins--;
            }

            if (turn > 0) {
                mainText.text += TurnReport(turn);
                mainText.text += "What shall we do in the morning?";
            } else {
                mainText.text += "Tonight we gather our strength, and think of our beloveds so far away. We are coming home to you, we swear it.\n";
                mainText.text += "A great journey starts with but a single waddle.\n How shall we begin?";
            }
            // check win/lose conditions:

            // if we have traveled to the goal, game over - winner, winner!
            if (distance >= goalDistance)
            {
                PlayEvent(EventManager.Instance.FindEventByID("win"));
                GameOver("win");
            }
            else

            // if morale is exhausted, game over
            if (morale <= 0)
            {
                morale = 0;
                PlayEvent(EventManager.Instance.FindEventByID("lose"));
                GameOver("lose");
            }
            else

            // if out of pengys, game over
            if (penguins <= 0)
            {
                penguins = 0;
                PlayEvent(EventManager.Instance.FindEventByID("lose"));
                GameOver("lose");

                Debug.Log("Game over!");
            }

        }
    

        // flip day/night
        planning = !planning;
        
    }

    void GameOver(string type) {
        UIManager.Instance.SetActionsPanelVisibility(false);
        if (type == "win") {
            UIManager.Instance.winButton.SetActive(true);
        } else {
            UIManager.Instance.loseButton.SetActive(true);
        }
        UIManager.Instance.endTurnButton.SetActive(false);
        gameOver = true;
        // FindObjectOfType<SceneFader>().SwitchSceneWithFade(1);
        // now what?
    }

    public void PlayEvent(Event e)
    {
        mainText.text = e.Copy;
        // add effects outcome summary text
        effectsSummaryText.text = e.ApplyEffects();

        // process event options
        if (e.Options.Count > 0) 
        {
            // show options
            UIManager.Instance.ShowEventOptions(e.Options);
        }
        if (e.illustration != null) 
        {
            illustrationImage.overrideSprite = e.illustration;
            illustrationImage.enabled = true;
            mainText.rectTransform.sizeDelta = new Vector2(291,122.4f);
        } else {
            illustrationImage.enabled = false;
            mainText.rectTransform.sizeDelta = new Vector2(492, 122.4f);

        }
    }

    public void TallyCounterForDay(Counters type, int value)
    {
        switch (type)
        {
            case Counters.FOOD:
                daysTally[turn].FishCaught += value;
                break;
            case Counters.MORALE:
                daysTally[turn].MoraleGained += value;
                break;
            case Counters.DISTANCE:
                daysTally[turn].DistanceTraveled += value;
                break;
            case Counters.PENGUINS:
                daysTally[turn].PenguinsGained += value;
                break;
        }
    
    }

    private void ApplyTally()
    {
        if (turn == 0) return;
        // add food to larder
        food += daysTally[turn].FishCaught;

        // consume food
        food -= daysTally[turn].FishEaten;

        // make progress
        distance += daysTally[turn].DistanceTraveled;

        // morale decays
        morale += daysTally[turn].MoraleGained;
        morale = Mathf.Min(morale, startingMorale);
    }

    private string TurnReport(int turn) {

        string report = "";
        report += $"Today we traveled {daysTally[turn].DistanceTraveled} miles, ";

        if (daysTally[turn].FishCaught > 0)
        {
            report += $"caught { daysTally[turn].FishCaught} fish, & ate {daysTally[turn].FishEaten} of them.\n";
        }
        else
        {
            report += $"& ate {daysTally[turn].FishEaten} fish. \n";
        }

        if (food <= 0) {
            report += "We are out of fish, and one of our party has starved.";
        
        } else if (food < 20) {
            report += "Fish stocks are getting low. \n";
        }

        if (daysTally[turn].MoraleGained > 0) {
            report +=  "Spirits were raised somewhat today, ";
            if (MoralePct() < 30)
            {
                report += "but perhaps a day of rest is in order?";
            } else {
                report += "thankfully.";
            }
        } else if (daysTally[turn].MoraleGained < 0) {
            report +=  "The day was hard, and our spirit burns a bit dimmer; ";
            if (MoralePct() < 30)
            {
                report += "perhaps a day of frolic would help?";
            }
        }


        return report + "\n";
    }

    public float MoralePct() {
        return 100f * morale / startingMorale;
    }

    // public void GetEvent()
    // {
    //     // load events file (json?)
    //     // randomly choose event, based on current gamestate

    //     event currentEvent = EventManager.Instance.events[0];
    //     // return event

    
    // }
}
