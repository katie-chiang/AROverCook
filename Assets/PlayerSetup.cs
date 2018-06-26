using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    private GameObject player;
    public Color m_Color;
    // Use this for initialization
    void Start () {
    //player = Instantiate(playerPrefab);
    //Debug.Log("Debug: add player");

    }
    
    // Update is called once per frame
    void Update () {
    
    }
    
    public override void OnStartClient()
    {
        Debug.Log("Debug: player setup");
        base.OnStartClient();
  
        if (!isServer) //if not hosting, we had the tank to the gamemanger for easy access!
            player = Instantiate(gameObject);
  
        GameObject m_Renderers = transform.Find("Player").gameObject;
  
        // Get all of the renderers of the tank.
        Renderer[] renderers = m_Renderers.GetComponentsInChildren<Renderer>();
  
        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            Debug.Log("Debug: change color");
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_Color;
        }
  
        if (m_Renderers)
            m_Renderers.SetActive(false);
  
        //m_NameText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_Color) + ">"+m_PlayerName+"</color>";
        //m_Crown.SetActive(false);
    }
    public override void OnStartServer()
    {
        Debug.Log("Debug: on start server");
    }

}
