using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeCameraButton : MonoBehaviour
{
    // create camera list that can be updated in the inspector
    public List<Camera> Cameras;
    public List<Light> Lights;

    // create frame and button variables 
    private VisualElement frame;
    private Button button;
    private Button light_button;

    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        // get the UIDocument component (make sure this name matches!)
        var uiDocument = GetComponent<UIDocument>();
        // get the rootVisualElement (the frame component)
        var rootVisualElement = uiDocument.rootVisualElement;
        frame = rootVisualElement.Q<VisualElement>("Frame");
        // get the button, which is nested in the frame
        button = frame.Q<Button>("Button");
        // create event listener that calls ChangeCamera() when pressed
        button.RegisterCallback<ClickEvent>(ev => ChangeCamera());

        light_button = frame.Q<Button>("LightButton");
        light_button.RegisterCallback<ClickEvent>(ev => ToggleLight());
    }

    // initialize click count
    int click = 0;
    private void ChangeCamera(){
        EnableCamera(click);
        click++;
        // reset counter so it is not out of bounds (only have 4 cameras)
        if(click > 3){
            click = 0;
        }
    }

    private void EnableCamera(int n)
    {
        // disable each of the cameras
        Cameras.ForEach(cam => cam.enabled = false);
        // Cameras.ForEach(cam => cam.depth = 0);

        // enable the selected camera
        Cameras[n].enabled = true;
        // Cameras[n].depth = 1;

    }

    private void ToggleLight()
    {
        Lights.ForEach(light => light.enabled = !light.enabled);
    }
    
}
