using System;
using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class Dosage
    {
        [SerializeField] public int medicineId;
        [SerializeField] public int diseaseId;
        [SerializeField] public float minDosage;
        [SerializeField] public float maxDosage;
    }
}