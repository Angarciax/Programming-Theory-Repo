using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Maze maze;
    [SerializeField] private float speedRotation = 45.0f;
    public GameObject wallObject;
    public GameObject playerObject;
    public GameObject keyObject;
    public GameObject goodEnemyObject;
    public GameObject badEnemyObject;
    public GameObject doorObject;
    public Text gameOverText;

    public bool gotKey { get; set; }
    public bool gameOver { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        maze = new Maze();
        for( int posY = 0; posY < maze.size; posY++ )
            for( int posX = 0; posX < maze.size; posX++)
            {
                if( maze.IsWall(posY,posX))
                {
                    float wallX = posX - (maze.size / 2);
                    float wallZ = posY - (maze.size / 2);
                    Instantiate(wallObject,new Vector3(wallX, 0.5f, wallZ ),transform.rotation);
                } 
            }
        // Create a Player at the bottom
        float playerX = 1 - (maze.size / 2);
        float playerZ = 1 - (maze.size / 2);
        Instantiate( playerObject, new Vector3(playerX, 0.5f, playerZ), transform.rotation);
        // Create a Key at Random
        int positionKey = maze.SetKey();
        float keyX = (positionKey % 100 ) - (maze.size / 2);
        float keyZ = (positionKey / 100) - (maze.size / 2);
        Instantiate(keyObject, new Vector3(keyX, 0.5f, keyZ), transform.rotation);
        // Create a good Enemy at Random
        int positionGoodEnemy = maze.SetGoodEnemy();
        float goodEX = (positionGoodEnemy % 100) - (maze.size / 2);
        float goodEZ = (positionGoodEnemy / 100) - (maze.size / 2);
        Instantiate(goodEnemyObject, new Vector3(goodEX, 0.5f, goodEZ), transform.rotation);
        // Create a bad Enemy at Random
        int positionBadEnemy = maze.SetBadEnemy();
        float badEX = (positionBadEnemy % 100) - (maze.size / 2);
        float badEZ = (positionBadEnemy / 100) - (maze.size / 2);
        Instantiate(badEnemyObject, new Vector3(badEX, 0.5f, badEZ), transform.rotation);
        // Create Door Object
        float doorX = (maze.size - 3 ) - (maze.size / 2);
        float doorZ = (maze.size - 1) - maze.size / 2;
        doorObject = Instantiate(doorObject, new Vector3(doorX, 0.5f, doorZ), transform.rotation);

        gotKey = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(new Vector3(0, speedRotation * Time.deltaTime, 0));

        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            transform.Rotate(new Vector3(0, -speedRotation * Time.deltaTime, 0));

        }
        // Anytime reload id ESC pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if ( gotKey && doorObject.activeSelf )
        {
            doorObject.SetActive(false) ;
        }
        if( gameOver)
        {
            gameOverText.gameObject.SetActive(true);
        }

    }
}
