/*
 * Programmer:      labthe3rd
 * Date:            08/25/23
 * Desc:            A script that teleports a player when they walk into the trigger
 * 
 */
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace labthe3rd
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TriggerTeleport : UdonSharpBehaviour
    {
        [Header("Teleport Location")]
        [SerializeField] private Transform teleport;

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                if(teleport != null)
                {
                    Networking.LocalPlayer.TeleportTo(teleport.position, teleport.rotation);
                }
                else
                {
                    Debug.Log("No teleport position detect");
                }
            }
        }

    }

}
