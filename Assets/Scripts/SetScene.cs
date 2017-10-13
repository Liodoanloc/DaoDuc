using UnityEngine;
using System.Collections;

public class SetScene : MonoBehaviour {

    // Use this for initialization
    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;

	public void Start () {
        int name = Random.Range(1, 4);
        if (name == 1)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = bg1;
        else if (name == 2)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = bg2;
        else this.gameObject.GetComponent<SpriteRenderer>().sprite = bg3;

    }
}
