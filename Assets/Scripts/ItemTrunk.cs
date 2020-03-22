using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using DG.Tweening;

public class ItemTrunk : MonoBehaviour
{
    public Transform target;
    private bool isTrigged = false;

    public Transform[] waypoints;
    private Vector3[] _waypoints;

    private Collider col;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        _waypoints = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            _waypoints[i] = waypoints[i].position;
        }
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTrigged)
        {
            GameLevelSystem.Instance.PassLevel(4);
            isTrigged = true;

            Destroy(transform.GetChild(0).gameObject);
            col.enabled = false;
            MoveToTarget();
            //transform.position = target.position;
            //transform.localRotation = target.localRotation;
        }
    }

    Sequence seq;

    public void MoveToTarget()
    {
        ////DEBUG
        initialPosition = transform.position;
        initialRotation = transform.localRotation;
        ////

        seq = DOTween.Sequence();
        //Vector3 rotate1 = new Vector3(85, 0, 0);
        //Vector3 move1 = new Vector3(0, 2, 0);
        //Vector3 rotate2 = new Vector3(0,30, 0);
        //mySequence.Append(transform.DOLocalRotate(rotate1, 5.45f));
        seq.Append(transform.DORotate(new Vector3(90, 0, 0), 1));
        seq.Append(transform.DORotate(new Vector3(0, 0, 56), 0.6f).SetLoops(-1, LoopType.Incremental));
        transform.DOPath(_waypoints, 15, PathType.CatmullRom).OnWaypointChange(WaypointChange);
        //mySequence.Join(transform.DOLocalMoveY(2,4f));

        //mySequence.Append(transform.DOLocalRotate(rotate2, 10.2f));
        //transform.DOLocalMove(move1, 10.5f);
        //transform.DOLocalRotate(rotate1, 5.45f);
        //transform.DOMove(target.position, 4.5f);
    }

    public void Reset()
    {
        transform.position = initialPosition;
        transform.localRotation = initialRotation;
    }

    private void WaypointChange(int index)
    {
        Debug.Log(index);
        //transform.DOLocalRotate(waypoints[index].rotation.eulerAngles, 5f);
        if (index == waypoints.Length - 1)
        {
            seq.Complete();
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        if (index == waypoints.Length)
        {
            transform.DORotate(new Vector3(0, 0, 90), 1);
            col.enabled = true;
        }
    }

}

[CustomEditor(typeof(ItemTrunk))]
public class ItemTrunkEditor: Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        ItemTrunk myTarget = (ItemTrunk)target;

        if (GUILayout.Button("Move To Target"))
        {
            myTarget.MoveToTarget();
        }

        if (GUILayout.Button("Reset"))
        {
            myTarget.Reset();
        }
    }
}