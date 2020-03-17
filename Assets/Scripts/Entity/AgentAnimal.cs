using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimal : Agent
{
    public Camera playerCamera;

    public float timeToLive = 60f;//Seconds
    public float moveSpeed = 8f;
    public float sightRadius;//How far away the Agent can see
    public float maxHunger;//How many calories it need to consume before being full
    public float hungerSpeed;//How fast the hunger drop([0,1]/s)
    public float thirstSpeed;//How fast the thirst drop([0,1]/s)

    float thirst = 1f;//[0,1]
    float hunger = 1f;//[0,1]

    public float stopDistance = 1f;

    Vector3 targetPosition;
    bool isMoving = false;

    void Update()
    {
        thirst -= thirstSpeed * Time.deltaTime;
        hunger -= hungerSpeed * Time.deltaTime;
        timeToLive -= Time.deltaTime;

        //TODO: CauseOfDeath
        if(thirst <= 0f)
        {
            Die();
        }
        if (hunger <= 0f)
        {
            Die();
        }
        if(timeToLive <= 0f)
        {
            Die();
        }

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

    //TODO: CauseOfDeat
    protected void Die()
    {
        Destroy(gameObject);
    }

}
