using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public BasePlayer activePlayer;
    private void Awake()
    {
        Instance = this;
    }


}
