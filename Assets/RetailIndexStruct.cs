using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class RetailIndexStruct : MonoBehaviour
    {

    static Dictionary<string, string> RetailItems = new Dictionary<string, string>()
    {
        { "KellogsTarget", "Kellogg's Raisin Bran Breakfast Cereal, Real Raisins, 18.7 Oz" },
        { "BountyTarget", "Bounty Paper Towels, Select-A-Size, 2 Bulk Rolls" },
        { "OreoTarget", "Nabisco Oreo Chocolate Sandwich Cookies, 0.78 oz, 18 ct" },
        { "PeanutTarget", "Planters Dry Roasted Peanuts Party Size, 34.5 oz (978 g)" },
        { "GranolabarTarget", "Quaker Chewy Granola Bars, 25% Less Sugar, Family Size Variety Pack, 24 Bars" },
        { "FlourTarget", "Gold Medal Organic All-Purpose Flour, 5 lb" },
        { "NesquickTarget", "NESTLE NESQUIK Chocolate Flavored Powder 9.3 oz. Canister" },
        { "HersheyTarget", "HERSHEY'S Chocolate Syrup, 48 Ounces" },
        { "HeinzTarget", "Heinz Tomato Ketchup Bottle, 64 oz" },
        { "CottonelleTarget", "Cottonelle Clean Care Toilet Paper, 12 Double Rolls" },
        { "FebrezeTarget", "Tide Plus Febreze Freshness Spring And Renewal Scent HE Turbo Clean Liquid Laundry Detergent, 37 oz, 24 loads" },
        { "FabsoftTarget", "Downy Liquid Fabric Softener, Concentrated, April Fresh, 51oz Bottle" },
        { "KleenexTarget", "Kleenex Ultra Soft Facial Tissue, White, Flat Box, 70 Sheets, 6 Ct" },
        { "CoffeeTarget", "Coffee-Mate Powder Original, 56 oz (2 Pack)" },
        { "RitzTarget", "Ritz Original Crackers, 10.3 Ounce" },
        { "PopcornTarget", "Gold Medal Organic All-Purpose Flour, 5 lb" }
    };

    public string getRetailItemValue(string targetname)
    {
        return RetailItems[targetname];
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


