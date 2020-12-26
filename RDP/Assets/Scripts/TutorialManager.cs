using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image[] img;
    public static TutorialManager instance;
    public int Step = 0;   
    public GameObject[] movedGameObject;
    public Transform[] views;
    public float transitionSpeed;
    public Text instruction;
    bool[] justStarted = new bool[]{ true, true, true, true, true, true, true, true, true};
    public GameObject cavity;
    public GameObject etch;
    public Light roomLight;
    public GameObject healedTooth;
    public GameObject drilledTooth;
    public GameObject nextButton;
    bool finishedSettingUp = false;
    public bool playedSound = false;
    bool touched = false;
    void Start()
    {

    }

    void Awake(){
        if (instance != null){
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    void Update()
    {
        // instruction.text = movedGameObject[1].transform.position.x.ToString() + " " + movedGameObject[1].transform.position.y.ToString() + " " + movedGameObject[1].transform.position.z.ToString() + "\n" + views[1].position.x.ToString() + " " + views[1].position.y.ToString() + " " + views[1].position.z.ToString() + "\n" + movedGameObject[0].transform.position.x.ToString() + " " + movedGameObject[0].transform.position.y.ToString() + " " + movedGameObject[0].transform.position.z.ToString() + "\n" + views[0].position.x.ToString() + " " + views[0].position.y.ToString() + " " + views[0].position.z.ToString() + "\n" + views[2].position.x.ToString() + " " + views[2].position.y.ToString() + " " + views[2].position.z.ToString();
        if (Step == 1 && !inPlaced(movedGameObject[0], views[0])  && !inPlaced(movedGameObject[1], views[1])){
            img[1].enabled = true;
            instruction.text = "Step 1:\n\nPatient receives local anesthetic\nso that he will feel numb and\nwon’t feel any of the drilling or\nfilling.\n\nThe dentist will take the syringe\nand inject the local anesthetic into\nthe patient’s gum just below the\ntooth that we’ll be working on.\n\n\n\n\n";
            moveObject(movedGameObject[0], views[0]);
            moveObject(movedGameObject[1], views[1]);
        }
        if(Step == 1 && inPlaced(movedGameObject[0], views[0]) ){
            movedGameObject[0].transform.rotation = views[0].transform.rotation;
            movedGameObject[0].transform.position = views[0].position;
            movedGameObject[1].transform.rotation = views[1].transform.rotation;
            movedGameObject[1].transform.position = views[1].position;
            Step = 2;
        }
        if (Step == 2 && !inPlaced(movedGameObject[1], views[2])){
            StartCoroutine(waitAndMove(movedGameObject[1],views[2],0,2f));
            img[0].enabled = true;
        }
        if(Step == 2 && inPlaced(movedGameObject[1], views[2])){
            movedGameObject[1].transform.rotation = views[2].transform.rotation;
            movedGameObject[1].transform.position = views[2].position;
        }
        if(Step == 3 && !inPlaced(movedGameObject[0], views[3]) && !inPlaced(movedGameObject[1], views[4]) && !inPlaced(movedGameObject[2], views[5])){
            moveObject(movedGameObject[0], views[3]);
            moveObject(movedGameObject[1], views[4]);
            moveObject(movedGameObject[2], views[5]);
            img[0].enabled = false;
            img[1].enabled = false;
            img[2].enabled = true;
            img[3].enabled = true;
            instruction.text = "Step 2:\n\nRinse out the mouth with water\nand wait for the patient to\nstarting getting numb for a few\nminutes.\n\nOnce the patient is numb, use a\ndrill to remove the decayed area.\n\n\n\n\n";
        }
        if(Step == 3 && !(!inPlaced(movedGameObject[0], views[3]) && !inPlaced(movedGameObject[1], views[4]) && !inPlaced(movedGameObject[2], views[5]))){
            movedGameObject[0].transform.rotation = views[3].transform.rotation;
            movedGameObject[0].transform.position = views[3].position;
            movedGameObject[1].transform.rotation = views[4].transform.rotation;
            movedGameObject[1].transform.position = views[4].position;
            movedGameObject[2].transform.rotation = views[5].transform.rotation;
            movedGameObject[2].transform.position = views[5].position;
            FindObjectOfType<AudioManager>().Play("drillSound");
            Step = 4;
        }
        if (Step == 4 && !inPlaced(movedGameObject[2], views[6])){
            StartCoroutine(waitAndMove(movedGameObject[2],views[6],1,5f));
        }
        if(Step == 4 && inPlaced(movedGameObject[2], views[6])){
            movedGameObject[2].transform.rotation = views[6].transform.rotation;
            movedGameObject[2].transform.position = views[6].position;
        }
        if (Step == 5 && !inPlaced(movedGameObject[3], views[7])){
            moveObject(movedGameObject[3], views[7]);
            img[2].enabled = false;
            img[3].enabled = false;
            img[4].enabled = true;
            instruction.text = "Step 3:\n\nOnce all the decay has been\nremove the dentist will prepare\nthe space for the filling by placing\netch gel on the tooth.\n\nEtch is a blued colored acid gel\nand it’s used to roughen the\nsurface of the enamel. This\ncreates a rough surface that\nhelps the filling bond to the tooth.\n\n\n";
        }
        if(Step == 5 && inPlaced(movedGameObject[3], views[7])){
            movedGameObject[3].transform.rotation = views[7].transform.rotation;
            movedGameObject[3].transform.position = views[7].position;
            Step = 6;
        }
        if (Step == 6 && !inPlaced(movedGameObject[3], views[8])){
            StartCoroutine(waitAndMove(movedGameObject[3],views[8],2,3f));
        }
        if(Step == 6 && inPlaced(movedGameObject[3], views[8])){
            movedGameObject[3].transform.rotation = views[8].transform.rotation;
            movedGameObject[3].transform.position = views[8].position;
        }
        if (Step == 7){
            instruction.text = "Step 4:\n\nThe etch will then be rinse,\nsuctioned, and dried. The\nnext step is to apply the\nfilling onto the affected\ntooth. Hence, turn off\nthe bright light because\ntooth colored fillings are\nhardened by light.\n\n\n";
            roomLight.intensity = Mathf.Lerp(roomLight.intensity, 0.01f, Time.deltaTime);
            etch.SetActive(false);
        }
        if (Step == 8 && !inPlaced(movedGameObject[4], views[9])){
            moveObject(movedGameObject[4], views[9]);
            img[4].enabled = false;
            img[5].enabled = true;
            instruction.text = "Step 5:\n\nComposite filing is a type of filling\nthat applied in layers.\n\nTake the composite filling syringe\nand place the filling into the\nprepared portion of the tooth\n\n\n\n\n\n";
        }
        if(Step == 8 && inPlaced(movedGameObject[4], views[9])){
            movedGameObject[4].transform.rotation = views[9].transform.rotation;
            movedGameObject[4].transform.position = views[9].position;
            Step = 9;
        }
        if (Step == 9 && !inPlaced(movedGameObject[4], views[10])){
            StartCoroutine(waitAndMove(movedGameObject[4],views[10],3,4f));
        }
        if(Step == 9 && inPlaced(movedGameObject[4], views[10])){
            movedGameObject[4].transform.rotation = views[10].transform.rotation;
            movedGameObject[4].transform.position = views[10].position;
        }
        if (Step == 10 && !inPlaced(movedGameObject[5], views[11])){
            moveObject(movedGameObject[5], views[11]);
            img[5].enabled = false;
            img[6].enabled = true;
            instruction.text = "Step 6:\n\nPack the composite into the\nprepared area of the tooth using\na condenser.\n\nTry to shape the filling into the\nshape of the original tooth.\n\nWe are done with the procedure!\n\n\n\n\n";
        }
        if(Step == 10 && inPlaced(movedGameObject[5], views[11])){
            movedGameObject[5].transform.rotation = views[11].transform.rotation;
            movedGameObject[5].transform.position = views[11].position;
            Step = 11;
        }
        if (Step == 11 && !inPlaced(movedGameObject[5], views[12])){
            StartCoroutine(waitAndMove(movedGameObject[5],views[12],4,6f));
        }
        if(Step == 11 && inPlaced(movedGameObject[5], views[12])){
            movedGameObject[5].transform.rotation = views[12].transform.rotation;
            movedGameObject[5].transform.position = views[12].position;
        }
        if(Step == 12){
            roomLight.intensity = Mathf.Lerp(roomLight.intensity, 1.2f, Time.deltaTime);
            if (!finishedSettingUp){
                setUp();
            }
        }
        if (Step == 12 && movedGameObject[0].transform.position.x >= movedGameObject[1].transform.position.x-0.1f && movedGameObject[0].transform.position.x <= movedGameObject[1].transform.position.x+0.1f && movedGameObject[0].transform.position.y >= movedGameObject[1].transform.position.y-0.1f && movedGameObject[0].transform.position.y <= movedGameObject[1].transform.position.y+0.1f && movedGameObject[0].transform.position.z >= movedGameObject[1].transform.position.z-0.1f && movedGameObject[0].transform.position.z <= movedGameObject[1].transform.position.z+0.1f){
                Step = 13;
        }
        if (Step == 13){
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Now try to recall and repeat the\nprocess!\n\nStep 1: Completed\n\n\n\n\n\n\n\n\n\n\n";
        }
        if (Step == 13 && movedGameObject[6].transform.position.x >= movedGameObject[1].transform.position.x-0.1f && movedGameObject[6].transform.position.x <= movedGameObject[1].transform.position.x+0.1f && movedGameObject[6].transform.position.y >= movedGameObject[1].transform.position.y-0.1f && movedGameObject[6].transform.position.y <= movedGameObject[1].transform.position.y+0.1f && movedGameObject[6].transform.position.z >= movedGameObject[1].transform.position.z-0.1f && movedGameObject[6].transform.position.z <= movedGameObject[1].transform.position.z+0.1f){
            playedSound = false;
            Step = 14;
        }
        if (Step == 14){
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Now try to recall and repeat the\nprocess!\n\nStep 1: Completed\n\nStep 2: Completed\n\n\n\n\n\n\n\n";
        }
        if (Step == 14 && movedGameObject[2].transform.position.x >= movedGameObject[6].transform.position.x-0.1f && movedGameObject[2].transform.position.x <= movedGameObject[6].transform.position.x+0.1f && movedGameObject[2].transform.position.y >= movedGameObject[6].transform.position.y-0.1f && movedGameObject[2].transform.position.y <= movedGameObject[6].transform.position.y+0.1f && movedGameObject[2].transform.position.z >= movedGameObject[6].transform.position.z-0.1f && movedGameObject[2].transform.position.z <= movedGameObject[6].transform.position.z+0.1f){
            playedSound = false;
            FindObjectOfType<AudioManager>().Play("drillSound");
            Step = 15;
        }
        if (Step == 15){
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Now try to recall and repeat the\nprocess!\n\nStep 1: Completed\n\nStep 2: Completed\n\nStep 3: Completed\n\n\n\n\n";
            StartCoroutine(waitAndStopAudio(5,4f));
        }
        if (Step == 15 && movedGameObject[6].transform.position.x >= movedGameObject[3].transform.position.x-0.1f && movedGameObject[6].transform.position.x <= movedGameObject[3].transform.position.x+0.1f && movedGameObject[6].transform.position.y >= movedGameObject[3].transform.position.y-0.1f && movedGameObject[6].transform.position.y <= movedGameObject[3].transform.position.y+0.1f && movedGameObject[6].transform.position.z >= movedGameObject[3].transform.position.z-0.1f && movedGameObject[6].transform.position.z <= movedGameObject[3].transform.position.z+0.1f){
            playedSound = false;
            etch.SetActive(true);
            Step = 16;
        }
        if (Step == 16){
            roomLight.intensity = Mathf.Lerp(roomLight.intensity, 0.1f, Time.deltaTime);
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Now try to recall and repeat the\nprocess!\n\nStep 1: Completed\n\nStep 2: Completed\n\nStep 3: Completed\n\nStep 4: Completed\n\n";
            StartCoroutine(waitAndStopAudio(6,4f));
        }
        if (Step == 16 && movedGameObject[6].transform.position.x >= movedGameObject[4].transform.position.x-0.1f && movedGameObject[6].transform.position.x <= movedGameObject[4].transform.position.x+0.1f && movedGameObject[6].transform.position.y >= movedGameObject[4].transform.position.y-0.1f && movedGameObject[6].transform.position.y <= movedGameObject[4].transform.position.y+0.1f && movedGameObject[6].transform.position.z >= movedGameObject[4].transform.position.z-0.1f && movedGameObject[6].transform.position.z <= movedGameObject[4].transform.position.z+0.1f){
            playedSound = false;
            Step = 17;
        }
        if (Step == 17){
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Now try to recall and repeat the\nprocess!\n\nStep 1: Completed\n\nStep 2: Completed\n\nStep 3: Completed\n\nStep 4: Completed\n\nStep 5: Completed";
            StartCoroutine(waitAndStopAudio(7,2f));
        }
        if (Step == 17 && movedGameObject[6].transform.position.x >= movedGameObject[5].transform.position.x-0.1f && movedGameObject[6].transform.position.x <= movedGameObject[5].transform.position.x+0.1f && movedGameObject[6].transform.position.y >= movedGameObject[5].transform.position.y-0.1f && movedGameObject[6].transform.position.y <= movedGameObject[5].transform.position.y+0.1f && movedGameObject[6].transform.position.z >= movedGameObject[5].transform.position.z-0.1f && movedGameObject[6].transform.position.z <= movedGameObject[5].transform.position.z+0.1f){
            playedSound = false;
            Step = 18;
        }
        if (Step == 18){
            if (!playedSound){
                playSound("tingSound");
            }
            instruction.text = "Congratulations!\n\nYou have completed all steps.\n\n\n\n\n\n\n\n\n\n\n\n";
        }
    }

    void playSound(string name){
        FindObjectOfType<AudioManager>().Play(name);
        playedSound = true;
    }
    
    void setUp(){
        img[6].enabled = false;
        nextButton.SetActive(false);
        instruction.text = "Now try to recall and repeat the\nprocess!\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
        finishedSettingUp = true;
        healedTooth.SetActive(false);
        cavity.SetActive(true);
        drilledTooth.SetActive(true);
    }

    void moveObject(GameObject go, Transform view){
        go.transform.position = Vector3.Lerp(go.transform.position, view.position, Time.deltaTime * transitionSpeed);
        Vector3 currentAngle = new Vector3(
        Mathf.LerpAngle(go.transform.rotation.eulerAngles.x, view.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(go.transform.rotation.eulerAngles.y, view.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(go.transform.rotation.eulerAngles.z, view.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
        go.transform.eulerAngles = currentAngle;
    }

    IEnumerator waitAndMove(GameObject go, Transform view, int justStartedIndex, float time) 
    {
        if (justStarted[justStartedIndex]){
            yield return new WaitForSeconds(time);
            justStarted[justStartedIndex] = false;
        }
        else{
            moveObject(go, view);
            if (Step == 4){
                FindObjectOfType<AudioManager>().Stop("drillSound");
                cavity.SetActive(false);
            }
            if (Step == 6){
                etch.SetActive(true);
            }
            if (Step == 9){
                etch.SetActive(false);
                healedTooth.SetActive(true);
            }
            if (Step == 12){
                drilledTooth.SetActive(false);
            }
        }
    }
    IEnumerator waitAndStopAudio(int justStartedIndex, float time) 
    {
        if (justStarted[justStartedIndex]){
            yield return new WaitForSeconds(time);
            justStarted[justStartedIndex] = false;
        }
        else{
            if (Step == 15){
                FindObjectOfType<AudioManager>().Stop("drillSound");
                cavity.SetActive(false);
            }
            if (Step == 16){
                etch.SetActive(false);
            }
            if (Step == 17){
                etch.SetActive(false);
                healedTooth.SetActive(true);        
            }
        }
    }
    bool inPlaced(GameObject movedGameObject, Transform views){
        if (movedGameObject.transform.rotation.eulerAngles.x >= views.transform.rotation.eulerAngles.x-0.01f && movedGameObject.transform.rotation.eulerAngles.x <= views.transform.rotation.eulerAngles.x+0.01f && movedGameObject.transform.rotation.eulerAngles.y >= views.transform.rotation.eulerAngles.y-0.01f && movedGameObject.transform.rotation.eulerAngles.y <= views.transform.rotation.eulerAngles.y+0.01f && movedGameObject.transform.rotation.eulerAngles.z >= views.transform.rotation.eulerAngles.z-0.01f && movedGameObject.transform.rotation.eulerAngles.z <= views.transform.rotation.eulerAngles.z+0.01f && movedGameObject.transform.position.x >= views.position.x-0.01f && movedGameObject.transform.position.x <= views.position.x+0.01f && movedGameObject.transform.position.y >= views.position.y-0.01f && movedGameObject.transform.position.y <= views.position.y+0.01f && movedGameObject.transform.position.z >= views.position.z-0.01f && movedGameObject.transform.position.z <= views.position.z+0.01f){
            return true;
        }
        else{
            return false;
        }
    }
}