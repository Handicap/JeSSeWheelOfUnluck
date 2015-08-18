using UnityEngine;
using System.Collections;

public class Game_logic_controller : MonoBehaviour {

    // build settingseistä saa tasojen indeksit selville
    // tasotaulukon logiikka:
    // oliossa on levelin nimi, ja bool siitä onko pelattu jo
    public int fade = 0;
        //[Range(-1,1)]
        [Range(0f, 1f)]
        public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    
    private static Game_logic_controller instance = null;
    public static Game_logic_controller Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        // singletonihomma
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //Time.fixedDeltaTime ehkä pitää implementoida jos fysiikat sekoilee
        fade = -1;
        // Set the texture so that it is the the size of the screen and covers it.
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        GetComponent<GUITexture>().enabled = true;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPE");
            if (Application.loadedLevel == 0) Application.Quit();
            else Application.LoadLevel("main_menu");

        }

        if (fade == -1) GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
        if (fade == 1) GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);

        if (GetComponent<GUITexture>().color.a <= 0.05f && fade == -1)
        {
            // ... set the colour to clear and disable the GUITexture.
            fade = 0;
        }
        if (GetComponent<GUITexture>().color.a >= 0.5f && fade == 1)
        {
            // ... reload the level.
            GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 1);
            fade = -1;
        }
    }

}
