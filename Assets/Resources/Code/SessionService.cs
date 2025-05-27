using System.Collections.Generic;
using System.Linq;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;

namespace Resources.Code
{
    public class SessionService
    {
        private static List<Plant> _plants = new List<Plant>();
        public static List<Plant> Plants => _plants;

        public static void UpdatePlants(string plants)
        {
            _plants.Clear();

            if (plants.Length != 0)
                foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _plants.Add(new Plant(int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]),
                        plant.Split(DataParsingExtension.ValueSplitter)[1]));
        }
        public static int GetPlantIdByName(string name) => _plants.First(x => x.PlantName == name).Id;

        private static List<Disease> _diseases = new List<Disease>();
        public static List<Disease> Diseases => _diseases;

        public static void UpdateDiseases(string diseases)
        {
            _diseases.Clear();

            if (diseases.Length != 0)
                foreach (var disease in diseases.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _diseases.Add(new Disease(int.Parse(disease.Split(DataParsingExtension.ValueSplitter)[0]),
                        disease.Split(DataParsingExtension.ValueSplitter)[1]));
        }

        public static int GetDiseaseIdByName(string name) => _diseases.First(x => x.DiseaseName == name).Id;
        
        private static List<Medicine> _medicines = new List<Medicine>();
        public static List<Medicine> Medicines => _medicines;

        public static void UpdateMedicines(string medicines)
        {
            _medicines.Clear();

            if (medicines.Length != 0)
                foreach (var medicine in medicines.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _medicines.Add(new Medicine(int.Parse(medicine.Split(DataParsingExtension.ValueSplitter)[0]),
                        medicine.Split(DataParsingExtension.ValueSplitter)[1],
                        int.Parse(medicine.Split(DataParsingExtension.ValueSplitter)[2])));
        }

        public static int GetMedicineIdByName(string name) => _medicines.First(x => x.MedicineName == name).Id;
        
        private static Account _account;

        public static Account UserAccount
        {
            get { return _account; }
            private set { }
        }

        public static void SetAccount(Account account)
        {
            _account = account;
        }

        public static bool isAdmin()
        {
            return _account.sv_cheats;
        }
    }
}