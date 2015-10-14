using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public string startupname;
    public string rafflename;

    public void LoadRafflegame()
    {
        Application.LoadLevel(rafflename);
    }

    public void LoadStartup()
    {
        Application.LoadLevel(startupname);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
