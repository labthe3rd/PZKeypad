
/*
 * Programmer:      labthe3rd
 * Rev:             1.5
 * Date:            08/25/23
 * Desc:            Added teleport controls to keypad AND the header for my code
 */

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;

namespace Labthe3rd.Keypad.Keypad_Main
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Keypad_Main : UdonSharpBehaviour
    {
        [Header("Passwords")]
        public string adminPassword;
        public string staffPassword;
        public string dJPassword;
        public string vIPPassword;

        [Header("Set the objects you want to show and hide for each position")]
        [Header("Admin Show And Hide Objects")]
        public GameObject[] adminShowObjects;
        public GameObject[] adminHideObjects;
        [Header("Staff Show And Hide Objects")]
        public GameObject[] staffShowObjects;
        public GameObject[] staffHideObjects;
        [Header("DJ Show And Hide Objects")]
        public GameObject[] dJShowObjects;
        public GameObject[] dJHideObjects;
        [Header("VIP Show And Hide Objects")]
        public GameObject[] vIPShowObjects;
        public GameObject[] vIPHideObjects;

        //Rev 1.5 added teleport
        [Header("Teleport Controls")]
        [SerializeField] private bool enableTeleport = false;
        [SerializeField] private Transform adminTeleport;
        [SerializeField] private Transform staffTeleport;
        [SerializeField] private Transform djTeleport;
        [SerializeField] private Transform vipTeleport;
        [Space]

        [Header("Internal UI Stuff")]
        public TextMeshProUGUI InputScreen;

        private VRCPlayerApi player;
        private string inputString;
        private bool loggedIn = false;

        [HideInInspector] public string Key;

        public void KeyPressed()
        {
            if (loggedIn == false)
            {
                inputString = string.Concat(inputString, Key);
                InputScreen.text = inputString;
            }


        }

        public void Enter()
        {
            if (loggedIn == false)
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    if (inputString == adminPassword)
                    {

                        AdminObjects();
                        StaffObjects();
                        DJObjects();
                        VIPObjects();

                        loggedIn = true;
                        Networking.LocalPlayer.SetPlayerTag("Position", "Admin");
                        InputScreen.text = "ADMIN LOGGED IN";
                        inputString = "";

                        //Rev 1.5 teleport logic
                        if (enableTeleport)
                        {
                            Debug.Log("Teleporting player to " + adminTeleport);
                            Networking.LocalPlayer.TeleportTo(adminTeleport.position, adminTeleport.rotation);
                        }
                    }

                    else if (inputString == staffPassword)
                    {
                        StaffObjects();
                        DJObjects();
                        VIPObjects();
                        loggedIn = true;
                        Networking.LocalPlayer.SetPlayerTag("Position", "Staff");
                        InputScreen.text = "STAFF LOGGED IN";
                        inputString = "";

                        //Rev 1.5 teleport logic
                        if (enableTeleport)
                        {
                            Debug.Log("Teleporting player to " + staffTeleport);
                            Networking.LocalPlayer.TeleportTo(staffTeleport.position, staffTeleport.rotation);
                        }
                    }

                    else if (inputString == dJPassword)
                    {
                        DJObjects();
                        VIPObjects();
                        loggedIn = true;
                        Networking.LocalPlayer.SetPlayerTag("Position", "DJ");
                        InputScreen.text = "DJ LOGGED IN";
                        inputString = "";

                        //Rev 1.5 teleport logic
                        if (enableTeleport)
                        {
                            Debug.Log("Teleporting player to " + djTeleport);
                            Networking.LocalPlayer.TeleportTo(djTeleport.position, djTeleport.rotation);
                        }

                    }

                    else if (inputString == vIPPassword)
                    {
                        VIPObjects();
                        loggedIn = true;
                        Networking.LocalPlayer.SetPlayerTag("Position", "VIP");
                        InputScreen.text = "VIP LOGGED IN";
                        inputString = "";

                        //Rev 1.5 teleport logic
                        if (enableTeleport)
                        {
                            Debug.Log("Teleporting player to " + vipTeleport);
                            Networking.LocalPlayer.TeleportTo(vipTeleport.position, vipTeleport.rotation);
                        }
                    }

                    else
                    {
                        InputScreen.text = "INVALID";
                        inputString = "";
                    }
                }
            }

        }

        public void AdminLogin()
        {

            if (loggedIn == false)
            {

                AdminObjects();
                StaffObjects();
                DJObjects();
                VIPObjects();
                loggedIn = true;
                InputScreen.text = "ADMIN LOGGED IN";
            }


        }

        public void StaffLogin()
        {

            if (loggedIn == false)
            {
                StaffObjects();
                DJObjects();
                VIPObjects();
                loggedIn = true;
                InputScreen.text = "STAFF LOGGED IN";
            }


        }

        public void DJLogin()
        {

            if (loggedIn == false)
            {
                DJObjects();
                VIPObjects();
                loggedIn = true;
                InputScreen.text = "dJ LOGGED IN";
            }


        }

        public void VIPLogin()
        {

            if (loggedIn == false)
            {
                VIPObjects();
                loggedIn = true;
                InputScreen.text = "VIP LOGGED IN";
            }


        }


        public void Clear()
        {

            inputString = "";
            InputScreen.text = inputString;
            loggedIn = false;


        }

        private void AdminObjects()
        {
            Debug.Log("Correct Admin Password Entered");
            if (adminShowObjects.Length > 0)
            {
                for (int i = 0; i < adminShowObjects.Length; i++)
                {
                    if (Utilities.IsValid(adminShowObjects[i]))
                    {
                        adminShowObjects[i].SetActive(true);
                    }
                }
            }
            if (adminHideObjects.Length > 0)
            {
                for (int i = 0; i < adminHideObjects.Length; i++)
                {
                    if (Utilities.IsValid(adminHideObjects[i]))
                    {
                        adminHideObjects[i].SetActive(false);
                    }
                }
            }
        }

        private void StaffObjects()
        {
            Debug.Log("Correct Staff Password Entered");
            if (staffShowObjects.Length > 0)
            {
                for (int i = 0; i < staffShowObjects.Length; i++)
                {
                    if (Utilities.IsValid(staffShowObjects[i]))
                    {
                        staffShowObjects[i].SetActive(true);
                    }
                }
            }
            if (staffHideObjects.Length > 0)
            {
                for (int i = 0; i < staffHideObjects.Length; i++)
                {
                    if (Utilities.IsValid(staffHideObjects[i]))
                    {
                        staffHideObjects[i].SetActive(false);
                    }
                }
            }
        }

        private void DJObjects()
        {
            Debug.Log("Correct DJ Password Entered");
            if (dJShowObjects.Length > 0)
            {
                for (int i = 0; i < dJShowObjects.Length; i++)
                {
                    if (Utilities.IsValid(dJShowObjects[i]))
                    {
                        dJShowObjects[i].SetActive(true);
                    }
                }
            }
            if (dJHideObjects.Length > 0)
            {
                for (int i = 0; i < dJHideObjects.Length; i++)
                {
                    if (Utilities.IsValid(dJHideObjects[i]))
                    {
                        dJHideObjects[i].SetActive(false);
                    }
                }
            }
        }

        private void VIPObjects()
        {
            Debug.Log("Correct VIP Password Entered");
            if (vIPShowObjects.Length > 0)
            {
                for (int i = 0; i < vIPShowObjects.Length; i++)
                {
                    if (Utilities.IsValid(vIPShowObjects[i]))
                    {
                        vIPShowObjects[i].SetActive(true);
                    }
                }
            }
            if (vIPHideObjects.Length > 0)
            {
                for (int i = 0; i < vIPHideObjects.Length; i++)
                {
                    if (Utilities.IsValid(vIPHideObjects[i]))
                    {
                        vIPHideObjects[i].SetActive(false);
                    }
                }
            }
        }




    }
}

