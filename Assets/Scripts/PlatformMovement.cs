using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformMovement : MonoBehaviour
{
    [Serializable]
    public struct MovementPoint
    {
        public Vector2 m_Position;
        public float m_TimeStay;
        public Color m_color;
    }

    [SerializeField] private Color trailColor;
    [SerializeField] private float platformSpeed;
    [SerializeField][ReadOnlyInspector] private float timeToTravel;
    public List<MovementPoint> movementPoints;

    private UnityAction MoveAction;

    private int indexPlatform;
    private int forceDirection;
    private float startTime;
    private float journeyLength;

    private void Awake()
    {
        if (movementPoints.Count <= 1)
        {
            this.enabled = false;
            return;
        }
        indexPlatform = 0;
        transform.position = movementPoints[0].m_Position;
        forceDirection = 1;
        RestartMovement();
        MoveAction += MovePlatformTo;
    }

    private void FixedUpdate()
    {
        MoveAction?.Invoke();
        if (Vector3.Distance(transform.position, movementPoints[indexPlatform + forceDirection].m_Position) < 0.01f && MoveAction != null)
        {
            StartCoroutine(StopTime(movementPoints[indexPlatform + forceDirection].m_TimeStay));
        }
    }

    private void RestartMovement()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(movementPoints[indexPlatform].m_Position, 
            movementPoints[indexPlatform + forceDirection].m_Position);
    }

    private void MovePlatformTo()
    {
        float distCovered = (Time.time - startTime) * platformSpeed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(movementPoints[indexPlatform].m_Position,
            movementPoints[indexPlatform + forceDirection].m_Position,
            fractionOfJourney);
    }

    IEnumerator StopTime(float time)
    {
        MoveAction -= MovePlatformTo;
        yield return new WaitForSeconds(time);
        indexPlatform += forceDirection;
        if (indexPlatform == 0 || indexPlatform == movementPoints.Count - 1)
        {
            forceDirection *= -1;
        }
        RestartMovement();
        MoveAction += MovePlatformTo;
    }


    private void OnValidate()
    {
        timeToTravel = UpdateTimeToTravel();
    }

    public float UpdateTimeToTravel()
    {
        float timeCalculated = 0;
        for (int i = 0; i < movementPoints.Count; i++)
        {
            if (i!= movementPoints.Count -1)
            {
                if (i != 0)
                {
                    timeCalculated += movementPoints[i].m_TimeStay;
                }
                timeCalculated += Vector2.Distance(movementPoints[i].m_Position, movementPoints[i+1].m_Position) / platformSpeed;
            }
        }
        return timeCalculated;
    }

    public void SetPositionPlatform(int index)
    {
        transform.position = movementPoints[index].m_Position;
    }

    public void UpdatePosition(int index)
    {
        if (movementPoints.Count == 0)
        {
            Debug.Log("No elements in the list");
            return;
        }
        MovementPoint point = movementPoints[index];
        point.m_Position = transform.position;
        movementPoints[index] = point;
    }

    public void CreateNewPoint()
    {
        MovementPoint point;
        point.m_Position = transform.position;
        point.m_TimeStay = 0;
        point.m_color = Color.black;
        movementPoints.Add(point);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.transform.parent = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = trailColor;
        for (int i = 0; i < movementPoints.Count; i++)
        {
            Gizmos.DrawIcon(movementPoints[i].m_Position, "Platform", true, movementPoints[i].m_color);
            //Pour le jour où j'aurais pas la flemme de tout découper
            //Gizmos.DrawIcon(movementPoints[i].m_Position, "Numbers_" + (i%10).ToString()); 
            if (i != 0)
            {
                Gizmos.DrawLine(movementPoints[i-1].m_Position, movementPoints[i].m_Position);
            }
        }
    }
}