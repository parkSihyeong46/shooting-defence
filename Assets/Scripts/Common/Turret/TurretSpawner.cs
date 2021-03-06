using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Player_Manager playerManager;
    public TurretSpawnUI turretSpawnUI;
    public bool isSpawn = false;
    public bool isMapEditor = false;

    public MoneyBox moneyBox;

    private IEnumerator m_Coroutine;
    public Text needMoneyText;

    const int turret1 = 30;
    const int turret2 = 50;
    const int turret3 = 100;
    const int turret4 = 200;
    const int turret5 = 40;

    void Start()
    {
        try 
        {
            moneyBox = GameObject.Find("MoneyBox").GetComponent<MoneyBox>();
            needMoneyText = GameObject.Find("ScreenUI").transform.Find("NeedMoney").GetComponent<Text>();
            m_Coroutine = PrintNeedMoneyText();

            GameObject findTurretSpawnUiGameObject = GameObject.Find("ScreenUI");
            if (findTurretSpawnUiGameObject == null)
            {
                isMapEditor = true;
                return;
            }
            turretSpawnUI = findTurretSpawnUiGameObject.GetComponent<TurretSpawnUI>();

            GameObject findPlayerManagerGameObject = GameObject.Find("Player");
            if (findPlayerManagerGameObject == null)
            {
                isMapEditor = true;
                return;
            }
            playerManager = findPlayerManagerGameObject.GetComponent<Player_Manager>();
        }
        catch
        {

        }
    }

    public GameObject SpawnTurret(TurretManager.TurretName turretName)
    {
        GameObject collisionGameObject = playerManager.GetTurretSpawnerCollision();

        if(collisionGameObject == gameObject)
        {
            isSpawn = true;
            return TurretManager.Instance.InstantiateTurret(turretName);
        }
        else
        {
            return null;
        }
    }

    public void CreateTurret(int i)
    {
        GameObject createTurretGameObject = null;

        // 터렛 추가하면 여기에 터렛 등록하면 됨
        switch(i-1)
        {
            case (int)TurretManager.TurretName.rifleTurret:
                createTurretGameObject = SpawnTurret(TurretManager.TurretName.rifleTurret);
                break;
            case (int)TurretManager.TurretName.sniperTurret:
                createTurretGameObject = SpawnTurret(TurretManager.TurretName.sniperTurret);
                break;
            case (int)TurretManager.TurretName.singleTurret:
                createTurretGameObject = SpawnTurret(TurretManager.TurretName.singleTurret);
                break;
            case (int)TurretManager.TurretName.doubleTurret:
                createTurretGameObject = SpawnTurret(TurretManager.TurretName.doubleTurret);
                break;
            default:
                break;
        }

        if(createTurretGameObject == null)
        {
            return;
        }

        if(isSpawn == true)
        {
            const string OBJECT_NAME = "Map_Objects";

            GameObject parentGameObject = GameObject.Find(OBJECT_NAME);

            if(parentGameObject != null)
                createTurretGameObject.transform.parent = parentGameObject.transform;
            else
            {
                parentGameObject = new GameObject(OBJECT_NAME);
                createTurretGameObject.transform.parent = parentGameObject.transform;
            }
        }

        createTurretGameObject.transform.position = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 추후에 가시성 위해서 터렛스포너 색 변경하는거 추가해도 괜찮음
        if (turretSpawnUI == null)
            return;

        if (isSpawn == true)
            return;

        if (!other.CompareTag("Player"))
            return;

        turretSpawnUI.PrintInitUI();
    }

    private void OnTriggerStay(Collider other)
    {
        if (turretSpawnUI == null)
            return;

        if (isSpawn == true)
            return;

        if (!other.CompareTag("Player"))
            return;

        // 무식하게 터렛 추가하면 여기 버튼 추가해서 등록하면 됨
        if (Input.GetKeyUp(KeyCode.Alpha1))     // 1
        {
            if (moneyBox.money >= turret1)
            {
                turretSpawnUI.DeleteUI();
                CreateTurret(1);

                moneyBox.SetMoneyText(moneyBox.money - turret1);
            }
            else
            {
                StartCoroutine("PrintNeedMoneyText");
            }

        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))    // 2
        {
            if (moneyBox.money >= turret2)
            {
                turretSpawnUI.DeleteUI();
                CreateTurret(2);

                moneyBox.SetMoneyText(moneyBox.money - turret2);
            }
            else
            {
                StartCoroutine("PrintNeedMoneyText");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))    // 3
        {
            if (moneyBox.money >= turret3)
            {
                turretSpawnUI.DeleteUI();
                CreateTurret(3);

                moneyBox.SetMoneyText(moneyBox.money - turret3);
            }
            else
            {
                StartCoroutine("PrintNeedMoneyText");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))    // 4
        {
            if (moneyBox.money >= turret4)
            {
                turretSpawnUI.DeleteUI();
                CreateTurret(4);

                moneyBox.SetMoneyText(moneyBox.money - turret4);
            }
            else
            {
                StartCoroutine("PrintNeedMoneyText");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (turretSpawnUI == null)
            return;

        if (isSpawn == true)
            return;

        if (!other.CompareTag("Player"))
            return;

        turretSpawnUI.DeleteUI();
    }

    IEnumerator PrintNeedMoneyText()
    {
        needMoneyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        needMoneyText.gameObject.SetActive(false);
    }
}
