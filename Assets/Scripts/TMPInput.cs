using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPInput : MonoBehaviour
{
    public static TMP_InputField tpInput;
    public static string namePlayer;
    private void Start()
    {
        tpInput = GetComponent<TMP_InputField>();
        namePlayer = tpInput.text;
       // DontDestroyOnLoad(tpInput);
    }
}
