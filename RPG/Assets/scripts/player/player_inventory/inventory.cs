using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    List<item> list;
    public GameObject Inventory;
    public GameObject Container;
    void Start()
    {
        list = new List<item>();   
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                item item = hit.collider.GetComponent<item>();
                if(item != null)
                {
                    list.Add(item);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (Inventory.activeSelf)
            {
                Inventory.SetActive(false);
                for (int i = 0; i < Inventory.transform.childCount; i++)
                {
                    if(Inventory.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(Inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                Inventory.SetActive(true);
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    item it = list[i];
                    if(Inventory.transform.childCount >= i)
                    {
                        GameObject img = Instantiate(Container);
                        img.transform.SetParent(Inventory.transform.GetChild(i).transform);
                        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.sprite);
                        img.AddComponent<Button>().onClick.AddListener(() => Remove(it, img));
                    }
                    else break;
                }
            }
        }
    }
    void Remove(item it, GameObject obj)
    {
        GameObject newo = Instantiate(Resources.Load<GameObject>(it.prefab));
        newo.transform.position = transform.position + transform.forward + transform.up;
        Destroy(obj);
        list.Remove(it);
    }
}
