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

    // ���� �� ĳ���� ����
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

    // ���� ����ȭ Inferface �޼���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    // �÷��̾ �濡 ������ ��ΰ� �޼��� ����(��� ���� ����� ȣ����� ����)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UIManager.instance.UpdateUserCountText(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // ���� ���� �� �ڵ� ����Ǵ� �޼���
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
