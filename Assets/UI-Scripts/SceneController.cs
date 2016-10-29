using UnityEngine;
using System.Collections;

//
// Summary:
//     ///
//     A ControllerScript that controls the sceneflow and UserInterface, MUST ALWAYS be present in the scene on a Seperate GameObject named: "SceneControllerObject".
//     ///

public class SceneController : MonoBehaviour  {

  
    //Private variables that are serialized can be seen from the inspector, below are a list of canvas variables that can take a canvas and use it in the private interface, 
    //for the SceneController. This makes it possible to have more than one submenu in the same scene, and can also be used for ingame inventory, savegame, merchants and more.
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 0, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas mainMenuCanvas;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 1, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas creditsCanvas;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 2, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas inGamePauseCanvas;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 3, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas inGameStandardCanvas;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 4, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_4;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 5, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_5;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 6, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_6;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 7, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_7;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 8, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_8;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 9, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_9;
    [SerializeField]
    [Tooltip("Insert Canvas, method value = 10, the canvas must be uncommented in canvasSwapper() - line 77+")]
    private Canvas placeholder_10;



    public bool isPaused = false;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        StartCoroutine("inGamePause");
        //Debug.Log(Time.timeScale);

          
        
	}

    public void sceneLoader(string x)
    {
        Time.timeScale = 1.0f; // this line ensures that the timeScale is resumed to normal if the scene is changed
        Cursor.lockState = CursorLockMode.Locked; //Fixes an issue that makes the camera move on load, if it is bound to the cursor, by locking the cursor to the center of the screen before loading. However, this must be compensated for with another script in the MainMenu
        UnityEngine.SceneManagement.SceneManager.LoadScene(x);
    }
    
    public void canvasSwapper(int x)

    {

        if (x >= 0 && x<=10)

            switch (x)
            {
                case 1:
                    {
                       
                        creditsCanvas.enabled = true;
                        mainMenuCanvas.enabled = false;


                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;

                        break;
                    }
                default:
                    {
                        
                        creditsCanvas.enabled = false;
                        mainMenuCanvas.enabled = true;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }

                case 2:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        inGamePauseCanvas.enabled = true;
                        inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 3:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        inGamePauseCanvas.enabled = false;
                        inGameStandardCanvas.enabled = true;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 4:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = true;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 5:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = true;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 6:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = true;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 7:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = true;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 8:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = true;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 9:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = true;
                        //placeholder_10.enabled = false;
                        break;
                    }
                case 10:
                    {
                        //mainMenuCanvas.enabled = false;
                        //creditsCanvas.enabled = false;

                        //inGamePauseCanvas.enabled = false;
                        //inGameStandardCanvas.enabled = false;
                        //placeholder_4.enabled = false;
                        //placeholder_5.enabled = false;
                        //placeholder_6.enabled = false;
                        //placeholder_7.enabled = false;
                        //placeholder_8.enabled = false;
                        //placeholder_9.enabled = false;
                        //placeholder_10.enabled = true;
                        break;
                    }
            }


        else
            Debug.Log("The value entered for the'canvasSwapper(int x)' method is not within the switch. Check Controller script line 40+ to add another value! Please note that the canvas must be added to the script, and either public or serialized in the code. Default is set to MainMenu");

    }



    public bool scenePauser(bool pauseState)
    {
        
        if (pauseState)
        {
            Time.timeScale = 0.0f;
            
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            
            isPaused = false;
        }
        return isPaused;
    }



   IEnumerator inGamePause()
    {
        if (Input.GetKeyDown(KeyCode.Pause) && !isPaused)
        {
            canvasSwapper(2);
            scenePauser(true);
            yield return new WaitForSeconds(1f);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            scenePauser(false);
            canvasSwapper(3);

            yield return new WaitForSeconds(1f);
        }
        if (Input.GetKeyDown(KeyCode.Pause) && isPaused)
        {
            scenePauser(false);
            canvasSwapper(3);
            yield return new WaitForSeconds(1f);
        }
    }

    public bool getPauseState()
    {
        
        return isPaused;
            
    }


}
