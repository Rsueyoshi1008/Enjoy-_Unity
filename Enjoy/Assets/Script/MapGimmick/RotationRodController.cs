using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRodController : MonoBehaviour
{
    public float rotationSpeed; //回転速度
    public float forceAmount; //反発力
    private Transform parentTransform; //親オブジェクトの場所を取得
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent != null)
        {
            // 親オブジェクトを取得する
            parentTransform = transform.parent;
        }
        else
        {
            Debug.Log("親オブジェクトがありません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (parentTransform != null)
        {
            // 親オブジェクトの周りを回転する
            transform.RotateAround(parentTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    // 他のオブジェクトと衝突した時に呼び出される関数
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが"Player"タグを持っている場合
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        //     // 衝突したオブジェクトにRigidbodyがアタッチされているかチェック
        //     if (rb != null)
        //     {
        //         // 反力を加える方向を計算する
        //         Vector3 forceDirection = collision.contacts[0].point - transform.position;
        //         forceDirection = -forceDirection.normalized;

        //         // 反力を加える（Impulseモードで）
        //         rb.AddForce(100f,100f,100f);
        //     }
        // }
    }
}
