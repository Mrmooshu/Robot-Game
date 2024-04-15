using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class FarmingData : ISerializationCallbackReceiver
{
    
    public Dictionary<string, PlotData> plots;

    [SerializeField]private List<UniversalHelperFunctions.SavedData<PlotData>> plotlist;

    [NonSerialized]public PlotData currentPlot;

    public FarmingData()
    {
        if (plots == null)
        {
            plots = new Dictionary<string, PlotData>();
        }
    }

    [System.Serializable]
    public class PlotData
    {
        [System.Serializable]
        public enum PlotType
        {
            tree,flower,vegetable,fruit
        }

        public PlotData(PlotType type)
        {
            this.type = type;
        }

        public PlotType type { get; private set; }
        public int currentstage = 0;
        public float stagedurationcountdown = 0;
        public Seed currentseed = null;
    }

    public void OnAfterDeserialize()
    {
        if (plotlist != null)
        {
            plots = plotlist.ToDictionary(x => x.name, x => x.data);
        }
    }

    public void OnBeforeSerialize()
    {
        try
        {
            if (plots != null)
            {
                plotlist = plots.Select(x => new UniversalHelperFunctions.SavedData<PlotData>(x.Key, x.Value)).ToList();
            }
        }
        catch (Exception)
        {

        }
    }
}
