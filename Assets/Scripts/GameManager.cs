using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerSetting{
    public GameObject playerObject;
}

public enum Daytime{
    Day,
    Night
}

[System.Serializable]
public struct StageSetting{
    public int HPperSecond;
    public float dayDuration;
    public float nightDuration;
    public Daytime startDaytime;
    public GameObject uiManager;
}

public class GameManager : MonoBehaviour
{
    public PlayerSetting playerSetting;
    public StageSetting stageSetting;
    public List<FishInfo> fishInfo;

    // 배의 속도
    private const float playerSpeed = 5.0f;

    private const int OBJECT_NULL = 0;
    private const int OBJECT_STORE = 1;
    private const int OBJECT_MARKET = 2;
    private const int OBJECT_PORTAL = 3;
    private const int OBJECT_OBSTACLE = 4;

    private Daytime daytime;
    private float timeFlow;

    // Start is called before the first frame update
    void Start()
    {
        timeFlow = 0;
        daytime = stageSetting.startDaytime;
        Fish.FishList = fishInfo;
        for(int i = 0; i < 100; i++)
        {
            Fish fish = Fish.RandomGenerate(1);
            if(fish == null){
                Debug.Log(fish);
            }else{
                Debug.Log("Name: " + fish.Name + ", Size: " + fish.Size + " , Price: " + fish.Price);
            }
        }
        Debug.Log(Fish.FishList);
        Debug.Log(Mathf.Pow(10, 1));
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        TimeAction();
    }

    void PlayerAction()
    {
        if(playerSetting.playerObject != null){
            Vector3 prevPosition = playerSetting.playerObject.transform.position;
            MovePlayer(playerSetting.playerObject);
            int collision = CollisionTest(playerSetting.playerObject);

            switch(CollisionTest(playerSetting.playerObject))
            {
                case OBJECT_STORE:
                    OpenStore();
                    break;
                case OBJECT_MARKET:
                    OpenMarket();
                    break;
                case OBJECT_PORTAL:
                    ChangeStage();
                    break;
                case OBJECT_OBSTACLE:
                    // 플레이어 위치 롤백
                    playerSetting.playerObject.transform.position = prevPosition;
                    break;
            }
        }
    }

    void TimeAction()
    {
        timeFlow += Time.deltaTime;
        if(daytime == Daytime.Day)
        {
            if(timeFlow >= stageSetting.dayDuration)
            {
                daytime = Daytime.Night;
                timeFlow -= stageSetting.dayDuration;
            }
        }
        // if(daytime == Daytime.Night)
        else
        {
            if(timeFlow >= stageSetting.nightDuration)
            {
                daytime = Daytime.Day;
                timeFlow -= stageSetting.nightDuration;
            }
        }
        //((Ship)Player.Instance.Equip[Etype.Ship]).WearOut(stageSetting.HPperSecond * Time.deltaTime);

    }

    void MovePlayer(GameObject target)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = playerSpeed * Time.deltaTime;

        target.transform.position += new Vector3(horizontal, 0, vertical).normalized * speed;
    }

    int CollisionTest(GameObject target)
    {
        // 충돌 체크해서 뭐랑 충돌했는지 반환
        return 1;
    }

    void OpenMarket()
    {
        // 물고기 파는 시장 열기
    }

    void OpenStore()
    {
        // 상점 열기
    }

    void OpenInventory()
    {
        // 인벤토리 열기
    }

    void OpenMenu()
    {
        // 메뉴 열기
    }

    void StartFishing()
    {
        // 낚시 시작
    }

    void ChangeStage()
    {
        // 다음 맵으로 장면 전환
    }
}
