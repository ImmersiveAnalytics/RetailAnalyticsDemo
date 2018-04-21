using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System;

public class AddBarData : MonoBehaviour
{
    public BarChart barChart;
    public EventArgs barEventArgs;

    void Start()
    {
        barChart = GetComponent<BarChart>();
        if (barChart != null)
        {
}
    }

    public void Clear()
    {
        if (barChart != null)
        {
            Debug.Log("Clearing barchart");
            barChart.DataSource.ClearCategories();
        }
    }

    public int getAmountOfCategories()
    {
        return barChart.DataSource.TotalCategories;
    }

    public void AddCategory(string cat, Material mat)
    {
        if (barChart != null)
        {
            bool shouldsetcat = true;
            Debug.Log("adding new barchart category " + cat);
            barChart.DataSource.AutomaticMaxValue = true;

            for(int i = 0; i < barChart.DataSource.TotalCategories; i++ )
            {
                if (barChart.DataSource.GetCategoryName(i) == cat)
                {
                    shouldsetcat = false;
                }
            }
            if(shouldsetcat == true)
            {
                barChart.DataSource.AddCategory(cat, mat);
            }

        }
    }

    public void RemoveCategory(string cat)
    {
        if (barChart != null)
        {
            bool shouldremovecat = false;
            Debug.Log("adding new barchart category " + cat);
            for (int i = 0; i < barChart.DataSource.TotalCategories; i++)
            {
                if (barChart.DataSource.GetCategoryName(i) == cat)
                {
                    shouldremovecat = true;
                }
            }
            if (shouldremovecat == true)
            {
                barChart.DataSource.RemoveCategory(cat);
            }
        }
    }


    public void SetValue(string category, float value)
    {
        if (barChart != null)
        {
            //barChart.DataSource.StartBatch();
            Debug.Log("Setting category to " + category + " and DimensionVal1 to " + value);
            //barChart.DataSource.SetValue(category, "DimensionVal1", value);
            barChart.DataSource.SlideValue(category, "DimensionVal1", value, 0.5f);
            //barChart.DataSource.EndBatch();
            //barChart.DataSource.EndBatch();
            //barChart.DataSource.MaxValue = 100000;
           // barChart.DataSource.MinValue = 0;
        }
    }

    public void barClicked(BarChart.BarEventArgs args)
    {
        Debug.Log(args.Category);
    }
}
