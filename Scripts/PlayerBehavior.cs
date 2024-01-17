using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private bool isDefaultPlayer;
    private void Awake()
    {
        if (isDefaultPlayer) Player.Current = this;
    }
}
