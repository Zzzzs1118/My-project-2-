using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    private GameObject targetObject = null;
    public GameObject rootTower = null;
    private float times = 0;

    public GameObject GetObject
    {
        set { targetObject = value; }
        get { return targetObject ; }
    }
    void Update() {
        times += Time.deltaTime;
        if (times >= 3) Destroy(gameObject);

        if (targetObject == null) { Destroy(gameObject); return; }
        if (Vector3.Distance(transform.position, targetObject.transform.position) > 0.5f)
        {
            transform.LookAt(targetObject.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
        else
        {
            rootTower.GetComponent<TowerAi>().enemys.Remove(targetObject);
            Destroy(targetObject);
            Destroy(gameObject);
        }
	}
}
