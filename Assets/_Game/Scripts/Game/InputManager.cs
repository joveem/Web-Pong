using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] public bool IsControlEnable = false;

    // [Range(-1f, 1f)]
    // [SerializeField] float input = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (IsControlEnable)
            HandleControlInputs();

    }

    void HandleControlInputs()
    {

        // debug
        //input = Input.GetAxis("Horizontal");

        GameManager.instance.ApplyLocalPlayerMovementInput(Input.GetAxis("Horizontal"));

    }

}
