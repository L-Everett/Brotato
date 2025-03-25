using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public GameObject map;

    public Vector2 mapMin;
    public Vector2 mapMax;

    public float smoothTime = 0f;      // ����ƽ��ʱ��
    public Vector3 offset = new Vector3(0, 0, -10); // 3D������zƫ��

    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        cam = GetComponent<Camera>();
        CalculateCameraSize();  
        target = GameObject.Find("Player").transform;
        map = GameObject.Find("MapImg");
        mapMin = map.GetComponent<SpriteRenderer>().bounds.min;
        mapMax = map.GetComponent<SpriteRenderer>().bounds.max;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // ����Ŀ��λ��
        Vector3 targetPosition = target.position + offset;
        targetPosition = ClampPosition(targetPosition);

        // ƽ���ƶ�
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

    Vector3 ClampPosition(Vector3 position)
    {
        // ����ʵ�ʱ߽����ƣ���������ӿڣ�
        float minX = mapMin.x + cameraHalfWidth + 1.15f;
        float maxX = mapMax.x - cameraHalfWidth - 1.15f;
        float minY = mapMin.y + cameraHalfHeight + 1.15f;
        float maxY = mapMax.y - cameraHalfHeight - 1.15f;

        // ��XYƽ������λ��
        return new Vector3(
            Mathf.Clamp(position.x, minX, maxX),
            Mathf.Clamp(position.y, minY, maxY),
            position.z
        );
    }

    void CalculateCameraSize()
    {
        // 2D����������㷽ʽ
        if (cam.orthographic)
        {
            cameraHalfHeight = cam.orthographicSize;
            cameraHalfWidth = cameraHalfHeight * cam.aspect;
        }
        // 3D͸��������㷽ʽ
        else
        {
            float distance = Mathf.Abs(transform.position.z);
            cameraHalfHeight = distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            cameraHalfWidth = cameraHalfHeight * cam.aspect;
        }
    }

#if UNITY_EDITOR
    // ��Scene��ͼ���ƿ��ӻ��ı߽��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3(
            (mapMin.x + mapMax.x) / 2,
            (mapMin.y + mapMax.y) / 2,
            0
        );
        Vector3 size = new Vector3(
            mapMax.x - mapMin.x,
            mapMax.y - mapMin.y,
            0.1f
        );
        Gizmos.DrawWireCube(center, size);
    }
#endif
}