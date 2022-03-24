using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ItemAndWeights
{
    public int Item = 0;
    public int Weight = 0;

    public ItemAndWeights(int n, int w)
    {
        this.Item = n;
        this.Weight = w;
    }
}

public static class Utilities
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    private static int getTotalWeight(List<ItemAndWeights> brokers)
    {
        int totalWeight = 0;
        foreach (ItemAndWeights broker in brokers)
        {
            totalWeight += broker.Weight;
        }
        return totalWeight;
    }

    public static int RandomElementByWeight(List<ItemAndWeights> brokers)
    {
        System.Random _rnd = new System.Random();

        int totalWeight = getTotalWeight(brokers);
        int randomNumber = _rnd.Next(0, totalWeight);

        ItemAndWeights selectedBroker = null;
        foreach (ItemAndWeights broker in brokers)
        {
            if (randomNumber <= broker.Weight)
            {
                selectedBroker = broker;
                break;
            }

            randomNumber = randomNumber - broker.Weight;
        }

        return selectedBroker.Item;
    }
}