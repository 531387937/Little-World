using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenceManage : MonoBehaviour {

	public static int StageSave;

    public int scence;
	// Use this for initialization
	public void Start () {
        
        StageSave = PlayerPrefs.GetInt("Stage",0);

		Debug.Log (StageSave);

        SceneManager.LoadScene(36);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
