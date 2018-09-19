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
    public class RetailItemScript : MonoBehaviour, ITrackableEventHandler, IInputClickHandler, IInputHandler
    {

        public Text ProdNametext;
        public Text SalesText;
        public Text AffinityText;
        bool ShowLinechart = false;
        public GameObject myLinechart;
        private TrackableBehaviour mTrackableBehaviour;
        private float SalesAmount = 0;
        private string Affinities = "";
        private string ProductName = "";
        public RetailIndexStruct RetailIndex;

        public float GetSales()
        {
            return SalesAmount;
        }
        public string GetAffinities()
        {
            return Affinities;
        }
        public string GetProductName()
        {
            return ProductName;
        }
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            myLinechart.SetActive(false);
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            // AirTap code goes here

            if (ShowLinechart == false)
            {
                ShowLinechart = true;
                myLinechart.SetActive(true);
            }
            else
            {
                ShowLinechart = false;
                myLinechart.SetActive(false);
            }

        }
        public void OnInputDown(InputEventData eventData)
        {

        }
        public void OnInputUp(InputEventData eventData)
        {

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
                component.enabled = false;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            foreach (Canvas component in canvasComponents)
            {
                component.enabled = true;
            }

            //Debug.Log("X COORD IS " + mTrackableBehaviour.transform.position.x);
            //Debug.Log("Y COORD IS " + mTrackableBehaviour.transform.position.y);
            //Debug.Log("Z COORD IS " + mTrackableBehaviour.transform.position.z);

            if (ProdNametext.GetComponentInChildren<Text>().text == "")
            {
                StartCoroutine(NewFilterProd());
            }
            
        }

        public void filtProd()
        {
            StartCoroutine(NewFilterProd());
        }

        public void clearText()
        {
            ProdNametext.GetComponentInChildren<Text>().text = "";
            SalesText.GetComponentInChildren<Text>().text = "";
            AffinityText.GetComponentInChildren<Text>().text = "";
            ShowLinechart = false;
            myLinechart.SetActive(false);
        }

        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
            Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

            foreach (Renderer component in rendererComponents)
            {
                //component.enabled = false;
            }

            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }
            foreach (Canvas component in canvasComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        public void getLinechartData()
        {
            Debug.Log("getting linechart");
            //string url = "http://rdmobile.qlikemm.com:8083/getSalesDates";
            string url = "http://pe.qlik.com:8083/getSalesDates";
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

                AddLineData myLineScript = myLinechart.GetComponent<AddLineData>();

                myLineScript.Clear();

                var node = JSONNode.Parse(s);
                int count = 0 ;
                if (node.Tag == JSONNodeType.Object)
                {
                    foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)node)
                    {
                        myLineScript.AddCategory(kvp.Key);
                        myLineScript.SetValue(kvp.Key, float.Parse(kvp.Value["sales"].Value), count);
                        count++;
                    }
                }                
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }


        IEnumerator NewFilterProd()
        {
           
            WWWForm form = new WWWForm();

            form.AddField("fieldName", "Product Name");
            form.AddField("fieldValue", RetailIndex.getRetailItemValue(this.gameObject.name.ToString()));

            //UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/filter", form);
            UnityWebRequest www = UnityWebRequest.Post("http://pe.qlik.com:8083/filter", form);
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
            //UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/listProducts", "ff");
            UnityWebRequest www = UnityWebRequest.Post("http://pe.qlik.com:8083/listProducts", "ff");
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

                foreach (var key in parsedResponse.Keys)
                {
                    ProductName = key;
                    ProdNametext.GetComponentInChildren<Text>().text = ProductName;
                }

                var valarr = parsedResponse[0]["affinities"].Values;
                foreach (var key in valarr)
                {
                    Affinities += key.ToString() + "\r\n";
                    Debug.Log(key);
                }
                SalesAmount = parsedResponse[0]["inventory"];
                SalesText.GetComponentInChildren<Text>().text = "Inventory amount: " + parsedResponse[0]["inventory"];
                AffinityText.GetComponentInChildren<Text>().text = "Affinity Products: " + "\r\n" + Affinities;
                getLinechartData();
            }
        }

        IEnumerator NewClearSelections()
        {
            //UnityWebRequest www = UnityWebRequest.Post("http://rdmobile.qlikemm.com:8083/clear", "foo");
            UnityWebRequest www = UnityWebRequest.Post("http://pe.qlik.com:8083/clear", "foo");
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            if (!string.IsNullOrEmpty(www.error))
            {
                string s = www.downloadHandler.text;
                Debug.Log(s);
                Debug.Log("selection clear complete!");
            }
            else
            {
                Debug.Log("WWW Clear Selection Error: " + www.error + www.downloadHandler.text);
            }
        }
    }
}
