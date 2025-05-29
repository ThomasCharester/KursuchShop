using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Resources.Code.Panels
{
    class Zone
    {
        public Vector2 Center;
        public float Radius;
        public float Liters;

        public Zone(Vector2 center, float radius, float liters)
        {
            this.Center = center;
            this.Radius = radius;
            this.Liters = liters;
        }
    }

    public class PlantProcessPanel : Panel
    {
        [SerializeField] private TMP_Text plant;
        [SerializeField] private TMP_Text disease;
        [SerializeField] private TMP_Text medicine;
        [SerializeField] private TMP_Text dosage;
        [SerializeField] private TMP_Text liters;
        [SerializeField] private RectTransform zonePrefab;
        [SerializeField] private RectTransform display;
        
        List<RectTransform> _zoneObjects = new();
        public new void Show()
        {
            base.Show();

            Clear();
        }
        public new void Hide()
        {
            base.Hide();

            Clear();
        }

        private List<Zone> _zones = new();

        public void ShowInfo(string info)
        {
            _zones.Clear();
            int zonesCount = int.Parse(info.Split(DataParsingExtension.AdditionalQuerySplitter)[1]);
            float totalLiters = 0;
            for (int i = 2; i < 2 + zonesCount; i++)
            {
                string zoneInfo = info.Split(DataParsingExtension.AdditionalQuerySplitter)[i];
                _zones.Add(new(
                    new(float.Parse(
                            zoneInfo.Split(DataParsingExtension.QuerySplitter)[0]
                                .Split(DataParsingExtension.ValueSplitter)[0],
                            CultureInfo.InvariantCulture),
                        float.Parse(
                            zoneInfo.Split(DataParsingExtension.QuerySplitter)[0]
                                .Split(DataParsingExtension.ValueSplitter)[1],
                            CultureInfo.InvariantCulture)),
                    float.Parse(zoneInfo.Split(DataParsingExtension.QuerySplitter)[1], CultureInfo.InvariantCulture),
                    float.Parse(zoneInfo.Split(DataParsingExtension.QuerySplitter)[2], CultureInfo.InvariantCulture)));
                totalLiters += _zones.Last().Liters;
                
                var zone = Instantiate(zonePrefab, display.pivot, Quaternion.identity, display);
                zone.localScale *= _zones.Last().Radius / float.Parse(info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 3]);
                zone.localPosition = new(_zones.Last().Center.x * display.rect.width / 100.0f, _zones.Last().Center.y * display.rect.width / 100.0f, 0);
                
                _zoneObjects.Add(zone);
            }

            var test = info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 4]
                .Split(DataParsingExtension.QuerySplitter)[0];
            
            plant.text = SessionService
                .Plants[
                    int.Parse(info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 4]
                        .Split(DataParsingExtension.QuerySplitter)[0])].PlantName;
            disease.text = SessionService
                .Diseases[
                    int.Parse(info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 4]
                        .Split(DataParsingExtension.QuerySplitter)[1])].DiseaseName;
            medicine.text = SessionService
                .Medicines[
                    int.Parse(info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 4]
                        .Split(DataParsingExtension.QuerySplitter)[2])].MedicineName;
            
            dosage.text = info.Split(DataParsingExtension.AdditionalQuerySplitter)[zonesCount + 2];
            
            liters.text = totalLiters.ToString();
            
        }

        public void Clear()
        {
            plant.text = "";
            disease.text = "";
            medicine.text = "";
            dosage.text = "";
            liters.text = "";

            foreach (var zoneObject in _zoneObjects)
                Destroy(zoneObject.gameObject);
            
            _zones.Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);

            //Clear();
        }

        public new void Toggle()
        {
            base.Toggle();

            Clear();
        }
    }
}