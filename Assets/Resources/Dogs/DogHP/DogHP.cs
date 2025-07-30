using UnityEngine;
using UnityEngine.UI;

public class DogHP : MonoBehaviour
{
    private Dog parent;
    private int maxHP;
    private Vector3 scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Dog>();
        maxHP = parent.getMaxHP();

        for(int i = 0; i < maxHP; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }

        if (maxHP < 10)
        {

            Vector2 shift = new Vector2(50 * (10 - maxHP), 0);

            transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition += shift;

        }

        scale = transform.localScale;
    }

    public void UpdateHP()
    {
        int HP = parent.getHP();

        // Debug.Log("HP: " + HP.ToString());

        for(int i = 0; i < maxHP; i++)
        {
            Image graphic = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>();

            if(graphic.color == Color.black)
                break;

            if(i + 1 > HP){
                graphic.color = Color.black;
                // Debug.Log("remove heart " + i.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Mathf.Sign(transform.parent.localScale.x) * scale.x, scale.y, scale.z);
    }
}
