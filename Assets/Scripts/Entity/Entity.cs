using UnityEngine;

public class Entity : MonoBehaviour
{
    public Camera playerCamera;

    public float moveSpeed = 8f;
    public float stopDistance = 1f;

    Vector3 startPosition;
    Vector3 targetPosition;
    bool isMoving = false; 

    float height2;


    void Start()
    {
        height2 = GetComponent<Renderer>().bounds.size.y * 2;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(targetPosition, transform.position) < stopDistance)
            {
                isMoving = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.name == "Mesh")
                {
                    MoveTo(hit.point);
                }
            }
        }
    }

    protected void MoveTo(Vector3 targetPos)
    {
        startPosition = transform.position;
        targetPos.y = transform.position.y;
        targetPosition = targetPos;
        LookAt(targetPos);
        isMoving = true;
    }

    protected void LookAt(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        transform.eulerAngles.Set(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }


}
