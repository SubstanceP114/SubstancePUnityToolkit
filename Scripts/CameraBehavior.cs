using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private enum Mode
    {
        FirstPerson,
        SecondPerson,
        ThirdPerson,
        Flexible
    }
    [SerializeField] private Mode mode;
    [SerializeField, Range(0, 1)] private float lag;
    [Header("Offset")]
    [SerializeField] private Vector3 offsetInFirstPerson = new Vector3(0, 1, 0.5f);
    [SerializeField] private Vector3 offsetInSecondPerson = new Vector3(0, 1, 3);
    [SerializeField] private Vector3 offsetInThirdPerson = new Vector3(0, 3, 3);
    [SerializeField] private Vector3 offsetFlexible = new Vector3(0, 10, 0);
    [Header("FOV")]
    [SerializeField] private float minFOV = 30;
    [SerializeField] private float maxFOV = 90;
    [SerializeField] private float FOVChangeScale = 300;
    private Dictionary<Mode, Vector3> offsets = new();
    private void Start()
    {
        offsets.Clear();
        offsets.Add(Mode.FirstPerson, offsetInFirstPerson);
        offsets.Add(Mode.SecondPerson, offsetInSecondPerson);
        offsets.Add(Mode.ThirdPerson, offsetInThirdPerson);
        offsets.Add(Mode.Flexible, offsetFlexible);
        transform.position = offsets[mode];
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) Camera.main.fieldOfView += Time.deltaTime * FOVChangeScale;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) Camera.main.fieldOfView -= Time.deltaTime * FOVChangeScale;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
    }
    private void FixedUpdate()
    {
        if (mode == Mode.Flexible) return;
        transform.position = Vector3.Lerp(transform.position,
            Player.Current.transform.position + offsets[mode], 1 - lag);
        if (mode == Mode.FirstPerson) transform.rotation = Player.Current.transform.rotation;
        else transform.LookAt(Player.Current.transform.position);
    }
}
