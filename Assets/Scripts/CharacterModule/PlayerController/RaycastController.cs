using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = .005f;
    private const float dstBetweenRays = .055f;

    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public float horizontalRayLength;
    [HideInInspector]
    public float verticalRayLength;

    public BoxCollider2D coll;
    [HideInInspector]
    public RaycastOrigins raycastOrigins;
 

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

	public void UpdateRaycastOrigins()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);
		float distance;
		float flipCenterX;
		distance = (bounds.min.x + bounds.max.x) / 2 - transform.position.x;
		flipCenterX = transform.position.x + 2 * distance;

        horizontalRayLength = bounds.extents.x;
        verticalRayLength = bounds.extents.y;
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
		raycastOrigins.center = new Vector3 ( flipCenterX , transform.position.y,transform.position.z);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;


        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
		public Vector3 center;
    }
}
