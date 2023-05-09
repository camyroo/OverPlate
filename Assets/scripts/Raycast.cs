using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    public Transform _selection;
    public Outline _outline;
    public bool hitting = false;

    void Update() {
        // Disable outline on previous selection
        if(_selection != null) {
            if(_outline != null) {
                _outline.enabled = false;
            }
            _selection = null;
            _outline = null;
        }

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 1f)) {
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.red);
            
            // Check if object is selectable
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag)) {
                hitting = true;
                // Enable outline on current selection
                _selection = selection;
                _outline = _selection.gameObject.GetComponent<Outline>();
                if(_outline != null) {
                    _outline.enabled = true;
                }
            }
        } 
        else {
            hitting = false;
            // Draw green ray and disable outline
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * 1f, Color.green);
        }
    }
}
