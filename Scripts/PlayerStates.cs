using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : AbstractStates
{
    PlayerFSM fsm;
    public PlayerIdleState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }
}
public class PlayerMoveState : AbstractStates
{
    PlayerFSM fsm;
    public PlayerMoveState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }
}