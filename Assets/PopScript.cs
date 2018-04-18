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
    public class PopScript : MonoBehaviour, ITrackableEventHandler, IInputClickHandler, IInputHandler
    {
       // public GameObject textInfo;
        public GameObject myLinechart;
        private TrackableBehaviour mTrackableBehaviour;
        private string RetailItemIdentifier = "pop";


        // private string RetailProductName = "blabla";

        void Start()
        {
            //textInfo.transform.Find(RetailItemIdentifier + "ProductNameText").GetComponent<Text>().text = "";
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            // AirTap code goes here
            Debug.Log("FLOUR IS CLICKED");
        }
        public void OnInputDown(InputEventData eventData)
        { }
        public void OnInputUp(InputEventData eventData)
        { }

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

            //mTrackableBehaviour.transform.position.Set(0,0,1);
            //textInfo.transform.Find(RetailItemIdentifier + "ProductNameText").GetComponent<Text>().transform.SetPositionAndRotation(mTrackableBehaviour.transform.position, mTrackableBehaviour.transform.rotation);

            // if (textInfo.transform.Find(RetailItemIdentifier + "ProductNameText").GetComponent<Text>().text == "")
            //  {
            //StartCoroutine(NewFilterProd("Product Name", "American Cole Slaw"));
               // getFlourLinechart();
          //  }



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


            /*
                        textInfo.transform.Find("ProductNameText").GetComponent<Text>().text = "";
                        textInfo.transform.Find("AffinitiesText").GetComponent<Text>().text = "";
                        textInfo.transform.Find("SalesText").GetComponent<Text>().text = "";
                        StartCoroutine(NewClearSelections());
                        //GameObject.Find("ProductNameText").GetComponent<Text>().text = "";
                        // GameObject.Find("AffinitiesText").GetComponent<Text>().text = "";
                        // GameObject.Find("SalesText").GetComponent<Text>().text = "";*/

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        public void getFlourLinechart()
        {
            Debug.Log("getting linechart");
            string url = "http://pe.qlik.com:8082/listLines";
            WWWForm form = new WWWForm();
            form.AddField("field", "val");
            WWW www = new WWW(url, form);
            StartCoroutine(LinechartRequest(www));
        }



        IEnumerator LinechartRequest(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                string s = www.text;

                FlourAddLineData myLineScript = myLinechart.GetComponent<FlourAddLineData>();
                myLineScript.Clear();

                JSONNode JLines = JSON.Parse(s);
                for (int i = 0; i < JLines.AsArray.Count; i++)
                {
                    string l = JLines[i].ToString();
                    string line = l.Substring(1, l.Length - 2);
                    string[] kvp = line.Split(':');
                    string key = kvp[0].Substring(1, kvp[0].Length - 2);
                    string val = kvp[1];

                    myLineScript.AddCategory(key);
                    myLineScript.SetValue(key, float.Parse(val), i);
                    Debug.Log("line: " + key + ":" + val);
                }
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
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

        public void getLinechart()
        {
            Debug.Log("getting linechart");

            FlourAddLineData myLineScript = myLinechart.GetComponent<FlourAddLineData>();
            myLineScript.Clear();

            string url = "http://pe.qlik.com:8082/listLines";
            WWWForm form = new WWWForm();
            form.AddField("field", "val");
            WWW www = new WWW(url, form);
            StartCoroutine(LinechartRequest(www));
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
               //     textInfo.transform.Find(RetailItemIdentifier + "ProductNameText").GetComponent<Text>().text = ProductName;
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

              //  textInfo.transform.Find(RetailItemIdentifier + "SalesText").GetComponent<Text>().text = "Amount Sold: " + parsedResponse[0]["sales"];
              //  textInfo.transform.Find(RetailItemIdentifier + "AffinitiesText").GetComponent<Text>().text = "Affinity Products: " + "\r\n" + Affinities;
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
