using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenceManage : MonoBehaviour {

	public static int StageSave;

    public int scence;

    [SerializeField]
    private GameObject LayerMask;
    [SerializeField]
    private float i;
    // Use this for initialization
    public void Start () {

        i = 2;
        LayerMask = Camera.main.transform.GetChild(0).gameObject;


        //SceneManager.LoadScene(36);
    }
	
	// Update is called once per frame
	void Update () {
        if (i < 1)
        {
            i += 0.01f;
            LayerMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
        }
    }

     public void StartGame()
    {
        StageSave = PlayerPrefs.GetInt("Stage", 0);

        Debug.Log(StageSave);
        StartCoroutine("FadeInAndOut");
    }

    IEnumerator FadeInAndOut()
    {
        i = 0;
        LayerMask.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(36);
    }
}
