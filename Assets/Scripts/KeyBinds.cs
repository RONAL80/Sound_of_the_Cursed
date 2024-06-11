using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    public List<char> Keys = new List<char>();
    public List<KeyCode> KeyCodes = new List<KeyCode> ();
    public Dictionary<KeyCode, bool> KeyHoldStates = new Dictionary<KeyCode, bool>();
    void Start()
    {
        Keys.AddRange(new char[] { 'A', 'S', 'D', 'G' ,'J', 'K', 'L' });
        foreach (char c in Keys)
        {
            KeyCode code;
            if (char.IsLetter(c))
            {
                code = (KeyCode)System.Enum.Parse(typeof(KeyCode), c.ToString().ToUpper());
            }
            else
            {
                code = (KeyCode)System.Enum.Parse(typeof(KeyCode), c.ToString());
            }

            KeyCodes.Add(code);
        }

        foreach (KeyCode keyCode in KeyCodes)
        {
            KeyHoldStates[keyCode] = false;
        }
    }

}
