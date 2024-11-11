using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class UtilitiesHolder : MonoBehaviour
{
    public static UtilitiesHolder Instance;

    private Dictionary<string, IUtility> container = new Dictionary<string, IUtility>();


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        LoadUtilities();
    }
    private void LoadUtilities()
    { 
        var utilityTypes = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(t => typeof(IUtility).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);


        foreach (var utilityType in utilityTypes)
        {
            var utilityInstance = (IUtility)Activator.CreateInstance(utilityType);

            container.Add(utilityType.Name, utilityInstance);
        }
    }

    public T GetUtility<T>()
        where T : IUtility
    {

        string name = typeof(T).Name;

        if(container.ContainsKey(name))
        {
            return (T)container[name];
        }
        else
        {
            Debug.LogError($"No Utility Found Under The Name Of {name}");
            return default(T);
        }
    }
}
