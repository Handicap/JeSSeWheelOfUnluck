using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public string startupname;
    public string rafflename;

    public void LoadRafflegame()
    {
        OptionPanelController optioner = GameObject.Find("options_panel").GetComponent<OptionPanelController>();
        Debug.Log("found optioner: " + optioner.ToString());

        RaffleController raffler = gameObject.GetComponent<RaffleController>();
        Debug.Log("found rafflecontroller: " + optioner.ToString());

        raffler.LoadOptions(optioner);

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
