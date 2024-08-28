using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.VersionControl.ListControl;

namespace StateMachine
{
    [ExecuteAlways]
    //[InitializeOnLoad]
    public class ButtonListeners : MonoBehaviour
    {
        //[SerializeField] private Button buttonLeftArrow;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonInfo;
        [SerializeField] private List<Button> buttonsMarkers = new List<Button>();
        [SerializeField] private Button buttonCloseCanvas;

        private StateMachineButtons _stateMachine;
        private States _lastState;

        private void Start()
        {
            _lastState = States.WithoutMarkers;
            _stateMachine = GetComponent<StateMachineButtons>();

            //buttonLeftArrow.onClick.AddListener(() => GetComponent<StateMachineButtons>().ChangeState(States.WithoutMarkers));
            buttonPlay.onClick.AddListener(OnPlayButtonClick);
            buttonInfo.onClick.AddListener(OnInfoButtonClick);
            buttonCloseCanvas.onClick.AddListener(OnInfoButtonClick);

            for (int i = 0; i < buttonsMarkers.Count; i++)
            {
                buttonsMarkers[i].onClick.AddListener(OnMarkerClick);
            }
            //buttonRightArrow.onClick.AddListener(() => GetComponent<StateMachineButtons>().ChangeState(States.Slider));
        }

        private void OnPlayButtonClick()
        {
            if (_stateMachine != null)
            {
                if (_stateMachine._currentState == States.WithoutMarkers || _stateMachine._currentState == States.WithMarkers)
                {
                    _lastState = _stateMachine._currentState;
                    _stateMachine.ChangeState(States.VideoPlayer);
                }
                else if(_stateMachine._currentState == States.VideoPlayer)
                {
                    _stateMachine.ChangeState(_lastState);
                }
            }
        }

        private void OnInfoButtonClick()
        {
            if (_stateMachine != null)
            {
                if (_stateMachine._currentState == States.WithoutMarkers)
                {
                    _stateMachine.ChangeState(States.WithMarkers);
                }
                else if (_stateMachine._currentState == States.WithMarkers)
                {
                    _stateMachine.ChangeState(States.WithoutMarkers);
                }
                else if (_stateMachine._currentState == States.Slider)
                {
                    _stateMachine.ChangeState(States.WithMarkers);
                }
            }
        }

        private void OnMarkerClick()
        {
            if (_stateMachine != null)
            {
                if (_stateMachine._currentState == States.WithMarkers)
                {
                    _stateMachine.ChangeState(States.Slider);
                }
            }
        }
    }
}