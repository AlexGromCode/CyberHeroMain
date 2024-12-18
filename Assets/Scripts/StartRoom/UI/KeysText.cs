using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysText : MonoBehaviour
{
    [SerializeField] private Text _keysText;

    private void Update() => _keysText.text = PlayerMain.CountKeys.ToString();
}
