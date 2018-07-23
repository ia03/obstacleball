using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour {


    public GameObject player;
    Renderer rend;
    public GameObject canvas;

    System.Random random = new System.Random();
    int randomNumber = 0;

    public void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }

    public void resetskin()
    {
        rend = player.GetComponent<Renderer>();
        switch (PlayerPrefs.GetInt("skin"))
        {
            case 0:
                rend.material = (Material)Resources.Load("Green", typeof(Material));
                break;
            case 1:
                rend.material = (Material)Resources.Load("Red", typeof(Material));
                break;
            case 2:
                rend.material = (Material)Resources.Load("Blue", typeof(Material));
                break;
            case 3:
                rend.material = (Material)Resources.Load("Purple", typeof(Material));
                break;
            case 4:
                rend.material = (Material)Resources.Load("Black", typeof(Material));
                break;
            case 5:
                rend.material = (Material)Resources.Load("Fabulous", typeof(Material));
                break;
            case 6:
                rend.material = (Material)Resources.Load("Gold", typeof(Material));
                break;
            case 7:
                rend.material = (Material)Resources.Load("Red Line", typeof(Material));
                break;
        }
    }

    public void resetcolor()
    {
        randomNumber = random.Next(1, 4);
        switch (randomNumber)
        {
            case 1:
                GameObject.FindWithTag("rtrigger").GetComponent<Renderer>().sharedMaterial.color = new Color(1.0f, 0.843f, 0.0f);
                break;
            case 2:
                GameObject.FindWithTag("rtrigger").GetComponent<Renderer>().sharedMaterial.color = new Color(0.0f, 1.0f, 0.0f);
                break;
            case 3:
                GameObject.FindWithTag("rtrigger").GetComponent<Renderer>().sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f);
                break;
            case 4:
                GameObject.FindWithTag("rtrigger").GetComponent<Renderer>().sharedMaterial.color = new Color(0.823f, 0.411f, 0.117f);
                break;
        }


    }

}
