using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour {
    private GameObject player;
    public UnityEvent triggerAction;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        triggerAction.Invoke();
    }
}
