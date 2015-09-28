using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpinnerLogic : MonoBehaviour {

    public Vector3 rotation = new Vector3(0,-15,0);
    private float spinningtime = 5;
    private float phase = 0f;
    [Range(0f,1f)]
    public float max_random = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartSpinner(float spintime)
    {
        Debug.Log("started spinner");
        spinningtime = spintime;
        StartCoroutine(StartSpinning());
    }
    
    /// <summary>
    /// Start the spinning, should be called by a public method.
    /// </summary>
    /// <returns>Coroutine IEnumerator</returns>
    private IEnumerator StartSpinning()
    {
        Debug.Log("entered coroutine");
        //float phase = 0f;
        Vector3 coroutinerotation = rotation + (rotation * Random.Range(0f, max_random));
        while (phase < 1f)
        {
            transform.Rotate(coroutinerotation - coroutinerotation * phase);

            phase = phase + (Time.deltaTime / spinningtime);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("exiting coroutine");
        phase = 0f;
        yield return new WaitForEndOfFrame();
    }
}
