using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    private GameObject LayerMask;
    [SerializeField]
    private float i;
	// Use this for initialization
	void Start () {
        i = 2;
        LayerMask = Camera.main.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
       if(i < 1)
        {
            i += 0.01f;
            LayerMask.GetComponent<SpriteRenderer>().color = new Color(0,0,0,i);
        }
    }

	public void LoadSceneAfter()
	{
		int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        //		Debug.Log(SceneIndex);
        StartCoroutine("FadeInAndOut1",SceneIndex);
		
		
	}

	public void ReLoad(){
        //		Application.LoadLevel(Application.loadedLevelName);
        StartCoroutine("FadeInAndOut2");

    }


    public void MainScene(){
        StartCoroutine("FadeInAndOut3");
    }

    public void ToChoose(){
        StartCoroutine("FadeInAndOut4");

    }

	public void ToStages(string StageName){
        StartCoroutine("FadeInAndOut6", StageName);
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    IEnumerator FadeInAndOut1(int Index)
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(Index + 1);
    }

    IEnumerator FadeInAndOut2()
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeInAndOut3()
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeInAndOut4()
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("ChooseStage");
    }

    IEnumerator FadeInAndOut5(int Name)
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(Name);
    }

    IEnumerator FadeInAndOut6(string Name)
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(Name);
    }
}
