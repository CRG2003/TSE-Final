using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class towerControl : MonoBehaviour
{
    // Init =======================================================================================================

    // variables
    string t = "";

    float raylen;
    float time;

    float mv = 100f;
    float sv = 100f;

    int gold = 900;

    int[] costs = {100, 120, 180, 250};

    Vector3[] pos = {new Vector3(3, 35, -21), new Vector3(3, 36, -24), new Vector3(3, 39, -27), new Vector3(5, 50, -35), new Vector3(22, 62, -46)};

    List<string> info = new List<string>();


    // objects
    Camera cam;
    GameObject ba;
    GameObject spots;
    GameObject spot;
    GameObject nexus;
    GameObject upgrade;
    GameObject cur = null;
    List<GameObject> locals = new List<GameObject>();
    List<GameObject> towers = new List<GameObject>();
    List<GameObject> ring = new List<GameObject>();

    public GameObject AAT;
    public GameObject HT;
    public GameObject MT;
    public GameObject PT;

    public GameObject dot;

    public TMP_Text bank;
    public TMP_Text hp;

    public TMP_Text nm;
    public TMP_Text rang;
    public TMP_Text dmg;
    public TMP_Text firert;
    public TMP_Text sell;
    public TMP_Text upgr;

    public AudioClip AAs;
    public AudioClip HTs;
    public AudioClip MTs;
    public AudioClip PTs;
    public AudioClip mus;

    AudioSource sound;


    // start
    void Start(){
        cam = FindObjectOfType<Camera>();
        ba = GameObject.Find("r");
        spots = GameObject.Find("spots");
        nexus = GameObject.Find("ForestCastle_Red");
        upgrade = GameObject.Find("upgradeMenu");

        sound = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        foreach (Transform child in spots.transform){
            GameObject ob = child.gameObject;
            locals.Add(ob);
        }
        sound.clip = mus;
        sound.loop = true;
        sound.volume = mv / 100;
        sound.Play();
    }


    // Update ======================================================================================================

    void FixedUpdate(){
        bank.text = "$ " + gold.ToString();
        hp.text = nexus.GetComponent<StructureHitPoints>().get().ToString();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        if (ground.Raycast(ray, out raylen)){
            Vector3 point = ray.GetPoint(raylen);
            if (upgradeTower(point.x, point.z) && Input.GetKey(KeyCode.Mouse0)){
                info = cur.GetComponent<Tower>().getInfo();
            }
            else if (Input.GetKey(KeyCode.Mouse0) && point.x > -15 && point.z > -27){
                cur = null;
            }
            if (touch(point.x, point.z) && Input.GetKey(KeyCode.Mouse0)){
                spawn();
            }
            else if (Input.GetKey(KeyCode.Mouse0)){
                t = "";
            }
        }


        if (cur != null){
            info = cur.GetComponent<Tower>().getInfo();
            upgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(-335, -160);
            nm.text = info[0];
            rang.text = info[1];
            dmg.text = info[2];
            firert.text = info[3];
            sell.text = "Sell: " + info[4];
            if (cur.GetComponent<Tower>().ups < 3){
                upgr.text = info[5];
            }
            else{
                upgr.text = ("Max Upgrades");
            }
            for (int i = 0; i < ring.Count; i++){
                Destroy(ring[i].gameObject);
            }
            ring.Clear();
            for (int i = 0; i < 360; i+=4){
                ring.Add(Instantiate(dot, new Vector3(cur.transform.position.x + Mathf.Sin(i) * cur.GetComponent<Tower>().range, 1.5f, cur.transform.position.z + Mathf.Cos(i) * cur.GetComponent<Tower>().range), Quaternion.identity));
            }

        }
        else{
            upgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(-335, -360);
            for (int i = 0; i < ring.Count; i++){
                Destroy(ring[i].gameObject);
            }
            ring.Clear();
        }
    }


    // Additional methods ===========================================================================================

    // allows buttons to select tower type 
    public void tower (string i){
        t = i;
    }

    // checks mouse is pointing at a placeable tower location
    bool touch(float x, float z){
        foreach(GameObject sp in locals){
            if (sp.GetComponent<spot>().available()){
                float tx = sp.transform.position.x;
                float tz = sp.transform.position.z;
                if (x > tx - 2 && x < tx + 2 && z > tz - 2 && z < tz + 2){
                    spot = sp;
                    return true;
                }
            }
        }
        return false;
    }

    bool upgradeTower(float x, float z){
        foreach(GameObject tw in towers){
            float tx = tw.transform.position.x;
            float tz = tw.transform.position.z;
            if (x > tx - 2 && x < tx + 2 && z > tz - 2 && z < tz + 2 && Input.GetKey(KeyCode.Mouse0)){
                cur = tw;
                return true;
            }
        }
        return false;
    }

    // spawns the selected tower
    void spawn(){
        if (t == "T1" && gold >= costs[0]){
            gold -= costs[0];
            GameObject t = Instantiate(AAT, new Vector3(spot.transform.position.x, 0, spot.transform.position.z), Quaternion.identity);
            spot.GetComponent<spot>().take();
            t.AddComponent<Tower>();
            t.GetComponent<Tower>().AA(AAs, sound, sv);
            towers.Add(t);
        }
        if (t == "T2" && gold >= costs[1]){
            gold -= costs[1];
            GameObject t = Instantiate(HT, new Vector3(spot.transform.position.x, 0, spot.transform.position.z), Quaternion.identity);
            spot.GetComponent<spot>().take();
            t.AddComponent<Tower>();
            t.GetComponent<Tower>().HT(HTs, sound, sv);
            towers.Add(t);
        }
        if (t == "T3" && gold >= costs[2]){
            gold -= costs[2];
            GameObject t = Instantiate(MT, new Vector3(spot.transform.position.x, 0, spot.transform.position.z), Quaternion.identity);
            spot.GetComponent<spot>().take();
            t.AddComponent<Tower>();
            t.GetComponent<Tower>().MT(MTs, sound, sv);
            towers.Add(t);
        }
        if (t == "T4" && gold >= costs[3]){
            gold -= costs[3];
            GameObject t = Instantiate(PT, new Vector3(spot.transform.position.x, 0, spot.transform.position.z), Quaternion.identity);
            spot.GetComponent<spot>().take();
            t.AddComponent<Tower>();
            t.GetComponent<Tower>().PT(PTs, sound, sv);
            towers.Add(t);
        }
    }

    public void kill(){
        gold += 50;
    }

    public void dirRange(){
        if (cur != null){
            if (gold > cur.GetComponent<Tower>().upgrade && cur.GetComponent<Tower>().ups < 3){
                gold -= cur.GetComponent<Tower>().upgrade;
                cur.GetComponent<Tower>().upRange();
            }
        }
    }
    public void dirDamage(){
        if (cur != null){
            if (gold > cur.GetComponent<Tower>().upgrade && cur.GetComponent<Tower>().ups < 3){
                gold -= cur.GetComponent<Tower>().upgrade;
                cur.GetComponent<Tower>().upDamage();
            }
        }
    }
    public void dirFireRate(){
        if (cur != null){
            if (gold > cur.GetComponent<Tower>().upgrade && cur.GetComponent<Tower>().ups < 3){
                gold -= cur.GetComponent<Tower>().upgrade;
                cur.GetComponent<Tower>().upFireRate();
            }
        }
    }

    public void sellTower(){
        gold += cur.GetComponent<Tower>().sellPrice();
        foreach (GameObject s in locals){
            if (s.transform.position.x == cur.transform.position.x && s.transform.position.z == cur.transform.position.z){
                s.GetComponent<spot>().untake();
            }
        }
        Destroy(cur.gameObject);
        towers.Remove(cur);
    }

    public void setV(float s, float m){
        mv = m;
        sv = s;
        Debug.Log(mv.ToString());
    }
}