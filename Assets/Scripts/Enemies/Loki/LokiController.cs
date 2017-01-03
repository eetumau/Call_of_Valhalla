using UnityEngine;
using System.Collections;


public class LokiController : MonoBehaviour {

    [SerializeField]
    private GameObject _pointGameObject1;
    [SerializeField]
    private GameObject _pointGameObject2;
    [SerializeField]
    private GameObject _pointGameObject3;
    [SerializeField]
    private GameObject _pointGameObject4;

    private Transform _firstMovePoint;
    private Transform _secondMovePoint;
    private Transform _thirdMovePoint;
    private Transform _fourthMovePoint;

    private Transform _currentMovePoint;
    private LokiMovement _lokiMovement;

    // Use this for initialization
    void Awake () {
        _firstMovePoint = _pointGameObject1.GetComponent<Transform>();
        _secondMovePoint = _pointGameObject2.GetComponent<Transform>();
        _thirdMovePoint = _pointGameObject3.GetComponent<Transform>();
        _fourthMovePoint = _pointGameObject4.GetComponent<Transform>();
        _lokiMovement = GetComponent<LokiMovement>();
        StartBossFight();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartBossFight()
    {
        SetMovement();
    }

    private float GetRandomMoveTime()
    {
        return Random.Range(3f, 5f);
    }

    private Transform GetRandomMovePoint()
    {

        Transform tmptf;
        while (true)
        {
            int tmp = (int)Random.Range(0f, 4f) + 1;
            Debug.Log(tmp);


            if (tmp == 1)
            {
                if (_firstMovePoint != _currentMovePoint)
                {
                    tmptf = _firstMovePoint;
                    break;
                }
            }
            else if (tmp == 2)
            {
                if (_secondMovePoint != _currentMovePoint)
                {
                    tmptf = _secondMovePoint;
                    break;
                }
            }
            else if (tmp == 3)
            {
                if (_thirdMovePoint != _currentMovePoint)
                {
                    tmptf = _thirdMovePoint;
                    break;
                }
            }
            else
            {
                if (_fourthMovePoint != _currentMovePoint)
                {
                    tmptf = _fourthMovePoint;
                    break;
                }
            }
        }

        _currentMovePoint = tmptf;
        return tmptf;
    }

    private void SetMovement()
    {
        float tmpTime = GetRandomMoveTime();
        Transform tmpPoint = GetRandomMovePoint();
        Debug.Log("MOVE");
        StartCoroutine(Movement(tmpTime, tmpPoint));

    }

    private IEnumerator Movement(float time, Transform tf)
    {

        yield return new WaitForSeconds(time);
        _lokiMovement.MoveToPoint(tf);
        SetMovement();
    }
}
