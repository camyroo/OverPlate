using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIPanel : MonoBehaviour
{
    public TMP_Text playerName;
    PlayerController player;

    public void AssignPlayer(int index) {
        StartCoroutine(AssignPlayerDelay(index));
    }

    IEnumerator AssignPlayerDelay(int index) {
        yield return new WaitForSeconds(0.01f);
        player = GameManager.instance.playerList[index].GetComponent<PlayerInputHandler>().playerController;

        SetUpInfoPanel(); 
    }

    void SetUpInfoPanel() {
        if(player != null) {
            playerName.text = player.thisPlayersName.ToString();
        }
    }
}
