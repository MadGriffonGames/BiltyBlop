using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOgreState 
{
    void Execute();
    void Enter(Ogre enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}
