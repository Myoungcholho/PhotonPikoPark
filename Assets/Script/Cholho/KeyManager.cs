using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction
{
    P1_LEFT,
    P1_RIGHT,
    P1_JUMP,
    P2_LEFT,
    P2_RIGHT,
    P2_JUMP,
    ESC,
    COUNT
}

public static class Key
{
    public static Dictionary<KeyAction, KeyCode> dictionary = new Dictionary<KeyAction, KeyCode>();
}

public class KeyManager : MonoBehaviour
{
    int key = -1;

    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.A,KeyCode.D,KeyCode.W,
        KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,
        KeyCode.Escape
    };

    private void Awake()
    {
        for (int i = 0; i < (int)KeyAction.COUNT; ++i)
        {
            Key.dictionary.Add((KeyAction)i, defaultKeys[i]);
        }
    }

    private void OnGUI()
    {
        Event KeyEvent = Event.current;

        if (KeyEvent.isKey)
        {
            KeyCode temp = KeyEvent.keyCode;

            for (int i = 0; i < (int)KeyAction.COUNT; ++i)
            {
                if (Key.dictionary[(KeyAction)i] == temp)
                {
                    key = -1;
                    return;
                }
            }

            Key.dictionary[(KeyAction)key] = KeyEvent.keyCode;
            
            key = -1;
        }
    }

    /*Button에서 OnClick() 으로 사용 중*/
    public void ChangeKey(int num)
    {
        key = num;
    }
}
