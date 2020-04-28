using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFeatures : MonoBehaviour
{   

    public bool alwaysShownMenus = true;
    public bool baubles = true;
    public bool allowFlight = false;
    public bool allowSlomo = false;
    public bool sitting = false;

    [Header("Tools")]
    public bool deleteTool = true;
    public bool weldTool = true;
    public bool thrusterTool = true;
    public bool wheelTool = true;
    public bool duplicatorTool = true;
    public bool hingeTool = true;
    public bool gravityTool = true;
    public bool balloonTool = true;

    private CharController thePlayer;
    private Weapons weaponStates;
    private ToggleUI uiStates;
    // Start is called before the first frame update
    void Start()
    {
        weaponStates = GameObject.Find("Player").GetComponent<Weapons>();
        thePlayer = GameObject.Find("Player").GetComponent<CharController>();
        uiStates = GameObject.Find("Canvas").GetComponent<ToggleUI>();
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            weaponStates.deleteTool = deleteTool;
            weaponStates.weldTool = weldTool;
            weaponStates.thrusterTool = thrusterTool;
            weaponStates.wheelTool = wheelTool;
            weaponStates.duplicatorTool = duplicatorTool;
            weaponStates.hingeTool = hingeTool;
            weaponStates.gravityTool = gravityTool;
            weaponStates.balloonTool = balloonTool;

            uiStates.AlwaysShownbool = alwaysShownMenus;
            uiStates.baublesbool = baubles;

            thePlayer.allowFlight = allowFlight;
            thePlayer.allowSlomo = allowSlomo;
            thePlayer.sitting = sitting;

            weaponStates.SetTextAliases();
            weaponStates.CheckWeapon();
            weaponStates.SetWeaponStatus();
            uiStates.changeStates();
        }
    }
}
