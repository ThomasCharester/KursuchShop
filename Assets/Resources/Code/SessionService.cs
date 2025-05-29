using System.Collections.Generic;
using System.Linq;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;

namespace Resources.Code
{
    public class SessionService
    {
        private static List<PlantDisease> _plantsDiseases = new List<PlantDisease>();
        public static List<PlantDisease> PlantsDiseases => _plantsDiseases;

        public static void UpdatePlantsDiseases(string plants)
        {
            _plantsDiseases.Clear();

            _plantsDiseases.Add(new PlantDisease(0,0));
            
            if (plants.Length != 0)
                foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _plantsDiseases.Add(new PlantDisease(int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]),
                        int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[1])));
        }
        private static List<PlantMedicine> _plantsMedicines = new List<PlantMedicine>();
        public static List<PlantMedicine> PlantsMedicines => _plantsMedicines;

        public static void UpdatePlantsMedicines(string plants)
        {
            _plantsMedicines.Clear();

            _plantsMedicines.Add(new PlantMedicine(0,0));
            if (plants.Length != 0)
                foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _plantsMedicines.Add(new PlantMedicine(int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]),
                        int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[1])));
        }
        private static List<MedicineDisease> _medicinesDiseases = new List<MedicineDisease>();
        public static List<MedicineDisease> MedicinesDiseases => _medicinesDiseases;

        public static void UpdateMedicinesDiseases(string plants)
        {
            _medicinesDiseases.Clear();

            _medicinesDiseases.Add(new MedicineDisease(0,0,0,0));
            if (plants.Length != 0)
                foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _medicinesDiseases.Add(new MedicineDisease(
                        int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]),
                        int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[1]),
                        float.Parse(plant.Split(DataParsingExtension.ValueSplitter)[2]),
                        float.Parse(plant.Split(DataParsingExtension.ValueSplitter)[3])));
        }
        
        private static List<Plant> _plants = new List<Plant>();
        public static List<Plant> Plants => _plants;

        public static void UpdatePlants(string plants)
        {
            _plants.Clear();

            _plants.Add(new Plant(0,""));
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

            _diseases.Add(new Disease(0,""));
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

            _medicines.Add(new Medicine(0,"",0));
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