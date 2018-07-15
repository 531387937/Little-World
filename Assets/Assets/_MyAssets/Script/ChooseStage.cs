using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseStage : MonoBehaviour {

	public void First(){
		SceneManager.LoadScene ("First");
	}

	public void Second(){
		if (ScenceManage.StageSave <= 5)
			return;
		SceneManager.LoadScene ("Second");
	}

	public void Third(){
		if (ScenceManage.StageSave <= 11)
			return;
		SceneManager.LoadScene ("Third");
	}

	public void Fourth(){
		if (ScenceManage.StageSave <= 17)
			return;
		SceneManager.LoadScene ("Fourth");
	}

	public void Fifth(){
		if (ScenceManage.StageSave <= 23)
			return;
		SceneManager.LoadScene ("Fifth");
	}
	/// <summary>
	/// //////////////////////////////////////////////////////
	/// </summary>
	/// <param name="param">Parameter.</param>
	public void Stage1(int param){
//		if (ScenceManage.StageSave == 0 && param == 0)
//			Debug.Log("start");
//		else 
			if (ScenceManage.StageSave < param)
			return;
		SceneManager.LoadScene (6 + param);
	}
    public void Stage2(int param)
    {
		if (ScenceManage.StageSave <= 5 + param)
			return;
        SceneManager.LoadScene(12 + param);
    }
    public void Stage3(int param)
    {
		if (ScenceManage.StageSave <= 11 + param)
			return;
        SceneManager.LoadScene(18 + param);
    }
    public void Stage4(int param)
    {
		if (ScenceManage.StageSave <= 17 + param)
			return;
        SceneManager.LoadScene(24 + param);
    }
    public void Stage5(int param){

		if (ScenceManage.StageSave <= 23 + param)
			return;
		SceneManager.LoadScene (30 + param);
	}
}
