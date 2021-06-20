using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Tasks
{
    public class ValveTask : Task
    {
        public GameObject eau1;
        public GameObject eau2;
        public GameObject eau3;
        public GameObject eau4;

        public GameObject valve1Verte;
        public GameObject valve1Orange;
        public GameObject valve1Rouge;

        public GameObject valve2Verte;
        public GameObject valve2Orange;

        public GameObject valve3Verte;
        public GameObject valve3Rouge;

        public GameObject valve4Rouge;
        public GameObject valve4Orange;

        private List<(List<GameObject>, List<GameObject>)> tasks;

        private List<GameObject> yaPlusDeau;
        private List<GameObject> valvesATourner;
        private List<GameObject> valvesDejaTournees;

        private int iValve;

        public new Animation animation;

        private void Start()
        {
            this.nom = "Salle du traitement des eaux";
            iValve = 0;
            tasks = new List<(List<GameObject>, List<GameObject>)>
            {
                (new List<GameObject> {eau1, eau3}, new List<GameObject> {valve3Rouge, valve4Orange}),
                (new List<GameObject> {eau2}, new List<GameObject> {valve3Verte, valve2Orange, valve1Rouge}),
                (new List<GameObject> {eau2, eau4}, new List<GameObject> {valve4Rouge, valve1Orange, valve1Verte, valve3Rouge}),
                (new List<GameObject> {eau4}, new List<GameObject> {valve4Orange, valve3Rouge}),
                (new List<GameObject> {eau1, eau2}, new List<GameObject> {valve1Verte, valve1Rouge, valve2Verte}),
                (new List<GameObject> {eau1, eau2, eau3}, new List<GameObject> {valve2Orange, valve1Orange, valve4Orange, valve3Rouge}),
            };

            int idTask = new Random().Next(tasks.Count);
            yaPlusDeau = tasks[idTask].Item1;
            valvesATourner = tasks[idTask].Item2;
            valvesDejaTournees = new List<GameObject>();

            foreach (GameObject eau in yaPlusDeau)
                eau.SetActive(false);
        }

        public void TurnValve(Transform valveTransform)
        {
            if (!animation.isPlaying)
            {
                animation.Play(valveTransform.name);
                if (valvesATourner[iValve] == valveTransform.gameObject)
                {
                    iValve++;
                    valvesDejaTournees.Add(valveTransform.gameObject);
                    if (valvesDejaTournees.Count == valvesATourner.Count)
                    {
                        //mission réussie
                        //GameObject.Find("ecran mission").GetComponent<ClasseDeNathDeLecranMission>().MissionValveFinie();
                    }
                }
                else if (!valvesDejaTournees.Contains(valveTransform.gameObject))
                {
                    iValve = 0;
                }
            }
        }
    }
}