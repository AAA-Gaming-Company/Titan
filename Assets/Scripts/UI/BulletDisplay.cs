using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BulletDisplay : MonoBehaviour {
    private Image image;

    private void Start() {
        this.image = base.gameObject.GetComponent<Image>();
    }

    public void UpdateBullet(GameObject prefab) {
        SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
        this.image.sprite = renderer.sprite;
        this.image.color = renderer.color;
        this.image.transform.localScale = prefab.transform.localScale;
    }
}
