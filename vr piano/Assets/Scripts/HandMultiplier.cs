using UnityEngine;
using UnityEditor.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Events;
using System.Threading;
namespace Valve.VR.InteractionSystem
{
    public class HandMultiplier : MonoBehaviour
    {
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean multiplyAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MultiplyHand");
        public SteamVR_Action_Boolean positionWestAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HandSpacingWest");
        public SteamVR_Action_Boolean positionEastAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HandSpacingEast");
        public SteamVR_Action_Boolean positionNorthAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HandSpacingNorth");
        public SteamVR_Action_Boolean positionSouthAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HandSpacingSouth");
        public SteamVR_Action_Boolean switchAction;
        public SteamVR_Action_Boolean deleteAction;
        public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");
        public GameObject duplicateHand;
        public float moveXValue = 0.005f;
        public float moveZValue = 0.005f;
        public float moveYValue = 0.005f;
        public float spawnX = 0.3f;
        GameObject settingHand;
        GameObject offHand;
        public List<GameObject> setHands;
        public List<GameObject> offHands;
        bool setting = false;
        
        
        // Update is called once per frame
        void Update()
        {
            CheckMultiply();
            
        }

        void StartMultiply()
        {
            settingHand = Instantiate(duplicateHand, transform.parent.transform);
            settingHand.transform.localPosition += new Vector3(300f, 0, 300f);
            settingHand.transform.GetChild(0).gameObject.GetComponent<HandPhysics>().enabled = false;
            offHand = settingHand;
            settingHand = Instantiate(duplicateHand, transform.parent.transform);
            settingHand.transform.localPosition += new Vector3(handType == SteamVR_Input_Sources.LeftHand ? -spawnX:spawnX , 0, 0);
            
            setting = true;
            Debug.Log("Setting...");
        }
        void EndMultiply()
        {
            setHands.Add(settingHand);
            offHands.Add(offHand);
            setting = false;
            Debug.Log("Done Setting");
        }
        void SetMultiply()
        {
            if (positionWestAction.GetState(handType))
            {
                settingHand.transform.localPosition = settingHand.transform.localPosition - new Vector3(moveXValue, 0f, 0f);
                Debug.Log("West");
            }
            else if (positionEastAction.GetState(handType))
            {
                settingHand.transform.localPosition = settingHand.transform.localPosition + new Vector3(moveXValue, 0f, 0f);
                Debug.Log("East");
            }
            if (positionNorthAction.GetState(handType))
            {
                if (switchAction.GetState(handType))
                {
                    settingHand.transform.localPosition = settingHand.transform.localPosition + new Vector3(0f, moveYValue, 0f);
                    Debug.Log("Up");
                }
                else
                {
                    settingHand.transform.localPosition = settingHand.transform.localPosition + new Vector3(0f, 0f, moveZValue);
                    Debug.Log("West");
                }
               
            }
            else if (positionSouthAction.GetState(handType))
            {
                
                if (switchAction.GetState(handType))
                {
                    settingHand.transform.localPosition = settingHand.transform.localPosition - new Vector3(0f, moveYValue, 0f);
                    Debug.Log("Down");
                }
                else
                {
                    settingHand.transform.localPosition = settingHand.transform.localPosition - new Vector3(0f, 0f, moveZValue);
                    Debug.Log("East");
                }
            }
        }
        void DeleteLastHand()
        {
            if (setting)
            {
                Destroy(settingHand);
                Destroy(offHand);
                setting = false;
            }
            else
            {
                Destroy(setHands[setHands.Count - 1]);
                setHands.RemoveAt(setHands.Count - 1);
                Destroy(offHands[offHands.Count - 1]);
                offHands.RemoveAt(offHands.Count - 1);
            }
        }
        void CheckMultiply()
        {
            if (setting && multiplyAction.GetStateUp(handType))
            {
                EndMultiply();

            }
            else if (multiplyAction.GetStateUp(handType))
            {
                StartMultiply();
            }
            else if (deleteAction.GetStateDown(handType))
            {
                DeleteLastHand();
            }
            else if (setting)
            {
                SetMultiply();
            }
            
        }
        
    }
}
