﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeGameSwitch : MonoBehaviour
{
    
    public GameObject Player;

    // Entry positions for each game
    public GameObject Spawn;
    public GameObject Airhockey;
    public GameObject Pinball;
    public GameObject Puffman;
    public GameObject CraneGame;
    public GameObject Snake;
    public GameObject TicketBooth;

    // Keep track of which game/position the player is in
    private int gameindex;
    private Dictionary<int, GameObject> gamelist;

    public static ArcadeGameSwitch instance = null;

    private void Awake()
    {

        if (ArcadeGameSwitch.instance == null)
        {
            ArcadeGameSwitch.instance = this;
        }
        
        // Start at spawn
        gameindex = 0;
      
        // Add all games to list
        gamelist = new Dictionary<int, GameObject>();
        gamelist.Add(0, Spawn);
        gamelist.Add(1, Airhockey);
        gamelist.Add(2, Pinball);
        gamelist.Add(3, Puffman);
        gamelist.Add(4, CraneGame);
        gamelist.Add(5, Snake);
        gamelist.Add(6, TicketBooth);

    }

    // Update is called once per frame
    void Update()
    {
        // this checks if were in the main arcade, so we dont mess with the player location in game
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {

            if(TranslationLayer.instance.GetButtonDown(ButtonCode.KeyBack))
            {
                // Rotate view 90 degrees
                Player.transform.Rotate(0,90,0);

            }
            // Sip simulation
            if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyLeft))
            {
                gameindex = mod(gameindex - 1, 7);
                Player.transform.position = gamelist[gameindex].transform.position;
                Vector3 newPos = gamelist[gameindex].transform.eulerAngles;

                // set rotation and flip 180... idk why we have to flip.
                Player.transform.eulerAngles = new Vector3(
                    newPos.x,
                    newPos.y + 180,
                    newPos.z
                    );

            }

            // Puff
            if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyRight))
            {
                gameindex = mod(gameindex + 1, 7);
                Player.transform.position = gamelist[gameindex].transform.position;
                Vector3 newPos = gamelist[gameindex].transform.eulerAngles;

                // set rotation and flip 180
                Player.transform.eulerAngles = new Vector3(
                    newPos.x,
                    newPos.y + 180,
                    newPos.z
                    );

            }

            // Enter Game
            if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyFoward))
            {
                switch (gameindex)
                {
                    case 0:
                        break;

                    case 1:
                        // load airhockey
                        SceneManager.LoadScene("Airhockey", LoadSceneMode.Single);
                        break;

                    case 2:
                        // load pinball
                        SceneManager.LoadScene("Pinball", LoadSceneMode.Single);
                        break;

                    case 3:
                        // load Puffman
                        SceneManager.LoadScene( "Puffman", LoadSceneMode.Single);
                        break;

                    case 4:
                        // load cranegame
                        SceneManager.LoadScene("CraneGame", LoadSceneMode.Single);
                        break;

                    case 5:
                        // load snake
                        SceneManager.LoadScene("Snake", LoadSceneMode.Single);
                        break;

                    case 6:
                        // load ticket booth? there is no ticket booth scene.
                        // Bam was going to make some easter egg from this, or a way of redeeming tickets in the arcade
                        // It was a short semester...
                        // SceneManager.LoadScene("TicketBooth", LoadSceneMode.Single);
                        break;

                    default:
                        break;
                }
            }


        }
    
    }

    // An always positive mod
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
