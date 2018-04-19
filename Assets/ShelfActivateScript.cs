/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;




namespace Vuforia
{
    public class ShelfActivateScript : MonoBehaviour, ITrackableEventHandler, IInputClickHandler, IInputHandler
    {


        private TrackableBehaviour mTrackableBehaviour;
        public GameObject Rack;
        bool initrack = false;

        // private string RetailProductName = "blabla";
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnInputClicked(InputClickedEventData eventData){
        }
        public void OnInputDown(InputEventData eventData){
        }
        public void OnInputUp(InputEventData eventData){
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                // OnTrackingLost();
            }

        }


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
            Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            foreach (Canvas component in canvasComponents)
            {
                component.enabled = true;
            }

            Debug.Log("X COORD IS " + mTrackableBehaviour.transform.position.x);
            Debug.Log("Y COORD IS " + mTrackableBehaviour.transform.position.y);
            Debug.Log("Z COORD IS " + mTrackableBehaviour.transform.position.z);

            if(initrack == false)
            {
                //initrack = true;
                var fwd = Camera.main.transform.up;
                Rack.transform.SetPositionAndRotation(mTrackableBehaviour.transform.position, Quaternion.LookRotation(fwd));
            }
            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " Found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
            Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }
            foreach (Canvas component in canvasComponents)
            {
                component.enabled = false;
            }
        }



 

    }
}
