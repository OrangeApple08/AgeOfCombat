using UnityEngine;
using UnityEngine.InputSystem;


public class FighterController : MonoBehaviour
{
    // public variables
    public int playerNumber = 1;

    // object indentification
    private Rigidbody rigi;
    // private IA_PlayerInputs ctrl;

    // inputs
    private Vector2 moveInput;

    public float speed;
    public float jump;
    private float Move;


    void Awake()
    {
        // rigi
        rigi = GetComponent<Rigidbody>();
        // ctrl = new IA_PlayerInputs();
        
        // input control map
        // ctrl.Enable();        
    }


    void OnDisabled()
    {
        // ctrl.Disable();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // get player input
        // change player x
        // jump if up pressed
        // 
        // check if special buttons are pressed
        // attack if so

        // x movement
        Move = Input.GetAxis("Horizontal");
        rigi.linearVelocity = new Vector2(speed * Move * Time.deltaTime, rigi.linearVelocity.y); 
    }
}
