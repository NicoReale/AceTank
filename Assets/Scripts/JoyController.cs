using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyController : Controller, IDragHandler, IEndDragHandler
{

    Vector3 moveDir;
    Vector3 moveDirModified;

    Vector3 initPos;

    public float Horizontal { get { return moveDirModified.x; } }
    public float Vertical { get { return moveDirModified.y; } }

    public float maxMagnitude;


    private void Start()
    {
        initPos = transform.position;
    }
    public override Vector3 GetMovementInput()
    {
        //Para moverse en Z
        //moveDirModified = new Vector3(moveDir.x, 0, moveDir.y);
        moveDirModified = moveDir;
        moveDirModified /= maxMagnitude;
        //if(moveDirModified.magnitude >)
        return moveDirModified;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveDirModified = moveDir;
        moveDirModified /= maxMagnitude;
        moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - initPos, maxMagnitude);
        transform.position = initPos + moveDir;       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initPos;
        moveDir = Vector3.zero;
        moveDirModified = Vector3.zero;
    }
}
