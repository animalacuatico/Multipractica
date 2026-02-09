using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class LanUI : MonoBehaviour
{
    [SerializeField] private string hostIP = "0. 0. 0. 0";
    [SerializeField] private string connectIp = "127.0.0.1";
    [SerializeField] private ushort port = 7777;

    public void StartHost()
    {
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.SetConnectionData(hostIP, port);
        NetworkManager.Singleton.StartHost();
        Debug.Log("HOST iniciado en puerto: " + port);
    }
    public void StartClient()
    {
        var transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.SetConnectionData(connectIp, port);
        NetworkManager.Singleton.StartClient();
        Debug.Log("CLIENT intentando conectar a " + connectIp + ":" + port);
    }
}
