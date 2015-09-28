using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaffleController : MonoBehaviour {

    public bool rowsdone = false;

    public string[] defaultrownames;
    public int[] defaultrowseats;

    public float spinningtime = 5f;

    private List<RowSeatContainer> containerlist = new List<RowSeatContainer>();
    
    public SpinnerLogic spinneri;
    public Button spinbtn;
    public Text rowtxt;
    public Text seattxt;

    public float phase = 0f;

	// Use this for initialization
	void Start () {
        
        //list
        for (int i = 0; i < defaultrownames.Length; i++)
        {
            containerlist.Add(new RowSeatContainer(defaultrownames[i], defaultrowseats[i]));
            Debug.Log("rowseat: " + containerlist[i]);
        }

        
    
    }


    public void StartRaffle()
    {
        spinbtn.interactable = false;
        if (!rowsdone)
        {
            spinneri.StartSpinner(spinningtime);
            StartCoroutine(RaffleRows());
        }
        else
        {
            spinneri.StartSpinner(spinningtime);
        }
    }

    private IEnumerator RaffleRows()
    {

        Debug.Log("entered coroutine - rafflerows");
        //float phase = 0f;
        while (phase < 1f)
        {
            rowtxt.text = containerlist[Random.Range(0, containerlist.Count)].rowname;
            phase = phase + (Time.deltaTime / spinningtime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("exiting coroutine - rafflerows");
        phase = 0f;
        spinbtn.interactable = true;

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator RaffleSeats()
    {

        yield return new WaitForEndOfFrame();
    }

    
}
