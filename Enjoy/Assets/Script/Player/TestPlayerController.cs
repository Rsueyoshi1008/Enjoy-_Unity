using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera mainCamera;
    public float speed;
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput =Input.GetAxis("horizontal1");
        float velocityInput = Input.GetAxis("vertical1");
        

        if(horizontalInput != 0.0f || velocityInput != 0.0f)
        {
            var input = new Vector3(horizontalInput, 0f, velocityInput);
            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            // Y軸成分を無視する
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            //方向キーの入力値とカメラの向きから、移動方向を決定
            Vector3 velocity = cameraForward.normalized * input.z + cameraRight.normalized * input.x;

            //  走った場合と歩いた場合
            //float speed = runFlag ? _model.RunSpeed : _model.WalkSpeed;

            rb.velocity = velocity * speed + new Vector3(0, rb.velocity.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);
            //Debug.Log(transform.Translate(input * Time.deltaTime));
        }
        
        
        
    }
}
