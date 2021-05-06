using UnityEngine;
using System;
using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;

namespace OculusSampleFramework
{

    [RequireComponent(typeof(Rigidbody))]
    public class interactionCollider2 : MonoBehaviour
    {
        private Vector3 _initPosition;
        private Quaternion _initRotation;
        [SerializeField] private ButtonController _resetbutton;
        private Rigidbody _rigidBody;
        [SerializeField] OVRHand _rightHand;// = HandsManager.Instance.RightHand;
        [SerializeField] OVRHand _leftHand;// = HandsManager.Instance.LeftHand;
        private int hitting = 0;

        List<Vector3> pos = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {
            _initPosition = this.transform.position;
            _initRotation = this.transform.rotation;
            _rigidBody = GetComponent<Rigidbody>();
            resetVelocity();
            _resetbutton.ActionZoneEvent += args =>
            {
                resetVelocity();
                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponent<Rigidbody>().freezeRotation = false;
                this.transform.SetPositionAndRotation(_initPosition, _initRotation);

            };
            for (int i = 0; i < 10; i++)
            {
                pos.Add(Vector3.zero);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(isPinchingHand(_rightHand).isPinching && hitting == 1)
            {
                _rigidBody.useGravity = false;
                _rigidBody.freezeRotation = true;
                this.transform.position = HandsManager.Instance.RightHand.PointerPose.position;
            }
            else if(isPinchingHand(_leftHand).isPinching && hitting == 2)
            {
                _rigidBody.useGravity = false;
                _rigidBody.freezeRotation = true;
                this.transform.position = HandsManager.Instance.LeftHand.PointerPose.position;
            }
            else
            {

                _rigidBody.useGravity = true;
                _rigidBody.freezeRotation = false;
            }
        }

        private void resetlist()
        {
            for (int i = 0; i < 10; i++)
            {
                pos.RemoveAt(0);
                pos.Add(Vector3.zero);
            }
        }
        private void resetVelocity()
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        /// <summary>
        /// 掴もうとしているか？
        /// </summary>
        /// <returns></returns>
        private (bool isPinching, Vector3 position) isPinchingHand(OVRHand hand)
        {
            Vector3 position = Vector3.zero;
            bool isPinching = false;

            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index)
                || hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)
                || hand.GetFingerIsPinching(OVRHand.HandFinger.Ring))
            {
                position = hand.PointerPose.position;
                isPinching = true;
            }

            return (isPinching, position);
        }

        /// <summary>
        /// 当たった方の手の取得。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private (OVRHand hand, string handName) getCollisionHand(Collision other)
        {
            try
            {
                //親子関係 OVRHandPrefab/Capsules/Hand_Index1_***
                GameObject targetObject = other.transform.parent.parent.gameObject;
                OVRHand rightHand = HandsManager.Instance.RightHand;
                OVRHand leftHand = HandsManager.Instance.LeftHand;
                if (targetObject.Equals(leftHand.gameObject)) return (leftHand, "LeftHand");
                if (targetObject.Equals(rightHand.gameObject)) return (rightHand, "RightHand");
                return (null, "None");
            }
            catch (Exception e)
            {
                //parentが無かった時のエラーをキャッチ
                return (null, "None");
            }
        }

        /// <summary>
        /// 触れた時
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            var collisionHand = getCollisionHand(other);
            if (collisionHand.hand == null)
            {
                return;
            }
            else if(collisionHand.handName == "RightHand")
            {
              
                hitting = 1;
            }
            else if (collisionHand.handName == "LeftHand")
            {
                hitting = 2;
            }
            else
            {
                _rigidBody.useGravity = true;
                _rigidBody.freezeRotation = false;
            }

            /*var result = isPinchingHand(collisionHand.hand);
            if (!result.isPinching) return;
            _rigidBody.useGravity = false;
            _rigidBody.freezeRotation = true;
            this.transform.position = result.position;*/
        }

        /// <summary>
        /// 触れている間
        /// </summary>
        /// <param name="other"></param>
        /*private void OnCollisionStay(Collision other)
        {
            var collisionHand = getCollisionHand(other);
            if (collisionHand.hand == null) return;

            var result = isPinchingHand(collisionHand.hand);
            if (result.isPinching)
            {
                pos.RemoveAt(0);
                pos.Add(this.transform.position);
                resetVelocity();
                this.transform.position = result.position;
            }
            else
            {
                _rigidBody.useGravity = true;
            }
        }*/

        /// <summary>
        /// 離れた時
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionExit(Collision other)
        {
            var collisionHand = getCollisionHand(other);
            if (collisionHand.hand == null)
            {
                return;
            }
            //hitting = 0;
        }
    }
}