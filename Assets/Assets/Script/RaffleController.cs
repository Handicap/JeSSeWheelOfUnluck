using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaffleController : MonoBehaviour {
    
    public string[] rownames;
    public int[] rowseats;

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


    // use internet protocol -like strings to express the phase
    // Valid options:
    // STARTUP -> the scene is being initialized (animations etc)
    // STANDBY1  -> the first spinning process is ready
    // STANDBY2  -> the second spinning process is ready
    // FINISHED  -> the raffle is done

    private const string STARTUP = "STARTUP";
    private const string STANDBY1 = "STANDBY1";
    private const string STANDBY2 = "STANDBY2";
    private const string FINISHED = "FINISHED";

    public string rafflingphase = "STARTUP";
    public float startup_seconds = 5f;



	// Use this for initialization
	void Start () {
        
        //list
        for (int i = 0; i < rownames.Length; i++)
        {
            containerlist.Add(new RowSeatContainer(rownames[i], rowseats[i]));
            Debug.Log("rowseat: " + containerlist[i]);
        }

        StartCoroutine(StartupCountdown());
        
    
    }

    public void LoadOptions(OptionPanelController input)
    {
        rowseats = input.optionrowseats;
        rownames = input.optionrownames;
    }

    private IEnumerator StartupCountdown()
    {
        yield return new WaitForSeconds(startup_seconds);
        rafflingphase = STANDBY1;
    }

    /// <summary>
    /// Use this method to start the raffling process.
    /// The method checks which part of the raffle is going on and automatically figures out the rest.
    /// </summary>
    public void StartRaffle()
    {
        if (rafflingphase.Equals(STANDBY1))
        {
            spinneri.StartSpinner(spinningtime);
            StartCoroutine(RaffleRows());
            spinbtn.interactable = false;
        }
        else if (rafflingphase.Equals(STANDBY2))
        {
            spinneri.StartSpinner(spinningtime);
            StartCoroutine(RaffleSeats());
            spinbtn.interactable = false;
        }
        else Debug.Log("The raffle is already done: " + rafflingphase);
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
        rafflingphase = STANDBY2;

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
        spinbtn.interactable = false;
        rafflingphase = FINISHED;

        yield return new WaitForEndOfFrame();
    }


    //These are the setters for the rafflingphase string, public use onleeh!
    #region statechanges

    public void SetStartup()
    {
        rafflingphase = STARTUP;
    }

    public void SetStandby1()
    {
        rafflingphase = STANDBY1;
    }

    public void SetStandby2()
    {
        rafflingphase = STANDBY2;
    }

    public void SetFinished()
    {
        rafflingphase = FINISHED;
    }

    #endregion

}
