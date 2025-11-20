using TMPro;
using UnityEngine;

public class TestRayScript : MonoBehaviour
{
    public Camera playerCam; //Refernces the player camera where the raycast comes out of. 
    public GameObject displayObj;//References the gameobject where the text resides. 
    public TMP_Text itemText; //References the text component so you can change the item text to whatever you want. 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //If the left mouse button is clicked
        {
            Ray Screenray = playerCam.ScreenPointToRay(Input.mousePosition); //Creates a ray that comes out of the camera at the mouse position
            RaycastHit itemHit; //Creates a variable to store information about what the raycast hits
            if (Physics.Raycast(Screenray, out itemHit)) //If the raycast hits something within 100 units
            {
                print("You have clicked on " + itemHit.collider.gameObject.name);
            }
        }
    }
}
