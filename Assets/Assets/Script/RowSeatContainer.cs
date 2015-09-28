public class RowSeatContainer {

    public int rowseats = 0;
    public string rowname = "Generic";


    public RowSeatContainer(string name, int seats)
    {
        rowseats = seats;
        rowname = name;
    }

    public override string ToString(){
        return "Row: " + rowname + " Seats: " + rowseats; 
    }
}
