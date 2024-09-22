using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetPrefabCollection", menuName = "ScriptableObjects/PlanetPrefabCollection", order = 1)] 
public class PlanetPrefabCollection : ScriptableObject
{
    
    public List<string> names = new List<string> { 
        "Aurelia",
        "Zyphor",
        "Tiberon",
        "Quinara",
        "Eldoria",
        "Volturn",
        "Nebulon",
        "Xaloria",
        "Galatea",
        "Orionis",
        "Draconis",
        "Phoenixia",
        "Arcturia",
        "Lyra",
        "Vespera"
    };



    public List<GameObject> planetPrefabs = new List<GameObject>();
}
