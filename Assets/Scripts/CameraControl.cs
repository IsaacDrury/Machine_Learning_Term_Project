using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private GameObject camPanel;
        [SerializeField] private GameObject mainCam;
        [SerializeField] private List<GameObject> team1;
        [SerializeField] private List<GameObject> team2;
        [SerializeField] private GameObject currentCam;
        [SerializeField] private string team1Tag;
        [SerializeField] private string team2Tag;

        private int index1;
        private int index2;

        public void Start()
        {
            GetCams();
        }

        public void GetCams()
        {
            currentCam = mainCam;
            currentCam.SetActive(true);
            GameObject[] cams = GameObject.FindGameObjectsWithTag("Agent");
            team1 = new List<GameObject>();
            team2 = new List<GameObject>();
            foreach (GameObject cam in cams)
            {
                if (cam.transform.parent.gameObject.tag == team1Tag)
                {
                    team1.Add(cam);
                    cam.SetActive(false);
                }
                else if (cam.transform.parent.gameObject.tag == team2Tag)
                {
                    team2.Add(cam);
                    cam.SetActive(false);
                }
            }
            index1 = 0;
            index2 = 0;
        }

        public void UseMainCam()
        {
            currentCam.SetActive(false);
            currentCam = mainCam;
            currentCam.SetActive(true);
            index1 = 0;
            index2 = 0;
        }

        public void UseTeam1Cams()
        {
            if (index1 == team1.Count)
            {
                index1 = 0;
            }
            if (team1[index1] != null && team1[index1].transform.parent.gameObject.activeSelf)
            {
                currentCam.SetActive(false);
                currentCam = team1[index1];
                currentCam.SetActive(true);
                index2 = 0;
                index1 += 1;
            }
        }

        public void UseTeam2Cams()
        {
            if (index2 == team2.Count)
            {
                index2 = 0;
            }
            if (team2[index2] != null && team2[index2].transform.parent.gameObject.activeSelf)
            {
                currentCam.SetActive(false);
                currentCam = team2[index2];
                currentCam.SetActive(true);
                index1 = 0;
                index2 += 1;
            }
        }

        public void ToggleCamPanel()
        {
            camPanel.SetActive(!camPanel.activeSelf);
        }
    }
}