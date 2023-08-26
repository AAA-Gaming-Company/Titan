using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BulletDisplay : MonoBehaviour {
    private Image image = null;

    public void UpdateBullet(GameObject prefab) {
        SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
        if (this.image == null) {
            this.image = this.gameObject.GetComponent<Image>();
        }

        this.image.sprite = renderer.sprite;
        this.image.color = renderer.color;
        this.image.transform.localScale = prefab.transform.localScale;
    }
}
