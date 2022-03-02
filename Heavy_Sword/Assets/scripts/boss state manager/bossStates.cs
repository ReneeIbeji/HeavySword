using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        PRESTART
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected State nextState;
    protected GameObject[] platforms;

    public State(GameObject[] Platforms)
    {
        platforms = Platforms;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State process()
    {
        if(stage == EVENT.ENTER) { Enter(); }
        if(stage == EVENT.UPDATE) { Update(); }
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

}


public class Prestart : State
{

    public  Prestart(GameObject[] Platforms)
        : base(Platforms)
    {
        name = STATE.PRESTART;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        base.Exit();
    }

}