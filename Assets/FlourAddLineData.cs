using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System;

public class FlourAddLineData : MonoBehaviour
{
    private GraphChart lineChart;
    public EventArgs lineEventArgs;

    void Start()
    {
        lineChart = GetComponent<GraphChart>();
        if (lineChart != null)
        {
        }
    }

    public void Clear()
    {
        if (lineChart != null)
        {
            Debug.Log("Clearing lineChart");
            lineChart.DataSource.ClearCategory("line1");
        }
    }


    public void AddCategory(string cat)
    {
        if (lineChart != null)
        {
            Debug.Log("adding new lineChart category " + cat);
        }
    }


    public void SetValue(string category, float value, int datapointval)
    {
        if (lineChart != null)
        {
            lineChart.DataSource.StartBatch();
            lineChart.HorizontalValueToStringMap[datapointval] = DateTime.Parse(category).ToString();
            Debug.Log("Setting point at " + category + " to " + value);
            lineChart.DataSource.AddPointToCategory("line1", DateTime.Parse(category), value);
            lineChart.DataSource.EndBatch();
        }
    }

    //    public void pointClicked(GraphChart. args)
    //    {
    //        Debug.Log(args.Category);
    //    }
}
