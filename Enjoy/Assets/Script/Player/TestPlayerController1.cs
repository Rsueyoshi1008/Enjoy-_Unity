using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController1 : MonoBehaviour
{
    private Rigidbody rb;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput =Input.GetAxis("horizontal1");
        float velocityInput = Input.GetAxis("vertical1");
        if(horizontalInput != 0.0f || velocityInput != 0.0f)
        {
            float speed = 5.0f;
            
            var input = new Vector3(horizontalInput, 0f, velocityInput);
            // // カメラの方向から、X-Z平面の単位ベクトルを取得
            // Vector3 cameraForward = mainCamera.transform.forward;
            // Vector3 cameraRight = mainCamera.transform.right;
            input = input.normalized;
            // // Y軸成分を無視する
            // cameraForward.y = 0f;
            // cameraRight.y = 0f;
            // 方向キーの入力値とカメラの向きから、移動方向を決定
            // Vector3 velocity = input;
            // //移動ベクトルを正規化する
            // velocity = velocity.normalized;
            //  走った場合と歩いた場合
            //float speed = runFlag ? _model.RunSpeed : _model.WalkSpeed;
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = input * speed + new Vector3(0, rb.velocity.y, 0);
        }
        //Debug.Log(rb.velocity);
    }
}
