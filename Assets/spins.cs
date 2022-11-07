using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spins : MonoBehaviour {

    public Text msgText;//用于更改转速

    public Text countText;

    public Text spinsText;

    public float speed=400.0f;//陀螺的初始角速度

    public AudioClip clip;

    bool r = false;

    public float speed1;

    int n;//转速

    int count = 5;//五次旋转机会

    Color color;

    float angles;

	// Use this for initialization
	void Start () {
        speed1 = speed;
        msgText.text = 0 + "/min";
        color = countText.color;
        color.a = 0f;
        countText.color = color;
        angles = 0;
        spinsText.text = "SPINS:" + angles;
   
	}
	
	// Update is called once per frame
	void Update () {
        //识别手指滑屏
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved)&&count==5)
        {
                Vector2 VecDeltaArea = Input.GetTouch(0).deltaPosition;
                transform.Rotate(0,0,VecDeltaArea.x);
        }            
        if (count > 0)
        {
            //手指离开屏幕    
             if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                speed1 += 400f;
                r = true;
                count--;
                countText.text = count + "";
                color.a = 1f;
                countText.color = color;
            }
            
                     
        }       
        if (r && speed1 != 0)
        {
            transform.Rotate(Vector3.forward *Time.deltaTime * speed1);            
            speed1 -= 2;  
            if(speed1>50&&speed1%100==0)
                AudioSource.PlayClipAtPoint(clip, Vector3.zero);          
        }
        n = (int)speed1 * 60 / 360;
        if (r) 
        {
            msgText.text = n +"/min";    
            angles += Time.deltaTime * speed1;
            spinsText.text = "SPINS:" + (int)angles / 360;
        }
        if (speed1 == 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 0);
            count = 5;
            countText.text = count + "";
            r = false;
            angles = 0;
            spinsText.text = "SPINS:" + angles;
        }
        if (!r||count==0)
        {
            color.a-=0.01f;
            countText.color = color;
        }        
    }
}
