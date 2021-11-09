using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class DestroyOnTrigger2D : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;
    [SerializeField] string triggeringTag_2;

    private void OnTriggerEnter2D(Collider2D other) {
        if ( enabled && (other.tag == triggeringTag ||
            other.tag == triggeringTag_2 && other.gameObject.GetComponent<Mover>().getVelocity().y < 0)) {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void Update() {
        /* Just to show the enabled checkbox in Editor */
    }
}
