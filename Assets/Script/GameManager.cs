using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks , IPunObservable
{
    #region Singleton
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }
    private static GameManager m_instance;
    #endregion

    public GameObject[] playerPrefab;
    public GameObject readyPanel;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 접속 시 캐릭터 생성
    void Start()
    {
        Vector3 randomSpawnPos = Random.insideUnitCircle * 5f;
        randomSpawnPos.y = 0f;

        int spawnType = (PhotonNetwork.CurrentRoom.PlayerCount-1) % playerPrefab.Length;
        PhotonNetwork.Instantiate(playerPrefab[spawnType].name, randomSpawnPos, Quaternion.identity);

        UIManager.instance.UpdateUserCountText(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    // 변수 동기화 Inferface 메서드
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    // 플레이어가 방에 들어오면 모두가 메서드 실행(방금 들어온 사람은 호출되지 않음)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UIManager.instance.UpdateUserCountText(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // 룸을 나갈 때 자동 실행되는 메서드
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
