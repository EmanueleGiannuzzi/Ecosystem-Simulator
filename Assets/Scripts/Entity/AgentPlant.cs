using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPlant : Agent
{
    public float growthSpeed;//[0,1]/s
    public float maxGrowthDelay;//How much remaining time it need to wait to grow after being eaten
    public float maxCalories;//How many calories the plant can provide when it's fully grown

    float growthValue = 0f;//How much plant is remaining to eat([0,1])
    float growthDelay = 0f;//How much remaining time it need to wait until growing again(seconds)


    void Update()
    {
        if(growthDelay <= 0f && growthValue < 1f)
        {
            Grow(growthSpeed * Time.deltaTime);
        }
        else
        {
            growthDelay -= Time.deltaTime;
        }
    }

    /** 
     * biteSize is expressed in calories
     * returns calories eaten
    **/
    public float EatCalories(float biteSize)
    {
        float amount = biteSize / maxCalories;//[0,1]
        float amountConsumed = Mathf.Max(0f, Mathf.Min(growthValue, amount));

        Grow(-amountConsumed);
        growthDelay = maxGrowthDelay;
        return amountConsumed * maxCalories;
    }

    public float GetCaloriesRemaing()
    {
        return maxCalories * growthValue;
    }

    private void Grow(float amount)
    {
        growthValue += amount;
        transform.localScale = Vector3.one * growthValue;
    }

}
