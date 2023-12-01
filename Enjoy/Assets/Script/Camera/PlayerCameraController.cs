using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float sensitivity = 2.0f; // マウス感度
    private float horizontalityAngle = 0.0f;  //水平のカメラ角度
    //カメラの軸になるオブジェクト
    [SerializeField] private Transform target;
    public new Vector3 distance; //カメラとPlayerの距離
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マウスのX座標の移動量を取得
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;

        //マウスのY座標の移動量を取得
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;

        horizontalityAngle += horizontal;
        Quaternion horizontalityRotation = Quaternion.Euler(-60f, horizontalityAngle, 0f);

        //上下の視点操作を無効にした
        Vector3 position = horizontalityRotation * distance;

        // カメラの位置を更新
        transform.position = position;
        // カメラがターゲットを注目する
        transform.LookAt(target);
        
        
    }
}
