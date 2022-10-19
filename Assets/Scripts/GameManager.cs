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

    public EnemyController redGhostControl;
    public EnemyController orangeGhostControl;
    public EnemyController pinkGhostControl;
    public EnemyController blueGhostControl;

    public List<NodeController> nodeControllers = new List<NodeController>();

    public bool deathInThisLevel = false;

    public bool levelRunning;

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode ghostMode;
    public bool newGame;
    public bool clearLevel;


    // Start is called before the first frame update
    void Awake()
    {

        newGame = true;
        clearLevel = false;
        redGhostControl = redGhost.GetComponent<EnemyController>();
        orangeGhostControl = orangeGhost.GetComponent<EnemyController>();
        blueGhostControl = blueGhost.GetComponent<EnemyController>();
        pinkGhostControl = pinkGhost.GetComponent<EnemyController>();
        levelRunning = true;
        pinkGhost.GetComponent<EnemyController>().leaveHome = true;
        ghostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<NodeController>().isGhostStart = true;
        pacStudent = GameObject.Find("Player");
        score = 0;
        currentEating = 0;
        siren.Play();
    }

    public IEnumerator Setup()
    {
        if (clearLevel)
        {
            yield return new WaitForSeconds(0.1f);
        }

        pelletsCollected = 0;
        ghostMode = GhostMode.scatter;
        levelRunning = false;

        float timer = 1f;


        if (clearLevel || newGame)
        {
            timer = 4f;
            for (int i = 0; i < nodeControllers.Count; i++) //respawning the pellets after level is complete or a new game
            {
                nodeControllers[i].RespawnPellet();
            }
        }
      
        pacStudent.GetComponent<PlayerController>().Setup();

        redGhostControl.Setup();
        orangeGhostControl.Setup();
        blueGhostControl.Setup();
        pinkGhostControl.Setup();

        newGame = false;
        clearLevel = false;
        yield return new WaitForSeconds(timer);
        StartGame();
    }

    void StartGame()
    {
        levelRunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPellet(NodeController nodeController)
    {
        nodeControllers.Add(nodeController);
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

        if (pelletsCollected >= requiredBluePellets && !blueGhost.GetComponent<EnemyController>().alreadyLeftHome)
        {
            blueGhost.GetComponent<EnemyController>().leaveHome = true;
        }

        if (pelletsCollected >= requiredOrangePellets && !orangeGhost.GetComponent<EnemyController>().alreadyLeftHome)
        {
            orangeGhost.GetComponent<EnemyController>().leaveHome = true;
        }
        AddScore(10);

        //add to the score


        //check if pellets are left

        //check how many pellets are eating

        //check if its a power pellet
    }
}
