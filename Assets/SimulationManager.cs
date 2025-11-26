using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    List<FollowPath> car_agents = new List<FollowPath>();
    
    int i =11; int j = 0; 
     //Control variables
    private Coroutine coroutine_ofchange = null;

    // Update is called once per frame
    void Start()
    {
        foreach(FollowPath car in car_agents)
        {
            car.path = transform.GetChild(2).GetChild(3);
        }
    }
    void Update()
    {
        if(coroutine_ofchange == null)
        {
            coroutine_ofchange = StartCoroutine(changeCoord());
        }
        foreach(FollowPath car in car_agents)
        {
            car.path = transform.GetChild(i).GetChild(j);
        }
    }
    private IEnumerator changeCoord()
    {
        yield return new WaitForSeconds(1f);
        j++; 
        j %= transform.GetChild(0).childCount;
        coroutine_ofchange = null;
    }
}
