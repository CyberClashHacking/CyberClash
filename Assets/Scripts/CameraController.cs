//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private float smoothing = 0.2f;
//    private Vector3 playerPos;

//    private void LateUpdate()
//    {
//        playerPos = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, 
//            transform.position.z);
//    }

//    private void FixedUpdate()
//    {
//        transform.position = Vector3.Lerp(transform.position, playerPos, smoothing);
//    }
//}
