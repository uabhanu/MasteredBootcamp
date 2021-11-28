using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class DayNightData : ScriptableObject
    {
        [SerializeField] private float dayLightIntensityValue;
        [SerializeField] private float nightLightIntensityValue;
        [SerializeField] private int nightHour;
        [SerializeField] private Material skyBoxDayMaterial;
        [SerializeField] private Material skyBoxNightMaterial;

        public float DayLightIntensityValue
        {
            get => dayLightIntensityValue;
            set => dayLightIntensityValue = value;
        }
        
        public int NightHour
        {
            get => nightHour;
            set => nightHour = value;
        }

        public float NightLightIntensityValue
        {
            get => nightLightIntensityValue;
            set => nightLightIntensityValue = value;
        }
        
        public Material SkyBoxDayMaterial
        {
            get => skyBoxDayMaterial;
            set => skyBoxDayMaterial = value;
        }

        public Material SkyBoxNightMaterial
        {
            get => skyBoxNightMaterial;
            set => skyBoxNightMaterial = value;
        }
    }
}
