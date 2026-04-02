using UnityEngine;
using UnityEngine.InputSystem;


public class FighterController : MonoBehaviour
{
    // public variables
    public int playerNumber = 1;

    // object indentification
    private Rigidbody rigi;
    private IA_PlayerInputs ctrl;

    // inputs
    private Vector2 moveInput;


    void Awake()
    {
        // rigi
        rigi = GetComponent<Rigidbody>();
        ctrl = new PlayerInputs();
        
        // input control map
        ctrl.Enable();        
    }


    void OnDisabled()
    {
        ctrl.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        //moveInput = ctrl.Player
    }
}
