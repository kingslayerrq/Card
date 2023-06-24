using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    private Transform _hand;
    private List<Transform> leftCard = new List<Transform>();
    private List<Transform> rightCard = new List<Transform>();
    private Transform curActiveCard;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        _hand = CardManager.Instance.playerHand;
    }

    
}
