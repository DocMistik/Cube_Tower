using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//� ��������� ������� ����������� ���� (���� ��� 34(��� ������) ��������)

public class Game_Controller : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0); //�������� ��������� ��� (6)
    public float CubeChangePlaceSpeed = 0.5f; //�������� ����� ���������, ����� ��������� ����� ��� (7)
    public Transform cubePlace; //���������� ����� ��������� �� ��� ������ CubeInvis � Unity (21)
    private float camMoveToYPosition, camMoveSpeed = 2f;// ���������� ����� ������� ������ �� Y + c������� ������

    public Text scoreTxtBest,scoreTxtNow;

    public GameObject[] cubesToCreate;

    public GameObject  allCubes, vfx; //������ ����, ������� ����� ��������� + ������ �� All Cubes � Unity(23)
    public GameObject[] canvasStartPage;
    private Rigidbody allCubesRB; // ������� ���������� ����� �������� ������ (31)

    public Color[] bgColors;
    private Color toCameraColor;

    private bool IsLose, firstCube; //������� ����������, ����� ������ ����� �������� (37)

    private List<Vector3> allCubesPosition = new List<Vector3> // ������������� ������ � �������� ������������ (15)
    {
        new Vector3 (0, 0, 0),
        new Vector3 (1, 0, 0),
        new Vector3 (-1, 0, 0),
        new Vector3 (0, 1, 0),
        new Vector3 (0, 0, 1),
        new Vector3 (0, 0, -1),
        new Vector3 (1, 0, 1),
        new Vector3 (-1, 0, -1),
        new Vector3 (-1, 0, 1),
        new Vector3 (1, 0, -1),
    };

    private int prevCounMaxHorizontal;
    private Transform mainCam;
    private Coroutine showCubePlace; //������� ����� �������� ��� ���� ����� ��������� ������ ������(40)

    private List<GameObject> posiblerCubesToCreate = new List<GameObject>();
    
    private void Start() //��������� ��������, ��� ����� ������ ������� (8)
    {
        if (PlayerPrefs.GetInt("score") < 5)
            posiblerCubesToCreate.Add(cubesToCreate[0]);
        else if (PlayerPrefs.GetInt("score") < 20)
        {
            AddPosibleCubes(2);   
        }
        else if (PlayerPrefs.GetInt("score") < 30)
        {
            AddPosibleCubes(3);
        }
        else if (PlayerPrefs.GetInt("score") < 45)
        {
            AddPosibleCubes(4);
        }
        else if (PlayerPrefs.GetInt("score") < 60)
        {
            AddPosibleCubes(5);
        }
        else if (PlayerPrefs.GetInt("score") < 80)
        {
            AddPosibleCubes(6);
        }
        else if (PlayerPrefs.GetInt("score") < 100)
        {
            AddPosibleCubes(7);
        }
        else if (PlayerPrefs.GetInt("score") < 125)
        {
            AddPosibleCubes(8);
        }
        else if (PlayerPrefs.GetInt("score") < 135)
        {
            AddPosibleCubes(9);
        }
        else if (PlayerPrefs.GetInt("score") < 150)
        {
            AddPosibleCubes(10);
        }
        else 
        {
            AddPosibleCubes(11);
        }
        scoreTxtBest.text = "best: " + PlayerPrefs.GetInt("score");
        scoreTxtNow.text = "now: 0";

        toCameraColor = Camera.main.backgroundColor;
        mainCam = Camera.main.transform;//������� ���������� ����� �������� ������ �� ���������� Y
        camMoveToYPosition = 5.9f + nowCube.y - 1f; // �������� ������� ������ ��� nowCube(0, 1, 0), ����� ������ ��� �� ������� ������������ ����

        allCubesRB = allCubes.GetComponent<Rigidbody>(); //���������� � Rigidbody ����� ���� ����� �������� � ��������� ���������� (32)
        showCubePlace = StartCoroutine(ShowCubePlace()); // �������� ��������, ShowCubePlace �������� ����� (9)
        //����������� ����� ����� ������� �������� (41)
    }
    private void Update() //��������� ����� �� ���������� ������� ���� ��� ������� �� ����� (24)
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && cubePlace != null && allCubes != null && !EventSystem.current.IsPointerOverGameObject())
        //GetMouseButtonDown(0) - ��� (����� ������� � �����)
        //��-�� ���� ��� ���������� ������ ������� && cubePlace != null (39)
        // ���� �� ������ �� �����-������ ������ ���� �����(����������) ��� �������� �� ����������� ��� �� ������������ � ����� �� �����������
        {
#if !UNITY_EDITOR //����� ���� ����� ����������� ���� �� �� � ����� ������ (����� �� �������� ��� ��) (25)
            if(Input.GetTouch(0).phase != TouchPhase.Began)
            return;
#endif


            if (!firstCube)
            {
                firstCube = true;
                foreach (GameObject obj in canvasStartPage)// ���������� ������ �� ���������� ���������� ���� �����(����������) ��� ��������
                {
                    Destroy(obj);
                }
            }
            GameObject createCube = null;
            if (posiblerCubesToCreate.Count == 1)
                createCube = posiblerCubesToCreate[0];
            else
                createCube = posiblerCubesToCreate[UnityEngine.Random.Range(0, posiblerCubesToCreate.Count)];

            GameObject newCube = Instantiate( //������� ������ cubeToCreate � ���������� cubePlace.position,  Quaternion.identity ����������� �������� (26)
                createCube,
                cubePlace.position,
                Quaternion.identity) as GameObject;

            newCube.transform.SetParent(allCubes.transform); //��������� �������� All Cubes (27)
            nowCube.setVector(cubePlace.position); //�� ������ ���������� ��� ������ �������� ���� (28)
            allCubesPosition.Add(nowCube.getVector()); //��������� ������ ������� ���������� � ������ (29)

            if (PlayerPrefs.GetString("music") != "OFF")
                GetComponent<AudioSource>().Play();

            GameObject newVfx = Instantiate(vfx, cubePlace.position, Quaternion.identity) as GameObject;
            Destroy(newVfx, 2f);
            allCubesRB.isKinematic = true; //�������� ���������� (33)
            allCubesRB.isKinematic = false; //��������� ���������� (34)
            //��� ����� ������� �� ������ ��� ��������(�� ������)

            SpawnPosition(); //�������� ����� ����� �� ����� ��������, � �� ����� 0,5 ������ (30)

            MoveCameraChangeBg(); //�������� ����� ����� �������� ������
        }
        if (!IsLose && allCubesRB.velocity.magnitude > 0.1f)// ��������� �������� �� ����� ����������, ���� ��� �� ����� � (35)
        {
            Destroy(cubePlace.gameObject); //����� ����� (36)
            IsLose = true; //������������� �������� true ����� ���������� ��������� ���� ��� (38)
            StopCoroutine(showCubePlace);//������������� �������� (42)
        }

        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition,
            new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z), camMoveSpeed * Time.deltaTime);
        //���������� ����� ������, ����, ��������
        if (Camera.main.backgroundColor != toCameraColor)
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, toCameraColor, Time.deltaTime / 1.5f);
    
    }
    IEnumerator ShowCubePlace() //������� �������� � ����������� ������ (10)
    {
        while (true)
        {
            SpawnPosition(); // ������� ��� �������� � ������ ����� (12)

            yield return new WaitForSeconds(CubeChangePlaceSpeed);// �������� ����� ����� 0,5 ��� (11)
        }

    }

    private void SpawnPosition() //������� ������� ����� �������� ���� ����� ���������� ��� (13)
    {
        List<Vector3> position = new List<Vector3>(); //������� ������������ ������ (14)
        //��������� ������ �� ������ ����� ������� if (IsPositionEmpty) ������� ����� (16)
        if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubePlace.position.x)
            position.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)); //�������� ����� ���������� ����. ���� (18)

        if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != cubePlace.position.x)
            position.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != cubePlace.position.y)
            position.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != cubePlace.position.y)
            position.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != cubePlace.position.z)
            position.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));

        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != cubePlace.position.z)
            position.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
        //&& nowCube.z - 1 != cubePlace.position.z) � �������� ����� ����� ������ ��� ���� � ���� ����� �� ������ 

        if (position.Count > 1)
            cubePlace.position = position[UnityEngine.Random.Range(0, position.Count)];// ������� ��� ��������� ������� ������ ���� (22)

        else if (position.Count == 0)
            IsLose = true;
        else
            cubePlace.position = position[0];
    }
    private bool IsPositionEmpty(Vector3 TargetPos) //������� true & false ����� ��������� ������ �� ������ (17)
    {
        if (TargetPos.y == 0) // ����� ���� �� ��������� ���� ��������� (19)
        {
            return false;
        }
        foreach (Vector3 pos in allCubesPosition) // ���������� ������������ ������, ����� �� ���� ����� �� ������� ��� � ������ if ���� (20)
        {
            if (pos.x == TargetPos.x && pos.y == TargetPos.y && pos.z == TargetPos.z)
            {
                return false;
            }

        }
        return true;
    }

    private void MoveCameraChangeBg() //���������� ������ ����� ��� ���� ����������
    {
        int maxX = 0, maxY = 0, maxZ = 0, maxHorizontal;

        foreach (Vector3 pos in allCubesPosition)
        {
            if (Math.Abs(Convert.ToInt32(pos.x)) > maxX)
                maxX = Convert.ToInt32(pos.x);

            if (Math.Abs(Convert.ToInt32(pos.y)) > maxY)
                maxY = Convert.ToInt32(pos.y);

            if (Math.Abs(Convert.ToInt32(pos.z)) > maxZ)
                maxZ = Convert.ToInt32(pos.z); //�������� ������������ ������� �� ���� ���� �����������
        }
        maxY--;
        if (PlayerPrefs.GetInt("score") < maxY)
            PlayerPrefs.SetInt("score", maxY);
        scoreTxtBest.text = "best: " + PlayerPrefs.GetInt("score");
        scoreTxtNow.text= "now: " + maxY;

        camMoveToYPosition = 5.9f + nowCube.y - 1f;

        maxHorizontal = maxX > maxZ ? maxX : maxZ;

        if (maxHorizontal % 3 == 0 && prevCounMaxHorizontal != maxHorizontal)
        {
            mainCam.localPosition += new Vector3(0, 0, -3f);
            prevCounMaxHorizontal = maxHorizontal;
        }

        if (maxY >= 74)
            toCameraColor = bgColors[23];
        else if (maxY >= 71)
            toCameraColor = bgColors[22];
        else if (maxY >= 68)
            toCameraColor = bgColors[21];
        else if (maxY >= 65)
            toCameraColor = bgColors[20];
        else if (maxY >= 62)
            toCameraColor = bgColors[19];
        else if (maxY >= 59)
            toCameraColor = bgColors[18];
        else if (maxY >= 56)
            toCameraColor = bgColors[17];
        else if (maxY >= 53)
            toCameraColor = bgColors[16];
        else if (maxY >= 50)
            toCameraColor = bgColors[15];
        else if (maxY >= 47)
            toCameraColor = bgColors[14];
        else if (maxY >= 44)
            toCameraColor = bgColors[13];
        else if (maxY >= 41)
            toCameraColor = bgColors[12];
        else if (maxY >= 38)
            toCameraColor = bgColors[11];
        else if (maxY >= 35)
            toCameraColor = bgColors[10];
        else if (maxY >= 32)
            toCameraColor = bgColors[9];
        else if (maxY >= 29)
            toCameraColor = bgColors[8];
        else if (maxY >= 26)
            toCameraColor = bgColors[7];
        else if (maxY >= 23)
            toCameraColor = bgColors[6];
        else if (maxY >= 20)
            toCameraColor = bgColors[5];
        else if (maxY >= 17)
            toCameraColor = bgColors[4];
        else if (maxY >= 14)
            toCameraColor = bgColors[3];
        else if (maxY >= 11)
            toCameraColor = bgColors[2];
        else if (maxY >= 8)
            toCameraColor = bgColors[1];
        else if (maxY >= 5)
            toCameraColor = bgColors[0];

    }
    private void AddPosibleCubes(int till)
    {
        for (int i = 0; i < till; i++)//dd
            posiblerCubesToCreate.Add(cubesToCreate[i]);
    }
    struct CubePos //��������� ������� �� �������� ��������� ������-���� ������� (1)
    {
        public int x, y, z; // ���������� � ������������ (2)

        public CubePos(int x, int y, int z) // ����������� ��� ����� ���������� ���������� (3)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 getVector() // ����� ��� ����������� ������� (4)
        {
            return new Vector3(x, y, z);
        }
        public void setVector(Vector3 pos) // ����� ��� ������ ������� (5)
        {
            x = Convert.ToInt32(pos.x); // ������������ �� float � int
            y = Convert.ToInt32(pos.y);
            z = Convert.ToInt32(pos.z);
        }

    }
}
