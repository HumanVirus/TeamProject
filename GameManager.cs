using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject obj_1;
    public GameObject obj_FX;
    private GameObject target;

    public Text scoretext;

    int random_num;
    int count = 0;
    private int i;
    private int j;
    //public Vector3 MousePosition;
    //private Camera cam;
    public int score = 0;
    public AudioSource audio;
    void Start()
    {
        //cam = Camera.main;
        random_num = Random.Range(1, 10);
        Debug.Log(random_num);
        float distance = 2.0f;
        for(i=0;i<3;i++)
        {
            for (j = 0; j < 3; j++)
            {
                count += 1;
                if (random_num == count)
                {
                    Instantiate(obj_1, new Vector3(i * distance - distance, j * distance - distance, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(obj, new Vector3(i * distance - distance, j * distance - distance, 0), Quaternion.identity);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    
        //MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Click");
            //Debug.Log("PositionX:" + MousePosition.x);
            //Debug.Log("PositionY:" + MousePosition.y);
            CastRay();
            if(target==this.gameObject)
            {
                Debug.Log("1");
            }
        }
    }

    void CastRay()
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            target =hit.collider.gameObject;
           

            if(hit.collider.name == "Block1(Clone)")
            {
         
                score += 100;
                Instantiate(obj_FX, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z),Quaternion.identity);
            }
            else
            {
                score -= 10;
                Instantiate(obj_FX, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), Quaternion.identity);
            }
            scoretext.text = "Score:"+score.ToString();
            Destroy(target);
            Debug.Log(score);

        }
    }
}
