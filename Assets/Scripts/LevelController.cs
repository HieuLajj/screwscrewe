using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelController>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }



    public LoadDataBase loadDataBase;
    public Transform MainLevelSetupCreateMap;
    public RootLevel rootlevel;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(17, 17, false);    
        

        rootlevel = new RootLevel();
        //loadDataBase.LoadLevelGame("Assets/_GameAssets/data_2.json");
        loadDataBase.LoadLevelGame(PlayerPrefs.GetString("levelstart"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
