// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DetectTouch : MonoBehaviour
// {
//     public GameObject injection;
//     bool touched = false;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (TutorialManager.instance.Step == 12 && gameObject.transform.position.x >= injection.transform.position.x-0.2f && gameObject.transform.position.x <= injection.transform.position.x+0.2f && gameObject.transform.position.y >= injection.transform.position.y-0.2f && gameObject.transform.position.y <= injection.transform.position.y+0.2f && gameObject.transform.position.z >= injection.transform.position.z-0.2f && gameObject.transform.position.z <= injection.transform.position.z+0.2f){
//             if (!touched){
//                 setStep();
//             }
//         }
//     }
//     // void OnCollisionEnter(Collision collision)
//     // {
//     //     //Check for a match with the specified name on any GameObject that collides with your GameObject
//     //     if (collision.gameObject.name == "free_injector_FBX" && TutorialManager.instance.Step == 12)
//     //     {
//     //         TutorialManager.instance.Step = 13;
//     //     }

//     // }
//     void setStep(){
//         touched = true;
//         TutorialManager.instance.Step = 13;
//     }
// }
