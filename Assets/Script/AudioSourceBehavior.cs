using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class AudioSourceBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject cameraTarget;
        public float MoveSpeed { get; set; }
        
        public bool Rotating { get; set; }

        public float RotationSpeed { get; set; }

        public Vector3 RotationDirection { get; set; }
       
        
        // Start is called before the first frame update
        private void Start()
        {
            RotationSpeed = 20;
            MoveSpeed = 50;
        }

        // Update is called once per frame
        private void Update()
        {
            if(Rotating)
                transform.RotateAround(cameraTarget.transform.position, RotationDirection, RotationSpeed * Time.deltaTime);
            else
            {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");
                var transform1 = transform;
                transform1.position += new Vector3(x * MoveSpeed * Time.deltaTime,
                    y * MoveSpeed * Time.deltaTime, 0);
            }
           

        }
    }
}
