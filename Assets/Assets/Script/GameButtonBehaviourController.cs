using UnityEngine;
using System.Collections;

public class GameButtonBehaviourController : MonoBehaviour {

    public RaffleController raffler;

	// Use this for initialization
	void Start () {
        raffler = GameObject.FindGameObjectWithTag("GameController").GetComponent<RaffleController>();
	}

    public void StartRaffling()
    {
        raffler.StartRaffle();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
