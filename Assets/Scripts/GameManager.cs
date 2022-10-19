/* using System.Collections;
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
    public AudioSource startGameAudio;

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

    public EnemyController redGhostController;
    public EnemyController orangeGhostController;
    public EnemyController pinkGhostController;
    public EnemyController blueGhostController;

    public List<NodeController> nodeControllers = new List<NodeController>();

    public bool deathInThisLevel = false;

    public bool gameisRunning;

    public enum GhostMode
    {
        chase, scatter
    }

    public GhostMode ghostMode;
    public bool newGame;
    public bool clearLevel;

    

    public int lives;
    public int currentLevel;


    // Start is called before the first frame update
    void Awake()
    {
        
        newGame = true;
        clearLevel = false;
        redGhostController = redGhost.GetComponent<EnemyController>();
        orangeGhostController = orangeGhost.GetComponent<EnemyController>();
        blueGhostController = blueGhost.GetComponent<EnemyController>();
        pinkGhostController = pinkGhost.GetComponent<EnemyController>();
        
        
        
        ghostNodeStart.GetComponent<NodeController>().isGhostStart = true;
        pacStudent = GameObject.Find("Player");

        StartCoroutine(Setup());
        
    }

    public IEnumerator Setup()
    {
        
        if (clearLevel)
        {
            yield return new WaitForSeconds(0.1f);
        }

        pelletsCollected = 0;
        ghostMode = GhostMode.scatter;
        gameisRunning = false;
        currentEating = 0;

        float timer = 1f;


        if (clearLevel || newGame)
        {
            timer = 4f;
            for (int i = 0; i < nodeControllers.Count; i++) //respawning the pellets after level is complete or a new game
            {
                nodeControllers[i].RespawnPellet();
            }
        }
        if (newGame)
        {
            startGameAudio.Play();
            score = 0;
            scoreText.text = "Score: " + score.ToString();
            lives = 3;
            currentLevel = 1;

        }
      
        pacStudent.GetComponent<PlayerController>().Setup();

        redGhostController.Setup();
        orangeGhostController.Setup();
        blueGhostController.Setup();
        pinkGhostController.Setup();

        newGame = false;
        clearLevel = false;
        yield return new WaitForSeconds(timer);
        StartGame();
    }

    void StartGame()
    {
       gameisRunning = true;
        siren.Play();
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
*/

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
        pinkGhost.GetComponent<EnemyController>().leaveHome = true;
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