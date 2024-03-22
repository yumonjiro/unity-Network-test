using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Col.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        void InitialCheck()
        {
            //check if necessary gameobjects exist
            //GDC : GameDataContainer
            //TM : TileManager
            bool isGDC = GameDataSource.Instance != null;
            bool isTM = TileManager.Instance != null;
            if(!(isGDC && isTM))
            {
                throw new System.Exception("Some Game Objects are missing. Check Hierarchy tab");
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            InitialCheck();
        }

        void Initialize()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}