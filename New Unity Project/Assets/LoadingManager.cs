using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour {

    public static LoadingManager instance;
    public GameObject loadingScreenObj;
    public Slider slider;
    AsyncOperation async;

    private void Awake()
    {
        //Check if instance already exist
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this) //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a TouchManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
        // Use this for initialization
        void Start () {
		
	}

    public IEnumerator LoadingScreen(int lvl)
    {
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;

        while(async.isDone==false)
        {
            slider.value = async.progress;
            if(async.progress==0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }

	// Update is called once per frame
	void Update () {
		
	}
}
