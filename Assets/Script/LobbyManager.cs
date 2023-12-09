using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/*
 자.. 들어가야하는게
버튼을 눌렀을 때 서버 만들기
상태에 대한 말풍선 Text 변경
Button 비활성

 
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
        connectionInfoText.text = "마스터 서버에 접속 중 ...";
    }

    // 마스터 서버 접속 시 자동 실행
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
    }

    // 서버 접속 실패 시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        PhotonNetwork.ConnectUsingSettings();
    }

    // Button 클릭 시 호출
    public void Connect()
    {
        joinButton.interactable = false;
        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "룸에 접속 ...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 마스터로 접속이 성공해 버튼을 눌렀지만 순간적으로 끊겼을 때를 대비
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // 버튼 클릭 후 -> 랜덤 조인에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // 버튼 클릭 후 -> 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "방 참가 성공";
        PhotonNetwork.LoadLevel("CholhoDev");
    }

}
