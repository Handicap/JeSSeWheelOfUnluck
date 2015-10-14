using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour {

    private List<RowSeatContainer> containerlist = new List<RowSeatContainer>();
    public GameObject rowseatsprototype;
    public GameObject rownameprototype;
    public List<GameObject> inputfields;
    public List<GameObject> inputnames;
    public string[] optionrownames;
    public int[] optionrowseats;

    public GameObject listingstart;
    public GameObject namingstart;
    public int list_count;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < optionrownames.Length; i++)
        {
            containerlist.Add(new RowSeatContainer(optionrownames[i], optionrowseats[i]));
            Debug.Log("Options: rowseat: " + containerlist[i]);
        }

        for (int i = 0; i < containerlist.Count; i++)
        {
            AddRow();
        }
	}
	
	// Update is called once per frame
	void Update () {
        list_count = this.inputfields.Count;
	}

    public void UpdateRow()
    {

    }

    public void AddRow(){

        CreateNewRowSeatInput();
        CreateNewRowNameInput();        
    }

    private void CreateNewRowSeatInput()
    {
        // seating stuff graphics:
        GameObject newrow = Instantiate(rowseatsprototype);

        newrow.transform.rotation = Quaternion.identity;
        newrow.transform.localScale = Vector3.one;

        Vector3 rowpos = listingstart.transform.localPosition;
        float offset = ((this.inputfields.Count) * -30);
        //Debug.Log("row offset: " + offset);
        rowpos.y = rowpos.y + offset;
        newrow.transform.position = rowpos;

        newrow.transform.SetParent(transform, false);

        if (inputfields.Count < containerlist.Count) newrow.GetComponent<InputField>().text = containerlist[inputfields.Count].rowseats.ToString();
        else
        {
            newrow.GetComponent<InputField>().text = containerlist[containerlist.Count - 1].rowseats.ToString();
            RowSeatContainer newcont = new RowSeatContainer("new", 0);
            containerlist.Add(newcont);
            
        }
        this.inputfields.Add(newrow);

        newrow.name = "inputrow " + this.inputfields.Count;

        // seating stuff data:

    }

    private void CreateNewRowNameInput()
    {
        // naming stuff:
        GameObject newrowname = Instantiate(rownameprototype);

        newrowname.transform.rotation = Quaternion.identity;
        newrowname.transform.localScale = Vector3.one;

        Vector3 rowpos = namingstart.transform.localPosition;
        float offset = ((this.inputnames.Count) * -30);
        //Debug.Log("inputnames offset: " + offset);
        rowpos.y = rowpos.y + offset;
        newrowname.transform.position = rowpos;

        newrowname.transform.SetParent(transform, false);

        if (inputnames.Count < containerlist.Count) newrowname.GetComponent<InputField>().text = containerlist[inputnames.Count].rowname.ToString();
        else newrowname.GetComponent<InputField>().text = containerlist[containerlist.Count - 1].rowname.ToString();
        this.inputnames.Add(newrowname);

        newrowname.name = "inputname " + this.inputnames.Count;
    }

    public void RemoveRow()
    {
        if (this.inputfields.Count <= 1 || this.inputnames.Count <= 1) return;
        // remove the row from list
        // remove the row instance
        GameObject target = this.inputfields[this.inputfields.Count - 1];
        this.inputfields.Remove(target);
        Debug.Log("Removed number: " + target);
        Destroy(target);

        // same for the naming field:
        GameObject nametarget = this.inputnames[this.inputnames.Count - 1];
        this.inputnames.Remove(nametarget);
        Debug.Log("Removed name: " + nametarget);
        Destroy(nametarget);

        // remove container from list
        Debug.Log("Removing container: " + containerlist[containerlist.Count - 1]);
        containerlist.RemoveAt(containerlist.Count - 1);
        
    }
}
