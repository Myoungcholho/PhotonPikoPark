using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIManager instance
    {
        get
        {
            if (instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private UIManager m_instance;

    public Text playerCountText;


    private void Awake()
    {
        if(m_instance != null) 
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUserCountText(int playerCount)
    {
        playerCountText.text = "PlayerCount :" + playerCount;
    }
}
