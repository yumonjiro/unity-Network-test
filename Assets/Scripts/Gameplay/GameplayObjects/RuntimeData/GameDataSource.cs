using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using Unity.Col.Gameplay.Actions;
using Action = Unity.Col.Gameplay.Actions.Action;

public class GameDataSource : MonoBehaviour
{
    //TODO Unitに一意的なIDを振り分ける。この値はサーバーとクライアント間でも同じでなければいけない。
    /// <summary>
    /// GameDataSourceはインスペクタ上でアタッチされたにアクションのスクリプタブルオブジェクトを管理する
    /// 
    /// </summary>
    public static GameDataSource Instance { get; private set; }

    public List<Action> allActions;

    void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception("Multiple GameDataSources detected");
        }

        BuildActionIds();
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public Action GetActionByID(ActionID actionID)
    {
        return allActions[actionID.ID];
    }

    private void BuildActionIds()
    {
        //TODO Implement this method
        //This method assign ids to Action scriptable objects
        int i = 0;
        foreach(var action in allActions)
        {
            action.actionID = new ActionID { ID = i};
            i++;
        }
    }
}

