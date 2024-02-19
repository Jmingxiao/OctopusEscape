using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    [Header("Scripts Reference:")]
    public HookRope grappleRope;

    [Header("Layers:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Reference:")]
    Transform player;
    public Transform pivot;
    public Transform hookPoint;

    [Header("Physics Reference:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [HideInInspector]public bool isGrappling = false;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        hookPoint = gameObject.transform;
        player = pivot.parent;
    }

    private void Update()
    {
        launchToPoint = !Input.GetKey(KeyCode.LeftControl);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerController.Instance.HasJumping = true;
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grappleRope.enabled)
            {
                RotateHook(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateHook(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 hookPointDistance = hookPoint.position - player.localPosition;
                    Vector2 targetPos = grapplePoint - hookPointDistance;
                    player.position = Vector2.Lerp(player.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 1;
            PlayerController.Instance.m_state = PlayerController.States.None;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateHook(mousePos, true);
        }
    }

    void RotateHook(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - pivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - pivot.position;
        if (Physics2D.Raycast(hookPoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(hookPoint.position, distanceVector.normalized);
            var go = _hit.transform.gameObject;
            if (go.layer == grappableLayerNumber || grappleToAll)
            {   
                if (Vector2.Distance(_hit.point, hookPoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)pivot.position;
                    grappleRope.enabled = true;
                    isGrappling =true;
                    PlayerController.Instance.m_state = PlayerController.States.IsGrappling; 

                    if(go.layer == 7)
                    {     
                        go.GetComponent<Rigidbody2D>().AddForce(-grappleDistanceVector.normalized * 3, ForceMode2D.Impulse);
                    }

                }else{
                    ShootRope(distanceVector); 
                }
            }
        }
        else
        {
            ShootRope(distanceVector);
        }
    }

    void ShootRope(Vector2 distanceVector){
        isGrappling = false;
        grapplePoint = new Vector2(hookPoint.position.x,hookPoint.position.y) +  distanceVector.normalized* maxDistance;
        grappleDistanceVector = grapplePoint - (Vector2)pivot.position;
        grappleRope.enabled = true;
        PlayerController.Instance.m_state = PlayerController.States.IsGrappling; 
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = hookPoint.position - player.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    public void DropRope()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
        PlayerController.Instance.m_state = PlayerController.States.None;
    }
    private void OnDrawGizmosSelected()
    {
        if (hookPoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(hookPoint.position, maxDistance);
        }
    }

}
