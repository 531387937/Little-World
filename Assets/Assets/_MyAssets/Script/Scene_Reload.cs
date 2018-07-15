using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Reload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reload(){
//		Application.LoadLevel(Application.loadedLevelName);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
