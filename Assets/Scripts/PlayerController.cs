using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text txt;
	public Text lvltxt;
    Text cointext;
    public GameObject coinpanel;
    public GameObject canvas;
    public Persistence persistence;
    


    


#if UNITY_ANDROID || UNITY_IPHONE
    AdManager admngr;
#endif
    private bool won;
	private Rigidbody rb;
	private Vector3 movement = new Vector3(0, 0, 0);



	// Use this for initialization
	void Start () {

        persistence = GameObject.FindWithTag("persistence").GetComponent<Persistence>();
        persistence.player = gameObject;
        canvas = persistence.canvas;
        canvas.SetActive(true);
        coinpanel = canvas.transform.Find("Extra Coins-Panel").gameObject;
        coinpanel.SetActive(false);

        
        initializebuttons();

        persistence.resetcolor();

        persistence.resetskin();

        txt = GameObject.FindWithTag("txt").GetComponent<Text>();
        lvltxt = GameObject.FindWithTag("lvltxt").GetComponent<Text>();
        cointext = GameObject.FindWithTag("cointext").GetComponent<Text>();

        

        
#if UNITY_ANDROID || UNITY_IPHONE
        admngr = GetComponent<AdManager>();
        admngr.coinpanel = coinpanel;
#endif

        won = false;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		rb = GetComponent<Rigidbody>();
		lvltxt.text = "Level " + SceneManager.GetActiveScene ().buildIndex;
#if UNITY_ANDROID || UNITY_IPHONE
        cointext.text = "Coins: " + PlayerPrefs.GetInt("coins");
#endif

        
    }

    

    public void reloadcointext()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        cointext.text = "Coins: " + PlayerPrefs.GetInt("coins");
#endif
    }

    

    void initializebuttons()
    {
        
        Button restartbutton = GameObject.FindWithTag("restartbutton").GetComponent<Button>();
        restartbutton.onClick.AddListener(gameObject.GetComponent<restart>().restartscene);

        Button mainmenubutton = GameObject.FindWithTag("mainmenubutton").GetComponent<Button>();
        mainmenubutton.onClick.AddListener(gameObject.GetComponent<restart>().mainmenu);

#if UNITY_ANDROID || UNITY_IPHONE
        Button yesadbutton = coinpanel.transform.Find("Ad Button").GetComponent<Button>();
        yesadbutton.onClick.AddListener(gameObject.GetComponent<AdManager>().coinvideo);

        Button noadbutton = coinpanel.transform.Find("No Button").GetComponent<Button>();
        noadbutton.onClick.AddListener(gameObject.GetComponent<AdManager>().novideo);

#endif
    }

    


	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID || UNITY_IPHONE
        // Exit condition for mobile devices
        if (Input.GetKeyDown(KeyCode.Escape))
            GetComponent<restart>().mainmenu();
#else
		// Exit condition for Desktop devices
		if (Input.GetKey("escape"))
			SceneManager.LoadScene("Main Menu");   
        if (Input.GetKeyDown(KeyCode.R))
            GetComponent<restart>().restartscene();
			        
#endif



    }


    void FixedUpdate()
	{
		
#if UNITY_ANDROID || UNITY_IPHONE

		// Player movement in mobile devices
		// Building of force vector 
		movement = new Vector3 (-Input.acceleration.y, 0.0f, Input.acceleration.x);
		// Adding force to rigidbody
		rb.AddForce(movement * speed * 2.5f * Time.deltaTime);

#else
		// Player movement in desktop devices
		// Definition of force vector X and Y components
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		// Building of force vector
		movement = new Vector3 (-moveVertical,0.0f,moveHorizontal);
		// Adding force to rigidbody
		rb.AddForce(movement * speed * Time.deltaTime);
#endif




	}

    

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("wall") && !won) {
            die ();
		} else if (other.CompareTag ("gwall")) {
			if (SceneManager.GetActiveScene ().buildIndex == SceneManager.sceneCountInBuildSettings - 1) { //if at the last level
				StartCoroutine (win ());
			} else {
#if UNITY_ANDROID || UNITY_IPHONE
                if((SceneManager.GetActiveScene ().buildIndex % 2) == 0)
                {
                    Time.timeScale = 0;
                    coinpanel.SetActive(true);

                    return;
                }

#endif
                PlayerPrefs.SetInt ("level", SceneManager.GetActiveScene ().buildIndex + 1);
				PlayerPrefs.Save ();
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1); // if not, move to the next level
			}
		} else if (other.CompareTag ("rtrigger") && !won) {
			die ();
		}
	}

	private void die()
	{
		
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		
	}

    


	IEnumerator win()
	{
		won = true;
		//DontDestroyOnLoad (gameObject);
		//Destroy (gameObject, 2);
		PlayerPrefs.SetInt ("level", 1);
		PlayerPrefs.Save ();
		txt.text = "You win!";
		yield return new WaitForSeconds (1.5f);

        GetComponent<restart>().mainmenu();
	}


}
