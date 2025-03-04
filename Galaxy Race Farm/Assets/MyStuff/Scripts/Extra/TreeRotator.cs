using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TreeRotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed;

    private void OnEnable()
    {
        //EditorApplication.update += RotateInEditor;
    }

    private void OnDisable()
    {
        //EditorApplication.update -= RotateInEditor;
    }

    private void RotateInEditor()
    {
        if (!Application.isPlaying) 
        {
            transform.Rotate(rotationSpeed * 0.01f); 
        }
    }
}
