using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseStage : MonoBehaviour {
    [SerializeField]
    private GameObject LayerMask;
    [SerializeField]
    private float i;
    [SerializeField]
    private GameObject UnlockGroup;
    [SerializeField]
    private int Save;

    void Start()
    {
        i = 2;
        LayerMask = Camera.main.transform.GetChild(0).gameObject;
        Save = ScenceManage.StageSave;

        switch (SceneManager.GetActiveScene().name)
        {
            case "First":
                break;
            case "Second":
                Save -= 6;
                break;
            case "Third":
                Save -= 12;
                break;
            case "Fourth":
                Save -= 18;
                break;
            case "Fifth":
                Save -= 24;
                break;
            default:
                break;
        }
        for (int Scene = 0; Scene < 6; Scene++)
        {
            GameObject temp = UnlockGroup.transform.GetChild(Scene).gameObject;
            if (Save >= Scene)
            {
                temp.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (i < 1)
        {
            i += 0.01f;
            LayerMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
        }


    }

    public void First(){
        StartCoroutine("FadeInAndOut1", "First");
	}

	public void Second(){
		if (ScenceManage.StageSave <= 5)
			return;
        StartCoroutine("FadeInAndOut1", "Second");
    }

	public void Third(){
		if (ScenceManage.StageSave <= 11)
			return;
        StartCoroutine("FadeInAndOut1", "Third");
    }

	public void Fourth(){
		if (ScenceManage.StageSave <= 17)
			return;
        StartCoroutine("FadeInAndOut1", "Fourth");
        //SceneManager.LoadScene ("Fourth");
    }

    public void Fifth(){
		if (ScenceManage.StageSave <= 23)
			return;
        StartCoroutine("FadeInAndOut1", "Fifth");
		//SceneManager.LoadScene ("Fifth");
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
        StartCoroutine("FadeInAndOut2", 6 + param);
		//SceneManager.LoadScene (6 + param);
    }
    public void Stage2(int param)
    {
		if (ScenceManage.StageSave <= 5 + param)
			return;
        StartCoroutine("FadeInAndOut2", 12 + param);
        //SceneManager.LoadScene(12 + param);
    }
    public void Stage3(int param)
    {
		if (ScenceManage.StageSave <= 11 + param)
			return;
        StartCoroutine("FadeInAndOut2", 18 + param);
        //SceneManager.LoadScene(18 + param);
    }
    public void Stage4(int param)
    {
		if (ScenceManage.StageSave <= 17 + param)
			return;
        StartCoroutine("FadeInAndOut2", 24 + param);
        //SceneManager.LoadScene(24 + param);
    }
    public void Stage5(int param){

		if (ScenceManage.StageSave <= 23 + param)
			return;
        StartCoroutine("FadeInAndOut2", 30 + param);
		//SceneManager.LoadScene (30 + param);
    }

    IEnumerator FadeInAndOut1(string Name)
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(Name);
    }
    IEnumerator FadeInAndOut2(int Index)
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(Index);
    }
}
