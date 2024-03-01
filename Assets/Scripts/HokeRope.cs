using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRope : MonoBehaviour {
    [Header("References:")]
    public Hook grapplingHook;
    public LineRenderer m_lineRenderer;

    [Header("Settings:")]
    [SerializeField] private int precision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool straightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        straightLine = false;
        LinePointsToFirePoint();
        m_lineRenderer.enabled = true;
        
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingHook.hookPoint.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!straightLine)
        {
            if (m_lineRenderer.GetPosition(precision - 1).x == grapplingHook.grapplePoint.x)
            {
                straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if(!grapplingHook.isGrappling){
                Debug.Log("Dropping rope");
                grapplingHook.DropRope();
                return;
            }
            if (!isGrappling)
            {
                grapplingHook.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingHook.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingHook.hookPoint.position, grapplingHook.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingHook.hookPoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, new Vector3(currentPosition.x, currentPosition.y, 1));
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingHook.hookPoint.position);
        m_lineRenderer.SetPosition(1, grapplingHook.grapPt.position);
        //m_lineRenderer.SetPosition(1, grapplingHook.grapplePoint);
    }
}
