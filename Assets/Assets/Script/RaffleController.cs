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
    public float localphase = 0f;
    public float localmax;

    public int winningrow;
    public int winningseat;
	// Use this for initialization
	void Start () {
        
        //list
        for (int i = 0; i < defaultrownames.Length; i++)
        {
            containerlist.Add(new RowSeatContainer(defaultrownames[i], defaultrowseats[i]));
            Debug.Log("rowseat: " + containerlist[i]);
        }

        
    
    }

    /// <summary>
    /// Use this method to start the raffling process.
    /// The method checks which part of the raffle is going on and automatically figures out the rest.
    /// </summary>
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
            StartCoroutine(RaffleSeats());
        }
    }

    /// <summary>
    /// Randomize the row (use the list to get the different rows)
    /// </summary>
    /// <returns>This is a coroutine</returns>
    private IEnumerator RaffleRows()
    {
        //float localphase = 0f;
        localmax = 1f / (containerlist.Count*30);
        Debug.Log("entered coroutine - rafflerows");
        //float phase = 0f;
        Debug.Log("localmax " + localmax);
        while (phase < 1f)
        {
            if (localphase > localmax)
            {
                winningrow = Random.Range(0, containerlist.Count);
                rowtxt.text = containerlist[winningrow].rowname;
                localphase = 0f;
            }

            localmax = localmax + ((localmax * Time.deltaTime)/2);
            phase = phase + (Time.deltaTime / spinningtime);
            localphase = localphase + (Time.deltaTime / spinningtime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("exiting coroutine - rafflerows");
        phase = 0f;
        localphase = 0f;
        spinbtn.interactable = true;
        rowsdone = true;

        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Randomize the seats. The row is determined by the RaffleRows method.
    /// </summary>
    /// <returns>Coroutine</returns>
    private IEnumerator RaffleSeats()
    {
        //float localphase = 0f;
        localmax = 1f / ((containerlist[winningrow].rowseats + 1) * 3);
        Debug.Log("entered coroutine - raffleseats");
        //float phase = 0f;
        Debug.Log("localmax " + localmax);
        while (phase < 1f)
        {
            if (localphase > localmax)
            {
                winningseat = Random.Range(1, containerlist[winningrow].rowseats + 1);
                seattxt.text = winningseat.ToString();
                localphase = 0f;
            }

            localmax = localmax + ((localmax * Time.deltaTime) / 2);
            phase = phase + (Time.deltaTime / spinningtime);
            localphase = localphase + (Time.deltaTime / spinningtime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("exiting coroutine - raffleseats");
        phase = 0f;
        localphase = 0f;
        spinbtn.interactable = true;

        yield return new WaitForEndOfFrame();
    }

    
}
