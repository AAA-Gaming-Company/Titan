using Pathfinding;
using UnityEngine;

public class SpawnTriggerArea : TriggerArea {
    [Header("Spawn")]
    public GameObject prefab;
    public Transform spawnLocation;

    [Header("AI Destination")]
    public bool hasAiDestination;
    public Transform aiDestination;

    protected override void TriggerAction() {
        GameObject newObject = Instantiate(this.prefab, this.spawnLocation.position, Quaternion.identity);

        //Make sure it has a Z=0
        Vector3 pos = newObject.transform.position;
        pos.z = 0;
        newObject.transform.position = pos;

        //AI Destination
        if (this.hasAiDestination) {
            AIDestinationSetter aiDestinationSetter = newObject.GetComponent<AIDestinationSetter>();
            aiDestinationSetter.target = this.aiDestination;
        }
    }
}
