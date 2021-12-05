using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerSetting{
    public GameObject playerObject;
    public GameObject catchObject;
    public GameObject missObject;
}

public enum Daytime{
    Day,
    Night
}

[System.Serializable]
public struct StageSetting{
    public float HPPerSecond;
    public float dayDuration;
    public float nightDuration;
    public Daytime startDaytime;
}

[System.Serializable]
public struct UISetting{
    public GameObject marketObject;
    public GameObject storeObject;
    public GameObject fishingManager;
    public GameObject bucket;
}

public class GameManager : MonoBehaviour
{
    public PlayerSetting playerSetting;
    public StageSetting stageSetting;
    public UISetting uiSetting;
    public List<FishInfo> fishInfo;

    //상수들
    public const string TAG_TERRAIN = "Terrain";
    public const string TAG_MARKET = "Market";
    public const string TAG_PORTAL = "Portal";
    public const string TAG_STORE = "Store";
    public const string TAG_PLAYER = "Player";
    public const string TAG_DEPTH = "Depth";
    public const int BAIT_NORMAL = 0;
    public const int BAIT_RED = 1;
    public const int BAIT_BLUE = 2;
    public const int BAIT_STRONG = 3;

    private const int OBJECT_NULL = 0;
    private const int OBJECT_STORE = 1;
    private const int OBJECT_MARKET = 2;
    private const int OBJECT_PORTAL = 3;
    private const int OBJECT_OBSTACLE = 4;

    // 배의 속도
    private const float playerSpeed = 15.0f;
    // 배위 회전 속도
    private const float rotateSpeed = 180.0f;
    // 카메라 줌 인/아웃 속도
    private const float cameraSpeed = 10.0f;
    // 카메라 최대 줌인
    private const float minFOV = 40.0f;
    // 카메라 최대 줌아웃
    private const float maxFOV = 100.0f;
    // 배 충돌 Raycast 범위
    private const float raycastRange = 2.0f;

    // 물고기가 오는데 걸리는 최소 시간
    private const float biteMin = 2.0f;
    // 물고기가 오는데 걸리는 평균 시간 (최소시간 이후로)
    private const float biteAverage = 10.0f;
    // 물고기가 물었을 지 확인할 빈도
    private const float biteFrequency = 0.5f;
    // 물고기가 물었을 때 다시 스페이스바를 눌러야 하는 시간제한
    private const float biteMax = 1.0f;
    // 물고기를 놓쳤을 때 다시 물고기를 잡을 수 있게 되는 시간
    private const float biteDelay = 2.0f;

    // 시간 관련
    private Daytime daytime;
    private float timeFlow;

    // 낚시 관련
    private bool isFishing = false;
    private int fishPhase = 0;
    private float fishTimeFlow;

    private bool isOpenUI = false;
    private bool isGameOver = false;
    private Animator playerAnimator;
    private int prevFishNum;

    // Start is called before the first frame update
    void Start()
    {
        timeFlow = 0;
        daytime = stageSetting.startDaytime;
        Fish.FishList = fishInfo;

        isFishing = false;
        fishPhase = 0;
        fishTimeFlow = 0;

        isOpenUI = false;
        isGameOver = false;

        if(playerSetting.playerObject.transform.GetChild(0).GetComponent<Animator>())
        {
            playerAnimator = playerSetting.playerObject.transform.GetChild(0).GetComponent<Animator>();
        }

        //RandomGenerateTest();
        //PlayerFishTankTest();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            PlayerAction();
            CameraAction();
            TimeAction();
            FishingAction();
            // DieTest();
            ShowMeTheMoney();
        }
    }

    void PlayerFishTankTest()
    {
        for(int i = 0; i < 10; i++)
        {
            Player.Instance.FishTank.Add(Fish.RandomGenerate(3, 0));
        }
    }

    void DieTest()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
    }

    void ShowMeTheMoney()
    {
        if(Input.GetKeyDown(KeyCode.M) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Y))
        {
            Debug.Log("치트 사용 (돈 + 1000)");
            Player.Instance.Money = Player.Instance.Money + 1000;
        }

    }

    void RandomGenerateTest()
    {
        Dictionary<string, int> num = new Dictionary<string, int>();
        Dictionary<string, float> size = new Dictionary<string, float>();
        Dictionary<string, float> price = new Dictionary<string, float>();

        foreach(FishInfo fish in Fish.FishList)
        {
            num.Add(fish.name, 0);
            size.Add(fish.name, 0);
            price.Add(fish.name, 0);
        }

        for(int i = 0; i < 10000; i++)
        {
            Fish fish = Fish.RandomGenerate(3, 2);

            num[fish.Name]++;
            size[fish.Name] += fish.Size;
            price[fish.Name] += fish.Price;
        }

        foreach(FishInfo fish in Fish.FishList)
        {
            size[fish.name] /= num[fish.name];
            price[fish.name] /= num[fish.name];
            Debug.Log("Name: " + fish.name + ", Num: " + num[fish.name] + ", Size: " + size[fish.name] + " , Price: " + price[fish.name]);
        }

    }

    void PlayerAction()
    {
        if(isFishing || CheckUIOpen())
        {
            return;
        }
        playerAnimator.SetBool("isMoving", false);

        GameObject playerObject = playerSetting.playerObject;
        if(playerObject != null)
        {
            MovePlayer(playerObject);
            PlayerBehavior pb = playerSetting.playerObject.GetComponent<PlayerBehavior>();
            if(pb)
            {
                if(!isOpenUI)
                {
                    isOpenUI = true;
                    if(pb.IsMarket)
                    {
                        OpenMarket();
                    }
                    else if(pb.IsStore)
                    {
                        OpenStore();
                    }
                    else
                    {
                        isOpenUI = false;
                    }
                }
                else
                {
                    if(!pb.IsMarket && !pb.IsStore)
                    {
                        isOpenUI = false;
                    }
                }
            }
            else
            {
                Debug.Log("Player Behavior 를 찾을 수 없습니다");
            }
        }
    }

    void CameraAction()
    {
        if(!CheckUIOpen())
        {
            float scrollSpeed = Input.GetAxis("Mouse ScrollWheel") * cameraSpeed * (-1);
            Camera.main.fieldOfView += scrollSpeed;
            if(Camera.main.fieldOfView > maxFOV)
            {
                Camera.main.fieldOfView = maxFOV;
            }
            else if(Camera.main.fieldOfView < minFOV)
            {
                Camera.main.fieldOfView = minFOV;
            }
            else
            {
                Camera.main.transform.Rotate(0.5f * (-scrollSpeed), 0f, 0f);
            }
        }
    }

    void TimeAction()
    {
        timeFlow += Time.deltaTime;
        if(daytime == Daytime.Day)
        {
            if(Daytime.Night > 0){
                if(timeFlow >= stageSetting.dayDuration)
                {
                    daytime = Daytime.Night;
                    timeFlow -= stageSetting.dayDuration;
                }
            }
        }
        // if(daytime == Daytime.Night)
        else
        {
            if(Daytime.Day > 0){
                if(timeFlow >= stageSetting.nightDuration)
                {
                    daytime = Daytime.Day;
                    timeFlow -= stageSetting.nightDuration;
                }
            }
        }

        ((Ship)Player.Instance.Equip[Etype.Ship]).WearOut(stageSetting.HPPerSecond * Time.deltaTime);
        if(((Ship)Player.Instance.Equip[Etype.Ship]).IsDead)
        {
            GameOver();
        }
    }

    void FishingAction()
    {
        if(!isFishing)
        {
            if(!isOpenUI){
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if(Player.Instance.GetCapacityRatio() >= 1.0f)
                    {
                        Debug.Log("GameManager : 배 용량이 가득참!");
                    }
                    else if(Player.Instance.HasCurrentBait())
                    {
                        Debug.Log("GameManager : 낚시 시작!");
                        isFishing = true;
                        playerAnimator.SetBool("isFishing", true);
                        fishPhase = 0;
                        fishTimeFlow = 0.0f;
                        Player.Instance.UseCurrentBait();
                        prevFishNum = Player.Instance.FishTank.Count;
                    }
                    else
                    {
                        Debug.Log("GameManaer : 미끼가 없음...");
                    }
                }
            }
        }
        // 0 : 최초 사이클 돌기 전
        // 1 : 최초 사이클 돈 후
        else if (fishPhase <= 1)
        {
            // 너무 빨리 낚아올림
            if(Input.GetKeyDown(KeyCode.Space))
            {
                fishTimeFlow = 0;
                fishPhase = 4;
                if(playerSetting.missObject){
                    playerSetting.missObject.SetActive(true);
                }
                Debug.Log("GameManager : ?? 아무것도 없다");
            }
            else
            {
                fishTimeFlow += Time.deltaTime;

                float term;
                if(fishPhase == 0)
                {
                    term = biteMin;
                }
                else
                {
                    term = biteFrequency;
                }

                // 빨간 미끼, 강력 미끼 보정치
                if(Player.Instance.CurrentBait == BAIT_RED || Player.Instance.CurrentBait == BAIT_STRONG)
                {
                    term = term * 0.7f;
                }

                // 물고기 잡혔나?
                if(fishTimeFlow >= term)
                {
                    float r = Random.Range(0.0f, 1.0f);

                    if(r < biteFrequency / biteAverage)
                    {
                        fishPhase = 2;
                        fishTimeFlow = 0.0f;
                        Debug.Log("GameManager : 물었다!");
                    }
                    else
                    {
                        Debug.Log("GameManager : 기다리는 중...");
                        fishPhase = 1;
                        fishTimeFlow -= term;
                    }
                }
            }
        }
        // 2 : 물고기가 미끼 뭄
        else if (fishPhase == 2)
        {
            if(playerSetting.catchObject)
            {
                playerSetting.catchObject.SetActive(true);
            }

            // 잘 낚음
            if(Input.GetKeyDown(KeyCode.Space))
            {
                fishPhase = 3;
                if(uiSetting.fishingManager)
                {
                    uiSetting.fishingManager.SetActive(true);
                }
            }

            fishTimeFlow += Time.deltaTime;
            // 시간 지나서 놓침
            if(fishTimeFlow > biteMax)
            {
                fishTimeFlow = 0.0f;
                fishPhase = 4;

                if(playerSetting.missObject)
                {
                    playerSetting.missObject.SetActive(true);
                }

                Debug.Log("GameManager : 도망갔다...");
            }
        }
        // 3 : 물고기 잡는 중
        else if (fishPhase == 3)
        {
            if(playerSetting.catchObject)
            {
                playerSetting.catchObject.SetActive(false);
            }

            if(!uiSetting.fishingManager.activeSelf)
            {
                fishTimeFlow = 0.0f;
                // 낚시 실패
                if(Player.Instance.FishTank.Count <= prevFishNum)
                {
                    if(playerSetting.missObject)
                    {
                        playerSetting.missObject.SetActive(true);
                    }
                    fishPhase = 4;
                }
                // 낚시 성공
                else
                {
                    isFishing = false;
                }

            }
        }
        // 4 : 낚시 후 딜레이
        else if (fishPhase == 4)
        {
            if(playerSetting.catchObject)
            {
                playerSetting.catchObject.SetActive(false);
            }
            playerAnimator.SetBool("isFishing", false);
            fishTimeFlow += Time.deltaTime;
            if(fishTimeFlow > biteDelay)
            {
                if(playerSetting.missObject)
                {
                    playerSetting.missObject.SetActive(false);
                }

                isFishing = false;
                
                Debug.Log("GameManager : 다시 가자");
            }
        }

    }

    void MovePlayer(GameObject target)
    {
        Vector3 deltaPosition = new Vector3(0, 0, 0);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = playerSpeed * Time.deltaTime;

        if(horizontal != 0 || vertical != 0)
        {
            Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
            deltaPosition = dir * speed;

            RaycastHit hit;
            bool isCollide = Physics.Raycast(target.transform.position, dir, out hit, raycastRange);
            if(isCollide)
            {
                switch(hit.transform.tag)
                {
                    case TAG_DEPTH:
                        isCollide = false;
                        break;
                }
            }
            
            if(!isCollide)
            {
                target.transform.position += deltaPosition;
                Camera.main.transform.position += deltaPosition;
                playerAnimator.SetBool("isMoving", true);
            }

            float prevAngle = target.transform.eulerAngles.y;
            float angle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
            float deltaAngle = Mathf.Min(posMod(angle - prevAngle, 360), rotateSpeed * Time.deltaTime);

            if(posMod(angle - prevAngle, 360) < posMod(prevAngle - angle, 360))
            {
                angle = posMod(prevAngle + deltaAngle, 360);
            }
            else
            {
                angle = posMod(prevAngle - deltaAngle, 360);
            }

            target.transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }



    private float posMod(float num, float mod)
    {
        num = num % mod;
        while(num < 0)
        {
            num += mod;
        }
        return num;
    }

    int CollisionTest(GameObject target)
    {
        // 충돌 체크해서 뭐랑 충돌했는지 반환
        return 1;
    }

    void GameOver()
    {
        isGameOver = true;
        uiSetting.fishingManager.SetActive(false);
        if(playerAnimator)
        {
            Debug.Log("내구도 0.. 게임오버");
            playerAnimator.SetBool("isDie", true);
        }
    }

    bool CheckUIOpen()
    {
        if(uiSetting.storeObject)
        {
            if(uiSetting.storeObject.active)
                return true;
        }
        if(uiSetting.marketObject)
        {
            if(uiSetting.marketObject.active)
                return true;
        }
        if(uiSetting.bucket){
            if(uiSetting.bucket.active)
                return true;
        }

        return false;
    }

    void OpenMarket()
    {
        uiSetting.marketObject.SetActive(true);
    }

    void OpenStore()
    {
        uiSetting.storeObject.SetActive(true);
    }
}
