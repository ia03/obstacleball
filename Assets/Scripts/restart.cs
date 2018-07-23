using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class restart : MonoBehaviour {


	public void restartscene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void mainmenu()
	{
        Destroy(gameObject.GetComponent<PlayerController>().canvas);
        Destroy(GameObject.FindWithTag("persistence"));
		SceneManager.LoadScene (0);
	}
}
