
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;

namespace Labthe3rd.Keypad.TagDistributor
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class TagDistribution : UdonSharpBehaviour
    {
        [Header("Write Admin Display Names Here")]
        public string[] Admin;
        [Header("Write Staff Display Names Here")]
        public string[] Staff;
        [Header("Write DJ Display Names Here")]
        public string[] DJ;
        [Header("Write VIP Display Names Here")]
        public string[] VIP;
        [Space]
        [Header("The Keypad Game Object")]
        public UdonBehaviour KeyPad;

        private VRCPlayerApi targetPlayer;
        private string displayName;

        public void Start()
        {
            Debug.Log("Setting Tags");
            //Make sure local player is valid
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                //Get variables since GET is expensive
                targetPlayer = Networking.LocalPlayer;
                displayName = targetPlayer.displayName;

                //Loop through admin names then send staff auto login to keypad
                for (int i = 0; i < Admin.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Admin[i]))
                    {
                        if (Admin[i] == displayName)
                        {
                            targetPlayer.SetPlayerTag("Position", "Admin");
                            KeyPad.SendCustomEvent("AdminLogin");
                            Debug.Log(targetPlayer.displayName + " has been given the tag " + "Admin");
                        }
                    }
                }

                //Loop through staff names then send staff auto login to keypad
                for (int i = 0; i < Staff.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Staff[i]))
                    {
                        if (Staff[i] == displayName)
                        {
                            targetPlayer.SetPlayerTag("Position", "Staff");
                            KeyPad.SendCustomEvent("StaffLogin");
                            Debug.Log(targetPlayer.displayName + " has been given the tag " + "Staff");
                        }
                    }
                }

                //Loop through DJ names then send DJ auto login to keypad
                for (int i = 0; i < DJ.Length; i++)
                {
                    if (!string.IsNullOrEmpty(DJ[i]))
                    {
                        if (DJ[i] == displayName)
                        {
                            targetPlayer.SetPlayerTag("Position", "DJ");
                            KeyPad.SendCustomEvent("DJLogin");
                            Debug.Log(targetPlayer.displayName + " has been given the tag " + "DJ");
                        }
                    }
                }

                //Loop through VIP names then send VIP auto login to keypad
                for (int i = 0; i < VIP.Length; i++)
                {
                    if (!string.IsNullOrEmpty(VIP[i]))
                    {
                        if (VIP[i] == displayName)
                        {
                            targetPlayer.SetPlayerTag("Position", "VIP");
                            KeyPad.SendCustomEvent("VIPLogin");
                            Debug.Log(targetPlayer.displayName + " has been given the tag " + "VIP");
                        }
                    }
                }
            }


            else
            {
                Debug.Log("Local player invalid in tag distributor");
            }

        }
    }
}


