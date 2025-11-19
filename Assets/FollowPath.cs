using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FollowPath : MonoBehaviour
{

    //Input Parameters
    [SerializeField]
    GameObject path;
    [SerializeField]
    float linear_vel;
    
    [SerializeField]
    float rotational_vel;
    [SerializeField]
    float min_distance_to_change_of_objective;
    [SerializeField]
    float wait_seconds;

    //Control variables
    private Coroutine coroutine_ofchange;
    
    void Awake(){
        
        path.transform.position = new Vector3(path.transform.position.x, transform.position.y, path.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if((path.transform.position - transform.position).magnitude > min_distance_to_change_of_objective ){
            moveTowardsObjective();
        }else{
            if(coroutine_ofchange == null)
            {
                coroutine_ofchange = StartCoroutine(Change_Objective());
            }
            if(path.name == "END" || path.transform.childCount == 0)
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }
        

    }
    void moveTowardsObjective(){
        Quaternion relative_rotation = Quaternion.LookRotation(path.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, relative_rotation, rotational_vel* Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * linear_vel;
    }
    public IEnumerator Change_Objective(){
        //Wait for a simple wait
        
        yield return new WaitForSeconds(wait_seconds);
        
        //Check all different posibilities to continue
        List<KeyValuePair<float,GameObject>> adyacencies = new List<KeyValuePair<float, GameObject>>() ;
        //Create the sum of all posibilities
        float Big_posibility = 0;
        foreach (Transform child in path.transform){
            //if no posibility by default set it to 1
            float posibility = child.gameObject.GetComponent<Data>()?.probability() ?? 1;
            Debug.Log(posibility);
            Big_posibility += posibility;
            adyacencies.Add(new KeyValuePair<float, GameObject>(posibility, child.gameObject));
        }
        float choice = Random.Range(0,1f) * Big_posibility;
        float currentProb = 0;
        int selected_index_path = 0;
        //Iterate over the algorithm to determine the first one that passes the threshold of choice
        for(; selected_index_path < adyacencies.Count; selected_index_path++){
            currentProb += adyacencies[selected_index_path].Key;
            if(currentProb > choice)
            {
                break;
            }
        }
        //Set the selected object as objective
        path = adyacencies[selected_index_path].Value;
        Debug.Log(path.name);
        coroutine_ofchange = null;
    }
}
