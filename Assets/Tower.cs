using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range;
    private float damage;
    private float attackSpeed;
    private float price = 0;
    private string Name;
    public int upgrade;

    public int ups = 0;
    float vol = 100f;

    GameObject enemy;
    AudioClip sound;
    AudioSource source;
    List<GameObject> enemies = new List<GameObject>();
    List<string> info = new List<string>();

    float tick;

    void Start(){
        enemy = GameObject.Find("Enemies");
    }

    void FixedUpdate(){
        tick++;
        enemies.Clear();
        foreach (Transform child in enemy.transform){
            GameObject i = child.gameObject;
            enemies.Add(i);
        }
        foreach (GameObject e in enemies){
            if (Vector3.Distance(e.transform.position, transform.position) < range){
                if (tick >= attackSpeed){
                    e.GetComponent<PlayerController>().health -= damage;
                    tick = 0;
                    source.PlayOneShot(sound, vol / 100);
                }
            }
        }
    }

    public void AA(AudioClip s, AudioSource a, float v){
        range = 18;
        damage = 5;
        attackSpeed = 80;
        Name = "Anti Air";
        price += 60;
        upgrade = 70;
        sound = s;
        source = a;
        vol = v;
    }
    public void HT(AudioClip s, AudioSource a, float v){
        range = 12;
        damage = 2;
        attackSpeed = 20;
        Name = "Hacker";
        price += 72;
        upgrade = 80;
        sound = s;
        source = a;
        vol = v;
    }
    public void MT(AudioClip s, AudioSource a, float v){
        range = 9;
        damage = 15;
        attackSpeed = 150;
        Name = "Cannon";
        price += 108;
        upgrade = 120;
        sound = s;
        source = a;
        vol = v;
    }
    public void PT(AudioClip s, AudioSource a, float v){
        range = 26;
        damage = 10;
        attackSpeed = 200;
        Name = "Plasma";
        price += 150;
        upgrade = 160;
        sound = s;
        source = a;
        vol = v;
    }

    public List<string> getInfo(){
        info.Clear();
        info.Add(Name);
        info.Add("Range: " + range.ToString());
        info.Add("Damage: " + damage.ToString());
        info.Add("FireRate: " + attackSpeed.ToString());
        info.Add(price.ToString());
        info.Add("Upgrade: " + upgrade.ToString());
        return info;
    }

    public void upRange(){
        range += range * 0.1f;
        ups++;
        upgrade += 20;
        price += price * .4f;
    }
    public void upDamage(){
        damage += damage * 0.1f;
        ups++;
        upgrade += 20;
        price += price * .4f;
    }
    public void upFireRate(){
        attackSpeed -= attackSpeed * 0.1f;
        ups++;
        upgrade += 20;
        price += price * .4f;
    }

    public int sellPrice(){
        return (int)price;
    }
}
