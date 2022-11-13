using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

namespace Script
{
    public class AudioControlUI : MonoBehaviour
    {
        [SerializeField] private GameObject audioSourceControlUI;
        [SerializeField] private AudioSourceBehavior audioSourceBehavior;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button playButton;
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private List<InputField> rollOffInputFields;
        [SerializeField] private InputField speedInputField;

        private void Update()
        {
            audioSource.volume = sliders[0].value;
            audioSource.pitch = sliders[1].value;
            audioSource.spatialBlend = sliders[2].value;
            audioSource.panStereo = sliders[3].value;
            audioSource.dopplerLevel = sliders[4].value;
            
        }

        public void EnableAudioSourceControls(bool value)
        {
            audioSourceControlUI.SetActive(value);
            SetUI();
        }

        private void SetUI()
        {
            rollOffInputFields[0].text = audioSource.minDistance.ToString();
            rollOffInputFields[1].text = audioSource.maxDistance.ToString();
            sliders[0].value = audioSource.volume;
            sliders[1].value = audioSource.pitch;
            sliders[2].value = audioSource.spatialBlend;
            sliders[3].value = audioSource.panStereo;
            sliders[4].value = audioSource.dopplerLevel;
            speedInputField.text = audioSourceBehavior.MoveSpeed.ToString();
        }

        public void OnClickPlayPause()
        {
            Debug.Log("click play");
            if(audioSource.isPlaying)
                audioSource.Pause();
            else
                audioSource.Play();
        }

        public void HandleSpeedInput(string value)
        {
            audioSourceBehavior.MoveSpeed = float.Parse(value);
        }

        public void HandleMinRollOffInput(string value)
        {
            audioSource.minDistance = float.Parse(value);
        }
        
        public void HandleMaxRollOffInput(string value)
        {
            audioSource.maxDistance = float.Parse(value);
        }
        
        public void HandleVolumeInput(float value)
        {
            Debug.Log("volume change " + value);
            audioSource.volume = value;
        }
        
        public void HandlePitchInput(float value)
        {
            audioSource.pitch = value;
        }
        
        public void HandleBlendInput(float value)
        {
            audioSource.spatialBlend = value;
        }
        
        public void HandleStereoInput(float value)
        {
            audioSource.panStereo = value;
        }
        
        public void HandleDopplerInput(float value)
        {
            audioSource.dopplerLevel = value;
        }
        
    }
}
