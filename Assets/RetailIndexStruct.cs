using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class RetailIndexStruct : MonoBehaviour
    {

    static Dictionary<string, string> RetailItems = new Dictionary<string, string>()
    {
//        { "KellogsTarget", "Kellogg's Raisin Bran Breakfast Cereal, Real Raisins, 18.7 Oz" },
        { "KellogsTarget", "Raisin Bran" },
//        { "BountyTarget", "Bounty Paper Towels, Select-A-Size, 2 Bulk Rolls" },
        { "BountyTarget", "Paper Towels" },
//        { "OreoTarget", "Nabisco Oreo Chocolate Sandwich Cookies, 0.78 oz, 18 ct" },
        { "OreoTarget", "Oreos" },
//        { "PeanutTarget", "Planters Dry Roasted Peanuts Party Size, 34.5 oz (978 g)" },
        { "PeanutTarget", "Peanuts" },
//        { "GranolabarTarget", "Quaker Chewy Granola Bars, 25% Less Sugar, Family Size Variety Pack, 24 Bars" },
        { "GranolaTarget", "Granola Bars" },
        { "FlourTarget", "Gold Medal Organic All-Purpose Flour, 5 lb" },
//        { "NesquickTarget", "NESTLE NESQUIK Chocolate Flavored Powder 9.3 oz. Canister" },
        { "NesquickTarget", "Chocolate" },
//        { "HersheyTarget", "HERSHEY'S Chocolate Syrup, 48 Ounces" },
        { "HersheyTarget", "Chocolate Syrup" },
//        { "HeinzTarget", "Heinz Tomato Ketchup Bottle, 64 oz" },
        { "HeinzTarget", "Ketchup" },
//        { "CottonelleTarget", "Cottonelle Clean Care Toilet Paper, 12 Double Rolls" },
        { "CottonelleTarget", "Toilet Paper" },
//        { "FebrezeTarget", "Tide Plus Febreze Freshness Spring And Renewal Scent HE Turbo Clean Liquid Laundry Detergent, 37 oz, 24 loads" },
        { "FebrezeTarget", "Laundry Detergent" },
//        { "FabsoftTarget", "Downy Liquid Fabric Softener, Concentrated, April Fresh, 51oz Bottle" },
        { "FabsoftTarget", "Fabric Softener" },
//        { "KleenexTarget", "Kleenex Ultra Soft Facial Tissue, White, Flat Box, 70 Sheets, 6 Ct" },
        { "KleenexTarget", "Tissue Paper" }, 
//        { "CoffeeTarget", "Coffee-Mate Powder Original, 56 oz (2 Pack)" },
        { "CoffeeTarget", "Coffee" },
//        { "RitzTarget", "Ritz Original Crackers, 10.3 Ounce" },
        { "RitzTarget", "Crackers" },
//        { "PopcornTarget", "Gold Medal Organic All-Purpose Flour, 5 lb" }
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


