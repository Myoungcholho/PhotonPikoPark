using Photon.Pun;
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

    public int playerCount = 0;         // private

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }

        if(Input.GetMouseButtonDown(0) && readyPanel.activeSelf) 
        {
            readyPanel.SetActive(false);
            CreateCharacter();
        }

    }

    // 변수 동기화 Inferface 메서드
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(playerCount);
        }
        else
        {
            playerCount = (int)stream.ReceiveNext();
        }

        Debug.Log("playerCount" + playerCount);
    }

    // 룸을 나갈 때 자동 실행되는 메서드
    public override void OnLeftRoom()
    {
        --playerCount;
        SceneManager.LoadScene("Lobby");
    }

    private void CreateCharacter()
    {
        Vector3 randomSpawnPos = Random.insideUnitCircle * 5f;
        randomSpawnPos.y = 0f;

        Debug.Log("Player SpawnPos x :" + randomSpawnPos.x + " , " + randomSpawnPos.y);

        int spawnType = playerCount % playerPrefab.Length;
        PhotonNetwork.Instantiate(playerPrefab[spawnType].name, randomSpawnPos, Quaternion.identity);
        ++playerCount;
    }
}
