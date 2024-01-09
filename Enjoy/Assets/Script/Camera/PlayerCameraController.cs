using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    public float RotationLimits = 100.0f; // 回れる距離
    private float horizontalityAngle = 0.0f;  //水平のカメラ角度
    //カメラの軸になるオブジェクト
    [SerializeField] private Transform target;
    public new Vector3 distance; //カメラとPlayerの距離
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // //マウスのX座標の移動量を取得
        //float horizontal = Input.GetAxis("Mouse X") * sensitivity;

        // //マウスのY座標の移動量を取得
        // float vertical = Input.GetAxis("Mouse Y") * sensitivity;

        var inputRightStick = Gamepad.current.rightStick.ReadValue() * RotationLimits;
        //Debug.Log(inputRightStick);
        Quaternion horizontalityRotation = Quaternion.Euler(-60f, inputRightStick.x, 0f);
        
        //上下の視点操作を無効にした
        Vector3 position = horizontalityRotation * distance;

        // ターゲットへのベクトルを求める
        Vector3 directionToTarget = target.position - position;

        // カメラとターゲットの距離が最大距離を超えている場合、カメラの位置を調整
        if (directionToTarget.magnitude > maxDistance) {
            directionToTarget = directionToTarget.normalized * maxDistance;
            position = target.position - directionToTarget;
        }
        // カメラの位置を更新
        transform.position = position;
        // カメラがターゲットを注目する
        transform.LookAt(target);
    }
}
