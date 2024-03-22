using System;
namespace Unity.Col.Gameplay
{
    public enum ClientGameState
    {
        PrepareBattle,
        PlayerActionSelection,
        EnemyActionSelection,
        WaitingServerResponse,
        ActionPerform,
    }

    public enum ServerGameState
    {
        PrepareBattle,
        WaitingClientActionInput,
        
    }

}