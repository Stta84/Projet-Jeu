using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using Unity.Cinemachine;

public class NetworkPlayer : NetworkBehaviour
{

    public PlayerController playerController;
    public CinemachineCamera playerCam;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        playerController.enabled = IsOwner;
        playerCam.Priority = IsOwner ? 10 : 0;
    }
}
