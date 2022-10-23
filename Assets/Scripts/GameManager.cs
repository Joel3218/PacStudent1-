

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pacStudent;
    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource eating1;
    public AudioSource eating2;
    public int currentEating = 0;

    public int score;
    public Text scoreText;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;

    public GameObject redGhost;
    public GameObject pinkGhost;
    public GameObject blueGhost;
    public GameObject orangeGhost;

    public int totalPellets;
    public int pelletsRemaining;
    public int pelletsCollected;

    public bool deathInThisLevel = false;

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode ghostMode;


    // Start is called before the first frame update
    void Awake()
    {
        pinkGhost.GetComponent<GhostController>().leaveHome = true;
        ghostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStart = true;
        pacStudent = GameObject.Find("Player");
        score = 0;
        currentEating = 0;
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPellet()
    {
        totalPellets++;
        pelletsRemaining++;
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score:" + score.ToString();
    }

    public void CollectedPellet(NodeController nodeController)
    {
        if (currentEating == 0)
        {
            eating1.Play();
            currentEating = 1;
        }
        else if (currentEating == 1)
        {
            eating2.Play();
            currentEating = 0;
        }

        pelletsRemaining--;
        pelletsCollected++;

        int requiredBluePellets = 0;
        int requiredOrangePellets = 0;

        if (deathInThisLevel)
        {
            requiredBluePellets = 12;
            requiredOrangePellets = 32;
        }
        else
        {
            requiredBluePellets = 30;
            requiredOrangePellets = 60;
        }

        if (pelletsCollected >= requiredBluePellets && !blueGhost.GetComponent<GhostController>().alreadyLeftHome)
        {
            blueGhost.GetComponent<GhostController>().leaveHome = true;
        }

        if (pelletsCollected >= requiredOrangePellets && !orangeGhost.GetComponent<GhostController>().alreadyLeftHome)
        {
            orangeGhost.GetComponent<GhostController>().leaveHome = true;
        }
        AddScore(10);

        //add to the score


        //check if pellets are left

        //check how many pellets are eating

        //check if its a power pellet
    }
}