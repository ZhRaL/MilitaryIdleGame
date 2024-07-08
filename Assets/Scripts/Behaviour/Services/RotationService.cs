using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class RotationService : MonoBehaviour
{
    List<Rot> rotations = new();

    // Update is called once per frame
    void Update()
    {
        Rot[] copy = new Rot[rotations.Count];
        rotations.CopyTo(copy);

        foreach (var item in copy)
        {
            if (item.time < 0)
            {
                rotations.Remove(item);
            }
            float anglediff = item.tf.rotation.y - item.degree;
            item.tf.Rotate(Vector3.up, Mathf.LerpAngle(item.tf.rotation.y, item.degree, item.turnPerSecond * Time.deltaTime));
            item.time -= Time.deltaTime;
        }

    }

    public void AddRotation(Transform tf, float degree, float time)
    {
        rotations.Add(new Rot(tf, degree, time));
    }

    private class Rot
    {
        public Transform tf;
        public float degree;
        public float time;
        public float turnPerSecond;

        public Rot(Transform tf, float degree, float time)
        {
            this.tf = tf;
            this.degree = degree;
            this.time = time;
            this.turnPerSecond = (degree / time);
            logger.log("turnPerSecond: " + turnPerSecond);
        }
    }
}
