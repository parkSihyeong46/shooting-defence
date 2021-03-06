using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_SelectCharacter : MonoBehaviour
{
    [SerializeField] Player_SetValues playerSetValues;

    public int stageIndex;

    private void Start()
    {
        playerSetValues = FindObjectOfType<Player_SetValues>();
    }

    public void SelectSkin(int skinIndex)
    {
        playerSetValues.skinNumber = skinIndex;
    }

    public void SelectGun(int gunIndex)
    {
        playerSetValues.gunNumber = gunIndex;
    }

    public void StartStage(int stageIndex)
    {
        stageIndex = this.stageIndex;

        MapStageManager.Instance.LoadAll();
        BlockManager.Instance.LoadAll();
        EnemyManager.Instance.LoadAll();
        TurretManager.Instance.LoadAll();
        ObjManager.Instance.LoadAll();

        GameManager.Instance.LoadScene(GameManager.PlayStates.IN_GAME, stageIndex);
    }
}
