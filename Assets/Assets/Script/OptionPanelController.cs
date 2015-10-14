using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour {

    private List<RowSeatContainer> containerlist = new List<RowSeatContainer>();
    public GameObject rowprototype;
    public List<GameObject> inputfields;
    public string[] defaultrownames;
    public int[] defaultrowseats;

    public GameObject listingstart;
    public int list_count;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < defaultrownames.Length; i++)
        {
            containerlist.Add(new RowSeatContainer(defaultrownames[i], defaultrowseats[i]));
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
        GameObject newrow = Instantiate(rowprototype);

        newrow.transform.rotation = Quaternion.identity;
        newrow.transform.localScale = Vector3.one;

        Vector3 rowpos = listingstart.transform.localPosition;
        float offset = ((this.inputfields.Count) * -30);
        Debug.Log("row offset: " + offset);
        rowpos.y = rowpos.y + offset;
        newrow.transform.position = rowpos;

        newrow.transform.SetParent(transform, false);

        newrow.GetComponent<InputField>().text = containerlist[containerlist.Count - 1].rowseats.ToString();        
        this.inputfields.Add(newrow);

        newrow.name = "inputrow " + this.inputfields.Count;
        
    }

    public void RemoveRow()
    {
        if (this.inputfields.Count <= 1) return;
        // remove the row from list
        // remove the row instance
        GameObject target = this.inputfields[this.inputfields.Count - 1];
        this.inputfields.Remove(target);
        Debug.Log("Removed: " + target);
        Destroy(target);
    }
}
