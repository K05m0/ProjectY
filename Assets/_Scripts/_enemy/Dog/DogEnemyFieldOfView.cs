using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemyFieldOfView : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)] private float deleyToRefresh;
    [SerializeField, Range(0.1f, 1f)] private float meshResolution;

    [SerializeField, Range(1, 5)] private float f_edgeDstThreshold;
    [SerializeField, Range(1, 5)] private int i_edgeResolveIteration;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    public bool isPlayerInFieldOfView;
    public Transform playerTransform;
    private void Awake()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }
    public IEnumerator FindPlayerWithDeley(EnemyStats stats)
    {
        while (true)
        {
            yield return new WaitForSeconds(deleyToRefresh);
            FindPlayerInEnemyFieldOfView(stats);
        }
    }
    public void DrawEnemyFieldOfView(EnemyStats stats)
    {
        int stepCount = Mathf.RoundToInt(stats.viewAngle * meshResolution);
        float stepAngleSize = stats.viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - stats.viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle, stats);

            if (i > 0)
            {
                bool edgeDstTresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > f_edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstTresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast, stats);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
        viewMesh.RecalculateNormals();

    }
    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast, EnemyStats stats)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < i_edgeResolveIteration; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle, stats);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > f_edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }
    private ViewCastInfo ViewCast(float globalAngle, EnemyStats stats)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, stats.seeRange, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
            return new ViewCastInfo(false, transform.position + dir * stats.seeRange, stats.seeRange, globalAngle);
    }
    private struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    private struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void FindPlayerInEnemyFieldOfView(EnemyStats stats)
    {

        Collider[] targetPlayer = Physics.OverlapSphere(transform.position, stats.seeRange, targetMask);

        if (targetPlayer.Length > 0)
        {
            Transform target = targetPlayer[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < stats.viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    isPlayerInFieldOfView = true;
                    playerTransform = target.transform;
                }

                else
                {
                    isPlayerInFieldOfView = false;
                    playerTransform = null;
                }
            }
            else
            {
                isPlayerInFieldOfView = false;
                playerTransform = null;
            }
        }
        else
        {
            isPlayerInFieldOfView = false;
            playerTransform = null;
        }

    }
}
