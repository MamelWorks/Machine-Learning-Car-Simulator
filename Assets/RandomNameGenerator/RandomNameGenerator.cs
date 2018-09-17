using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomNameGenerator : MonoBehaviour
{

    private List<String> _names;
    public Dictionary<string, long[]> Colors;
    public 

    void Awake()
    {
        InitializeNamesAndColors();
    }

    public String GetRandomName()
    {
        if (_names == null || Colors == null)
            InitializeNamesAndColors();
        string name = _names[Random.Range(0, _names.Count)];
        string color = Colors.Keys.ToList()[Random.Range(0, Colors.Count)];
        return UppercaseFirst(color) + " " + name;
    }

    private void InitializeNamesAndColors()
    {
        using (StreamReader r = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Names.json")))
        {
            string json = r.ReadToEnd();
            _names = JsonConvert.DeserializeObject<List<String>>(json);
        }
        using (StreamReader r = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Colors.json")))
        {
            string json = r.ReadToEnd();
            Colors = JsonConvert.DeserializeObject<Dictionary<string, long[]>>(json);
        }
    }
    private string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
