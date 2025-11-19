using UnityEngine;

public class Data : MonoBehaviour
{

    [SerializeField]
    float posibility_to_be_choosen = 0.15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float probability(){
        return posibility_to_be_choosen;
    }
}
