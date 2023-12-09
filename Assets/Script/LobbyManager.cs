using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/*
 ��.. �����ϴ°�
��ư�� ������ �� ���� �����
���¿� ���� ��ǳ�� Text ����
Button ��Ȱ��

 
 */

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }

    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "������ ������ ���� �� ...";
    }

    // ������ ���� ���� �� �ڵ� ����
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }

    // ���� ���� ���� �� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";

        PhotonNetwork.ConnectUsingSettings();
    }

    // Button Ŭ�� �� ȣ��
    public void Connect()
    {
        joinButton.interactable = false;
        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "�뿡 ���� ...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // �����ͷ� ������ ������ ��ư�� �������� ���������� ������ ���� ���
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ��ư Ŭ�� �� -> ���� ���ο� ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // ��ư Ŭ�� �� -> �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "�� ���� ����";
        PhotonNetwork.LoadLevel("CholhoDev");
    }

}
