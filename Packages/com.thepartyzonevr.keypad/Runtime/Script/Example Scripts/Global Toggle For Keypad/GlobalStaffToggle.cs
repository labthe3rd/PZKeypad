
/*
 * Programmer:      labthe3rd
 * Date:            10/01/22
 * Description:     Script that is used to sync up global toggles. The game object needs to always be active
 */

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class GlobalStaffToggle : UdonSharpBehaviour
{
    [Header("THIS SCRIPT NEEDS TO BE ON AN ACTIVE GAMEOBJECT ALWAYS!!!")]
    public GameObject targetObject;
    [Space]
    [Header("Enable debug mode to get info in Log")]
    public bool debugMode;
    
    [UdonSynced,FieldChangeCallback(nameof(ToggleState))] private bool _toggleState;

    void Start()
    {
        //Have the owner set the default value of toggleState
        if (Utilities.IsValid(Networking.LocalPlayer))
        {
            if (Networking.IsOwner(gameObject))
            {
                DebugMessage(Networking.LocalPlayer.displayName + " Is the object owner, attempt to set toggleState");
                if(targetObject != null)
                {
                    ToggleState = targetObject.activeSelf;
                    DebugMessage("Setting toggleState to " + targetObject.activeSelf);
                    RequestSerialization();
                }
                else
                {
                    DebugMessage("Target Object is null, this script cannot run");
                }
            }
            else
            {
                DebugMessage(Networking.LocalPlayer.displayName + " is not the owner, do not set toggleState default Value");
            }
        }
        else
        {
            DebugMessage("Local Player Is Invalid");
        }
    }

    private void DelayedRetry()
    {
        //Wait 1 second and try again to wait for network to settle
        SendCustomEventDelayedSeconds("ToggleObject", 1.0f);
    }

    public void ToggleObject()
    {
        if (Utilities.IsValid(Networking.LocalPlayer))
        {
            //Make sure network is ready to take changes to prevent anything wonky from happening
            if(Networking.IsNetworkSettled && !Networking.IsClogged)
            {
                //Request ownership
                if (!Networking.IsOwner(gameObject))
                {
                    DebugMessage("Setting Owner To " + Networking.LocalPlayer.displayName);
                    Networking.SetOwner(Networking.LocalPlayer, gameObject);
                }

                //Make sure ownership request worked
                if (Networking.IsOwner(gameObject))
                {
                    //Set to opposite state
                    ToggleState = !ToggleState;
                    RequestSerialization();
                }
                else
                {
                    DebugMessage("Ownership request failed");
                }
            }

            else
            {
                //Send debug of Network failure
                if (!Networking.IsNetworkSettled)
                {
                    DebugMessage("Network is not settled, retry in 1 second");
                    //Now send retry event
                    DelayedRetry();
                }

                if (Networking.IsClogged)
                {
                    DebugMessage("Network is clogged, retry in 1 second");
                    //Now send retry event
                    DelayedRetry();
                }

            }

        }
        else
        {
            DebugMessage("Local Player returned invalid. Cannot run script");
        }

    }

    private void ChangeObjectState()
    {
        if (Utilities.IsValid(targetObject))
        {
            targetObject.SetActive(_toggleState);
        }
        else
        {
            DebugMessage("Target Object is invalid. This must be set to use this script.");
        }
    }

    private void DebugMessage(string message)
    {
        if (debugMode)
        {
            Debug.Log(gameObject.name + " Debug Message: " + message);
        }
    }


    public bool ToggleState
    {
        set
        {
            DebugMessage("Setting toggleState to " + value);
            _toggleState = value;
            ChangeObjectState();
        }

        get => _toggleState;
    }
}
