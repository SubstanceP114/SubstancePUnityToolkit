using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovableItem2D : MonoBehaviour
{
    [Header("平移")]
    [SerializeField] protected GameObject trail;
    [SerializeField] protected float moveVelocity;
    [Header("自转")]
    [SerializeField] protected float selfCycle;
    [Header("公转")]
    [SerializeField] protected Transform anchor;
    [SerializeField] protected float cycle;

    private List<Transform> points;
    private int currentTarget;
    protected Vector3 orient;
    private float distance;

    private float timer;
    private float time;

    private float selfRotateVelocity;

    private float angleOrigin;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.isKinematic = true;

        timer = 0;

        selfRotateVelocity = 2 * Mathf.PI / selfCycle;

        if (trail)
        {
            points = trail.GetComponentsInChildren<Transform>().ToList();
            points.RemoveAt(0);
            foreach (Transform t in points) t.position = new Vector3(t.position.x, t.position.y);
            currentTarget = 0;
            transform.position = points[0].position;
            Next();
        }
        if (anchor)
        {
            anchor.position = new Vector3(anchor.position.x, anchor.position.y);
            distance = Vector2.Distance(transform.position, anchor.position);
            angleOrigin = Vector2.Angle(Vector2.right, (Vector2)(rb.transform.position - anchor.position));
        }
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (selfRotateVelocity != float.PositiveInfinity
            && selfRotateVelocity != float.NegativeInfinity)
            rb.MoveRotation(selfRotateVelocity + rb.rotation);
        if (anchor)
        {
            angleOrigin -= 2 * Mathf.PI / cycle * Time.fixedDeltaTime % (2 * Mathf.PI);
            Vector2 localPos = new Vector2(distance * Mathf.Cos(angleOrigin), distance * Mathf.Sin(angleOrigin));
            rb.MovePosition((Vector2)anchor.position + localPos);
        }
        if (trail)
        {
            rb.MovePosition(transform.position + moveVelocity * orient.normalized * Time.fixedDeltaTime);
            if (timer >= time) Next();
        }
    }
    private void Next()
    {
        currentTarget = (currentTarget + 1) % points.Count;
        orient = points[currentTarget].position - transform.position;
        time = Mathf.Abs(orient.magnitude / moveVelocity);
        timer = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (trail)
        {
            points = trail.GetComponentsInChildren<Transform>().ToList();
            points.RemoveAt(0);
            Gizmos.DrawWireSphere(points[0].position, 0.25f);
            for (int i = 0; i < points.Count; i++)
                Gizmos.DrawLine(points[i].position, points[(i + 1) % points.Count].position);
        }
        if (anchor)
        {
            Gizmos.DrawWireSphere(anchor.position, 0.25f);
            Gizmos.DrawWireSphere(this.transform.position, 0.25f);
            Gizmos.DrawLine(anchor.position, this.transform.position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (trail)
        {
            Gizmos.DrawWireSphere(points[0].position, 0.25f);
            for (int i = 0; i < points.Count; i++)
                Gizmos.DrawLine(points[i].position, points[(i + 1) % points.Count].position);
        }
        if (anchor)
        {
            Gizmos.DrawWireSphere(anchor.position, 0.25f);
            Gizmos.DrawWireSphere(this.transform.position, 0.25f);
            Gizmos.DrawLine(anchor.position, this.transform.position);
        }
    }
}
