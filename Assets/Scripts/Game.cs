using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Game : Singleton<Game>
{

    public int turn = 0;
    // don't actually want these to be public
    public int food = 100;
    public int penguins = 10;


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
        GetEvent();

        // advance turn
        turn++;

        // consume food
        food -= penguins;

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
    }

    public void GetEvent()
    {   
        // load events file (json?)
        // randomly choose event, based on current gamestate
        // return event

        // this is temp
        mainText.text = "You happen across a friendly walrus who points you to a hidden cache of delicious fish.";
        food += 25;
        

    
    }
}
