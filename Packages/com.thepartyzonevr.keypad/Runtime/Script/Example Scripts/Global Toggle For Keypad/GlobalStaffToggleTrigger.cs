/*
 * Programmer:      labthe3rd
 * Date:            10/01/22
 * Description:     Connect this to the trigger and it will interact with the active script
 * 
 * 
 */


using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class GlobalStaffToggleTrigger : UdonSharpBehaviour
{
    [Header("Put the always active Global Toggle script here")]
    public GlobalStaffToggle targetScript;
    [Space]
    [Header("Enable debug mode to get log messages")]
    public bool debugMode;

    public override void Interact()
    {
        if (targetScript != null)
        {
            targetScript.ToggleObject();
            DebugMessage("Sending ToggleObject Event");
        }
        else
        {
            DebugMessage("Target script is null, this script will not work without it set to a ToggleObject script");
        }
    }

    private void DebugMessage(string message)
    {
        if (debugMode)
        {
            Debug.Log(gameObject.name + "Debug Message: " + message);
        }
    }
}
