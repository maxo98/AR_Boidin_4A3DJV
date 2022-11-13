using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script
{
    public class UIBehavior : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private AudioControlUI audioControls;
        [SerializeField] private List<GameObject> objectsList;
        [SerializeField] private Text objectSelectedText;
        [SerializeField] private Dropdown objectSelector;
        
        [SerializeField] private List<InputField> transformInputField;

        [SerializeField] private Text audioSourceRotationText;
        [SerializeField] private List<Button> audioSourceRotationList;
        [SerializeField] private InputField rotationSpeedInputField;
        
        [SerializeField] private AudioSourceBehavior audioSourceBehaviour;
        
        [SerializeField] private string textForObjectSelected;
        
        private GameObject _selectedGameObjet;
        private Vector3 _selectedGameObjectPosition;
        private bool _azimuthClicked;
        private bool _elevationClicked;

        private void Start()
        {
            _azimuthClicked = false;
            _elevationClicked = false;
            _selectedGameObjet = objectsList[0];
            objectSelectedText.text = $"{textForObjectSelected}{objectSelector.options[0].text.ToUpper()}";
            SetUI();
        }

        private void Update()
        {
            SetUI();
        }

        public void HandleSelectedObject(int value)
        {
            _selectedGameObjet = objectsList[value];
            objectSelectedText.text = $"{textForObjectSelected}{objectSelector.options[value].text.ToUpper()}";
            SetUI();
            var audioSourceSelected = value == 0;
            audioControls.EnableAudioSourceControls(audioSourceSelected);
            foreach(var button in audioSourceRotationList)
            {
                button.gameObject.SetActive(audioSourceSelected);
            }
            audioSourceRotationText.gameObject.SetActive(audioSourceSelected);
            rotationSpeedInputField.gameObject.SetActive(audioSourceSelected);
        }

        private void SetUI()
        {
            _selectedGameObjectPosition = _selectedGameObjet.transform.position;
            transformInputField[0].text = _selectedGameObjectPosition.x.ToString();
            transformInputField[1].text = _selectedGameObjectPosition.y.ToString();
            transformInputField[2].text = _selectedGameObjectPosition.z.ToString();
            rotationSpeedInputField.text = audioSourceBehaviour.RotationSpeed.ToString();
        }

        public void HandleXValueChanged(string value)
        {
            _selectedGameObjet.transform.position = new Vector3(float.Parse(value), _selectedGameObjectPosition.y, _selectedGameObjectPosition.z);
        }
        
        public void HandleYValueChanged(string value)
        {
            _selectedGameObjet.transform.position = new Vector3(_selectedGameObjectPosition.x, float.Parse(value),  _selectedGameObjectPosition.z);
        }
        
        public void HandleZValueChanged(string value)
        {
            _selectedGameObjet.transform.position = new Vector3(_selectedGameObjectPosition.x, _selectedGameObjectPosition.y, float.Parse(value));
        }

        public void OnClickAzimuth()
        {
            _azimuthClicked = !_azimuthClicked;
            _elevationClicked = false;
            audioSourceBehaviour.Rotating = _azimuthClicked;
            if(_azimuthClicked)
                audioSourceBehaviour.RotationDirection = Vector3.up;
            else
                eventSystem.SetSelectedGameObject(null);
        }

        public void OnClickElevation()
        {
            _elevationClicked = !_elevationClicked;
            _azimuthClicked = false;
            audioSourceBehaviour.Rotating = _elevationClicked;
            if(_elevationClicked)
                audioSourceBehaviour.RotationDirection = Vector3.left;
            else
                eventSystem.SetSelectedGameObject(null);
        }
        
        public void HandleRotationSpeedValueChanged(string value)
        {
            audioSourceBehaviour.RotationSpeed = float.Parse(value);
        }
    }
}
