using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using HoloToolkit.Unity;
using System.Collections.Generic;
using ChartAndGraph;

public class SenseConnector : MonoBehaviour
{

    public Sankey mySankey;
    public GameObject myBarchart;
    public GameObject cursorForHover;
    public GameObject myPiechart;
    public GameObject myLinechart;
    public Material myChartMaterial;
    public Material[] myPieMaterials;

    void Awake()
    {
        getPaths();
    }

    void Start()
    {

    }
    void Update()
    {

    }

    public void getPaths()
    {
        Debug.Log("getting paths");
        string url = "http://rdmobile.qlikemm.com:8083/listProducts";
        WWWForm form = new WWWForm();
        form.AddField("field", "val");
        WWW www = new WWW(url, form);
        StartCoroutine(PathRequest(www));
    }

    IEnumerator PathRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            string s = www.text;
            //mySankey.NewData(s);
           // getBarchart();
         //   getPiechart();
          //  getLinechart();
         //   getFields();
         //   getText(s);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void getBarchart()
    {
        Debug.Log("getting barchart");

        AddBarData myBarScript = myBarchart.GetComponent<AddBarData>();
        myBarScript.Clear();
        

        string url = "http://pe.qlik.com:8082/listBars";
        WWWForm form = new WWWForm();
        form.AddField("field", "val");
        WWW www = new WWW(url, form);
        StartCoroutine(BarchartRequest(www));
    }
    IEnumerator BarchartRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            string s = www.text;

            AddBarData myBarScript = myBarchart.GetComponent<AddBarData>();
            myBarScript.Clear();


            JSONNode JBars = JSON.Parse(s);
            for (int i = 0; i < JBars.AsArray.Count; i++)
            {
                string b = JBars[i].ToString();
                string bar = b.Substring(1, b.Length - 2);
                string[] kvp = bar.Split(':');
                string key = kvp[0].Substring(1, kvp[0].Length - 2);
                string val = kvp[1];


                myBarScript.AddCategory(key, myChartMaterial);
                myBarScript.SetValue(key, float.Parse(val));
                Debug.Log("bar: " + key + ":" + val);
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    public void barClicked(BarChart.BarEventArgs args)
    {
        selectBar("Admission Type", args.Category);
    }
    public void barHovered(BarChart.BarEventArgs args)
    {
        cursorForHover.SetActive(true);
        Text hoverText = cursorForHover.GetComponentInChildren<Text>();
        hoverText.text = args.Category;
        Debug.Log("bar hovered " + args.Category);
    }
    public void barHoverExit()
    {
        cursorForHover.SetActive(false);
    }

    public void getPiechart()
    {
        Debug.Log("getting piechart");

        string url = "http://pe.qlik.com:8082/listPies";
        WWWForm form = new WWWForm();
        form.AddField("field", "val");
        WWW www = new WWW(url, form);
        StartCoroutine(PiechartRequest(www));
    }

    IEnumerator PiechartRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            string s = www.text;

           // AddPieData myPieScript = myPiechart.GetComponent<AddPieData>();
            //myPieScript.Clear();


            JSONNode JPies = JSON.Parse(s);
            for (int i = 0; i < JPies.AsArray.Count; i++)
            {
                string p = JPies[i].ToString();
                string pie = p.Substring(1, p.Length - 2);
                string[] kvp = pie.Split(':');
                string key = kvp[0].Substring(1, kvp[0].Length - 2);
                string val = kvp[1].Substring(1, kvp[1].Length - 2);

                //myPieScript.AddCategory(key, myPieMaterials[i]);
               // myPieScript.SetValue(key, float.Parse(val));
                Debug.Log("pie: " + key + ":" + val);
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void pieClicked(PieChart.PieEventArgs args)
    {
        //        selectPie("AbWVpk", args.Category);
        selectPie("Age Group", args.Category);
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

                myLineScript.SetValue(key, float.Parse(val),i);
                Debug.Log("line: " + key + ":" + val);
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }




    public void getFields()
    {
        Debug.Log("getting fields");

        string url = "http://pe.qlik.com:8082/getFieldStates";
        WWWForm form = new WWWForm();
        form.AddField("field", "val");
        WWW www = new WWW(url, form);
        StartCoroutine(FieldRequest(www));
    }

    IEnumerator FieldRequest(WWW www)
    {
        //        UnityWebRequest wwwF = UnityWebRequest.Get(url);
        //        yield return wwwF.Send();
        yield return www;

        // check for errors
        if (www.error == null)
        {
            string s = www.text;
            mySankey.updateUI(s);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void clear()
    {
        Debug.Log("clearing selections");
        string url = "http://pe.qlik.com:8082/clear";
        WWWForm form = new WWWForm();
        form.AddField("field", "val");
        WWW www = new WWW(url, form);

        StartCoroutine(ClearSelections(www));
    }

    public void selectBar(string cat, string val)
    {
        Debug.Log("selecting " + val + " from " + cat + " BarChart");
        StartCoroutine(FilterSelect(cat, val));
    }
    public void selectPie(string cat, string val)
    {
        Debug.Log("selecting " + val + " from " + cat + " PieChart");
        StartCoroutine(FilterSelect(cat, val));
    }


    IEnumerator FilterSelect(string field, string val)
    {
        WWWForm form = new WWWForm();
        form.AddField("fieldName", field);
        form.AddField("fieldValue", val);

        //        UnityWebRequest www = UnityWebRequest.Post("http://pe.qlik.com:8082/filter", form);
        WWW www = new WWW("http://pe.qlik.com:8082/filter", form);
        //        yield return www.Send();
        yield return www;

        //        if (www.isError)
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {
            //            string s = www.downloadHandler.text;
            string s = www.text;
            s = s.Replace(',', '\n');
            Debug.Log(s);
            Debug.Log("selection POST complete!");

            yield return new WaitForSeconds(1);
            getPaths();
        }
    }

    IEnumerator ClearSelections(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            string s = www.text;
            Debug.Log(s);
            Debug.Log("selection clear complete!");

            yield return new WaitForSeconds(1);
            mySankey.clearUI();
            getPaths();
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
