using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class main : MonoBehaviour {

	public GameObject playbtn;
	public GameObject exitbtn;
	public GameObject quitpanel;
    public GameObject shoppanel;
    public GameObject shopbtn;
	public GameObject resetbtn;
	public GameObject resetscorepanel;
    public GameObject plane;
    public GameObject legendarypanel;
    public GameObject commonpanel;
    public GameObject rarepanel;
    public GameObject uicanvas;


    Persistence persistence;


#if UNITY_ANDROID || UNITY_IPHONE

    int skincost = 0;


    public Text cointext;
    
#endif

    void Start()
    {
        persistence = GameObject.FindWithTag("persistence").GetComponent<Persistence>();
        persistence.player = gameObject;
        DontDestroyOnLoad(uicanvas);
#if UNITY_ANDROID || UNITY_IPHONE
        cointext = GameObject.FindWithTag("cointext").GetComponent<Text>();
        cointext.text = "Coins: " + PlayerPrefs.GetInt("coins");
#endif
        if (PlayerPrefs.GetInt("level") == 0)
        {
            PlayerPrefs.SetInt("level", 1);

        }

        persistence.resetcolor();

        persistence.resetskin();
    }


    public void exit(int i = 0)
    {
        if (i == 1)
        {
            commonpanel.SetActive(false);
            rarepanel.SetActive(false);
            legendarypanel.SetActive(false);
            openpanel(3);
            return;
        }
        playbtn.SetActive(true);
        shopbtn.SetActive(true);
        resetbtn.SetActive(true);
        exitbtn.SetActive(true);
        quitpanel.SetActive(false);
        resetscorepanel.SetActive(false);
        shoppanel.SetActive(false);
        commonpanel.SetActive(false);
        rarepanel.SetActive(false);
        legendarypanel.SetActive(false);
    }

    public void openpanel(int i)
    {
        playbtn.SetActive(false);
        shopbtn.SetActive(false);
        exitbtn.SetActive(false);
        resetbtn.SetActive(false);
        switch (i)
        {
            case 1:
                quitpanel.SetActive(true);
                break;
            case 2:
                resetscorepanel.SetActive(true);
                break;
            case 3:
                shoppanel.SetActive(true);
                break;
            case 4:
                shoppanel.SetActive(false);
                commonpanel.SetActive(true);
                break;
            case 5:
                shoppanel.SetActive(false);
                rarepanel.SetActive(true);
                break;
            case 6:
                shoppanel.SetActive(false);
                legendarypanel.SetActive(true);
                break;
        }
    }

    public void setskincost(int id)
    {
#if UNITY_ANDROID || UNITY_IPHONE
        skincost = id;
#endif
    }

    public void equipskin(int id)
    {
#if UNITY_ANDROID || UNITY_IPHONE
        if (!(PlayerPrefs.GetInt("coins") >= skincost))
            return;
#endif
        PlayerPrefs.SetInt("skin", id);
        PlayerPrefs.Save();

        persistence.resetskin();
    }

    

    

	
	public void resetscore()
	{
		PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("coins", 0);
#if UNITY_ANDROID || UNITY_IPHONE
        cointext.text = "Coins: " + PlayerPrefs.GetInt("coins");
#endif
        exit();
	}
	public void exitgame()
	{
		Application.Quit ();
	}
	public void play()
	{

		SceneManager.LoadScene (PlayerPrefs.GetInt ("level"));
	}
}
