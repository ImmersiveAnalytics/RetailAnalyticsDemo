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
using System.Collections.Generic;




namespace Vuforia
{
    public class FlourScript : MonoBehaviour,
                                                ITrackableEventHandler
    {
        public GameObject textInfo;
        private TrackableBehaviour mTrackableBehaviour;

        void Start()
        {
            textInfo.transform.Find("ProductNameText").GetComponent<Text>().text = "";
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
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
                OnTrackingLost();
            }
        }


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            foreach (Renderer component in rendererComponents)
            {
                // component.enabled = true;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
            if(textInfo.transform.Find("ProductNameText").GetComponent<Text>().text == "")
            {
                StartCoroutine(NewFilterProd("Product Name", "American Cole Slaw"));
            }
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " Found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }


/*
            textInfo.transform.Find("ProductNameText").GetComponent<Text>().text = "";
            textInfo.transform.Find("AffinitiesText").GetComponent<Text>().text = "";
            textInfo.transform.Find("SalesText").GetComponent<Text>().text = "";
            StartCoroutine(NewClearSelections());
            //GameObject.Find("ProductNameText").GetComponent<Text>().text = "";
            // GameObject.Find("AffinitiesText").GetComponent<Text>().text = "";
            // GameObject.Find("SalesText").GetComponent<Text>().text = "";
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");*/
        }

        IEnumerator NewFilterProd(string field, string val)
        {
            WWWForm form = new WWWForm();
            form.AddField("fieldName", field);
            form.AddField("fieldValue", val);

            UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/filter", form);
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("getting paths");
              StartCoroutine(NewProductReq());
                 Debug.Log("Form upload complete!");
            }
        }

        IEnumerator NewProductReq()
        {

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("fieldName=&fieldValue="));
            UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/listProducts", "foo");
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                  Debug.Log(www.error);
            }
           else
            {
                string response = www.downloadHandler.text;
                var parsedResponse = JSON.Parse(response);
                Debug.Log("PRINTING RESPONSE");

                string ProductName;
                //ProductName = "";

                //GameObject.Find("ProductNameText").GetComponent<Text>().text = ProductName;
                foreach (var key in parsedResponse.Keys)
                {
                    ProductName = key;
                    textInfo.transform.Find("ProductNameText").GetComponent<Text>().text = ProductName;
                    //GameObject.Find("ProductNameText").GetComponent<Text>().text = ProductName;
                    Debug.Log(key);
                }

                string Affinities;


                Affinities = "";

                var valarr = parsedResponse[0]["affinities"].Values;

                Debug.Log(valarr.ToString());
            
                foreach (var key in valarr)
                {
                    Affinities += key.ToString() + "\r\n";
                    Debug.Log(key);
                }

                textInfo.transform.Find("SalesText").GetComponent<Text>().text = "Amount Sold: " + parsedResponse[0]["sales"];
                textInfo.transform.Find("AffinitiesText").GetComponent<Text>().text = "Affinity Products: " + "\r\n" + Affinities;
                //GameObject.Find("SalesText").GetComponent<Text>().text = "Amount Sold: " + parsedResponse[0]["sales"];
                //GameObject.Find("AffinitiesText").GetComponent<Text>().text = "Affinity Products: " + "\r\n" + Affinities;

                //Debug.Log("PRODUCT NAME IS " + ProductName);
                Debug.Log("AFFINITIES" + Affinities);
                Debug.Log("SALES " + parsedResponse[0]["sales"]);

                Debug.Log("List of Products Selected: " + response);
            }
        }

        IEnumerator NewClearSelections()
        {
            UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/clear", "foo");
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error))
            {
                string s = www.downloadHandler.text;
                Debug.Log(s);
                Debug.Log("selection clear complete!");
                //yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("WWW Clear Selection Error: " + www.error + www.downloadHandler.text);
            }
        }

    }
}
