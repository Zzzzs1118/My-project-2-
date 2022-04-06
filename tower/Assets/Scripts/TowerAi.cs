using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAi : MonoBehaviour {
    public List<GameObject> enemys = new List<GameObject>();
    private GameObject targetObject;
    private Transform paottong;
    public GameObject bulletPrefab;
    public AudioClip clip;
    private Transform firePos;
    private float times;//计时器
    void Start() {
        times = 0;
        targetObject = null;
        paottong = transform.GetChild(0).GetChild(0);
        firePos = transform.GetChild(0).GetChild(0).GetChild(0);
    }
    void Update() {
        if (0 != enemys.Count)
        {
            if (null == targetObject)
            {
                targetObject = GetTargetObj(enemys);
            }
        }
        else
        {
            targetObject = null;
        }
        if (targetObject != null)
        {
            LookAtObject();
        }
        Debug.Log("列表元素为" + enemys.Count + "当前目标" +  targetObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemys" && !enemys.Contains(other.gameObject))
            enemys.Add(other.gameObject);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemys" && enemys.Contains(other.gameObject))
        {
            if (other.gameObject == targetObject)
            {
                targetObject = null;
            }
            enemys.Remove(other.gameObject);
        }
           
    }

    private GameObject GetTargetObj(List<GameObject> list)
    {
        GameObject obj = null;
        float tempDis = 1000;//对比值
        float dis = 0;//实际值

        for (int i = 0; i < list.Count; i++)
        {
            dis = Vector3.Distance(transform.position, list[i].transform.position);
            if (dis <= tempDis)
            {
                tempDis = dis;
                obj = list[i];
            }
        }
        return obj;
    }

    private void Attack()//攻击发射的子弹
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<BulletMove>().GetObject = targetObject;
        bullet.GetComponent<BulletMove>().rootTower = gameObject;
        AudioSource.PlayClipAtPoint(clip, firePos.position, 0.5f);
    }

    private void LookAtObject()//朝向目标
    {
        Vector3 pos = targetObject.transform.position;
        pos.y = paottong.position.y;
        paottong.LookAt(pos);
        times += Time.deltaTime;
        if (times >= 1f)
        {
            times = 0;
            Attack();
        }
    }
}
