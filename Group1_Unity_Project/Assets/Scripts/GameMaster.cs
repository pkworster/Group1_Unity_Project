using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

public Player Player;
public static GameMaster Instance;
 public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }

    private void Awake() {
        Instance = this;
    }
}