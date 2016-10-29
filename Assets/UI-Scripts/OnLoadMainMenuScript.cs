using UnityEngine;
using System.Collections;


//
// Summary:
//     ///
//     A Script which will force the cursor to be visible in the scene if applied to a game object in a scene.
//     ///
public class OnLoadMainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
