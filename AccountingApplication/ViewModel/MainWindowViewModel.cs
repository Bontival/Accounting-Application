using AccountingApplication.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using AccountingApplication.Model;
using System.Collections;
using Google.Protobuf.WellKnownTypes;
using System.ComponentModel;
using Google.Protobuf.Compiler;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AccountingApplication.Commands;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using Org.BouncyCastle.Utilities;

namespace AccountingApplication.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public UserViewModel CurrentUser { get; private set; }
        private MySqlConnection connection;
        public IWindowService WindowService { get; }
        public MaterialTableViewModel CurrentViewModel { get; }
        public ObservableCollection<MaterialsViewModel> Materials { get; }
        public ObservableCollection<StorekeeperViewModel> Storekeepers { get; }
        public ObservableCollection<WarehouseViewModel> Warehouses2 { get; private set; }
        public ObservableCollection<WarehouseViewModel> Warehouses { get; }
        public ObservableCollection<MaterialCollectionViewModel> MaterialList { get; }
        public ObservableCollection<LogViewModel> Logs { get; }
        public List<UserViewModel> Users { get; }
        public ICommand AddElement { get; }
        public ICommand DeleteElement { get; }
        public ICommand RefreshMaterials { get; }
        public ICommand ShowLogs { get; }
        public MainWindowViewModel()
        {
            Materials = new ObservableCollection<MaterialsViewModel>();
            Storekeepers = new ObservableCollection<StorekeeperViewModel>();
            Warehouses = new ObservableCollection<WarehouseViewModel>();
            Warehouses2 = new ObservableCollection<WarehouseViewModel>();
            MaterialList = new ObservableCollection<MaterialCollectionViewModel>();
            Users = new List<UserViewModel>();
            CurrentViewModel = new MaterialTableViewModel(this, MaterialList.ToList());
            Logs = new ObservableCollection<LogViewModel>();
            WindowService = new WindowService(Logs);

            AddElement = new AddElementCommand(this);
            DeleteElement = new DeleteElementCommand(this);
            RefreshMaterials = new RefreshMaterialsCommand(this);
            ShowLogs = new ShowLogsCommand(this);
        }
        public bool OpenConnection(string ip)
        {
            try
            {
                connection = new MySqlConnection();
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Add("SERVER", ip);
                builder.Add("DATABASE", "accounting_db");
                builder.Add("UID", "root");
                builder.Add("PASSWORD", "2ng02gnxs02n3df");
                connection.ConnectionString = builder.ConnectionString;
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void FillViewModels()
        {
            IDictionary<string, DataTable> Tables = LoadDataFromDB();
            foreach (DataRow row in Tables["material"].Rows)
            {
                Materials.Add(new MaterialsViewModel(new Material(
                    row.Field<string>("material"),
                    row.Field<int>("id")
                    )));
            }
            foreach (DataRow row in Tables["storekeeper"].Rows)
            {
                Storekeepers.Add(new StorekeeperViewModel(new Storekeeper(
                    row.Field<string>("name"),
                    row.Field<int>("id")
                    )));
            }
            foreach (DataRow row in Tables["warehouse"].Rows)
            {
                Warehouses.Add(new WarehouseViewModel(new Warehouse(
                    row.Field<string>("name"),
                    row.Field<int>("id")
                    )));
            }
            foreach (DataRow row in Tables["material_list"].Rows)
            {
                var tempId= row.Field<int>("id");
                var m = new Material(Materials.FirstOrDefault(i => i.Id == row.Field<int>("materials_id")).Name,
                        Materials.FirstOrDefault(i => i.Id == row.Field<int>("materials_id")).Id);
                var s = new Storekeeper(Storekeepers.FirstOrDefault(i => i.Id == row.Field<int>("storekeeper_id")).Name,
                        Storekeepers.FirstOrDefault(i => i.Id == row.Field<int>("storekeeper_id")).Id);
                var w = new Warehouse(Warehouses.FirstOrDefault(i => i.Id == row.Field<int>("warehouse_id")).Name,
                        Warehouses.FirstOrDefault(i => i.Id == row.Field<int>("warehouse_id")).Id);
                MaterialList.Add(new MaterialCollectionViewModel(new MaterialCollection(m, s, w,
                    row.Field<int>("amount")
                    )));
            }
            foreach (DataRow row in Tables["users"].Rows)
            {
                Users.Add(new UserViewModel(new User(
                    row.Field<string>("username"),
                    row.Field<string>("password"),
                    row.Field<int>("permissions"),
                    row.Field<int?>("warehouse_id")
                    )));
            }
            foreach (DataRow row in Tables["log"].Rows)
            {
                Logs.Insert(0, new LogViewModel(new Log(
                    row.Field<string>("change"),
                    row.Field<DateTime>("date")
                    )));
            }
        }
        public IDictionary<string, DataTable> LoadDataFromDB()
        {
            List<string> keys = new List<string>()
            {
                "material",
                "storekeeper",
                "warehouse",
                "material_list",
                "users",
                "log"
            };
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            IDictionary<string, DataTable> Tables = new Dictionary<string, DataTable>();

            foreach (string key in keys)
            {
                command.CommandText = "SELECT * FROM " + key;
                command.ExecuteNonQuery();
                DataTable i_dt = new DataTable();
                i_dt.Load(command.ExecuteReader());
                Tables.Add(key, i_dt);
            }
            return Tables;
        }
        public void ValidateLogin(UserViewModel tempUser)
        {
            CurrentUser = tempUser;
            if (tempUser.Permissions == 1)
            {
                var tempid = tempUser.WarehouseId;
                var tempWarehouse = Warehouses.FirstOrDefault(i => i.Id == tempUser.WarehouseId);
                string tempWarehouseName = tempWarehouse.Name;
                CurrentViewModel.UpdateMaterialListTable(MaterialList, CurrentUser, Warehouses);
                Warehouses2.Add(tempWarehouse);
            }
            else if (tempUser.Permissions > 1)
            {
                foreach (var warehouse in Warehouses)
                {
                    Warehouses2.Add(warehouse);
                }
                CurrentViewModel.UpdateMaterialListTable(MaterialList, CurrentUser, Warehouses);
                if (tempUser.Permissions > 2)
                {
                    CanClose = true;
                }
            }
        }
        private string selectedWarehouse;
        public string SelectedWarehouse
        {
            get { return selectedWarehouse; }
            set
            {
                selectedWarehouse = value;
                OnPropertyChanged(nameof(SelectedWarehouse));
            }
        }
        private string selectedMaterial;
        public string SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;
                OnPropertyChanged(nameof(SelectedMaterial));
            }
        }
        private string selectedAmount;
        public string SelectedAmount
        {
            get { return selectedAmount; }
            set
            {
                selectedAmount = value;
                OnPropertyChanged(nameof(SelectedAmount));
            }
        }
        private string selectedStorekeeper;
        public string SelectedStorekeeper
        {
            get { return selectedStorekeeper; }
            set
            {
                selectedStorekeeper = value;
                OnPropertyChanged(nameof(SelectedStorekeeper));
            }
        }
        private bool canClose = false;
        public bool CanClose
        {
            get { return canClose; }
            set
            {
                canClose = value;
                OnPropertyChanged(nameof(CanClose));
            }
        }
        public void AddMaterialListToDb(MaterialsViewModel _tempMaterial, StorekeeperViewModel tempStorekeeper, WarehouseViewModel tempWarehouse, int amount)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            string txt = "INSERT INTO material_list (amount, warehouse_id, materials_id, storekeeper_id) " +
                "VALUES " + "(\"" + amount.ToString() + "\",\"" + tempWarehouse.Id + "\",\"" + _tempMaterial.Id + "\",\"" + tempStorekeeper.Id + "\");";
            Console.WriteLine(txt);
            command.CommandText = txt;
            command.ExecuteNonQuery();
            var log = tempStorekeeper.Name + " добавил(а) " + _tempMaterial.Name + " в количестве " + amount.ToString() + " на " + tempWarehouse.Name;
            UpdateLogs(log, DateTime.Now);
        }
        public int AddMaterialToDb(string m)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO material (material) " +
                "VALUES (@M);";
            command.Parameters.Add("@M", MySqlDbType.VarChar);
            command.Parameters["@M"].Value = m;
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.LastInsertedId);
        }
        public void UpdateMaterialListDb(MaterialCollectionViewModel tempMaterialCollection)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE material_list " +
                "SET storekeeper_id = " + Storekeepers.FirstOrDefault(i => i.Name == tempMaterialCollection.Storekeeper).Id.ToString() + ", " +
                "amount = " + tempMaterialCollection.Amount.ToString() + " " +
                "WHERE materials_id = " + Materials.FirstOrDefault(i => i.Name == tempMaterialCollection.Material).Id.ToString() + " AND warehouse_id = " +
                Warehouses.FirstOrDefault(i => i.Name == tempMaterialCollection.Warehouse).Id.ToString() + ";";
            command.ExecuteNonQuery();
            var log = tempMaterialCollection.Storekeeper + " обновил количество материала (" + tempMaterialCollection.Amount.ToString() + ") " + tempMaterialCollection.Material + " на складе " + tempMaterialCollection.Warehouse;
            UpdateLogs(log, DateTime.Now);
        }
        public void DeleteElementFromDb(MaterialCollectionViewModel tempMaterialCollection)
        {
            var warehouse = Warehouses.FirstOrDefault(i=>i.Name == tempMaterialCollection.Warehouse);
            var storekeeper = Storekeepers.FirstOrDefault(i => i.Name == tempMaterialCollection.Storekeeper);
            var material = Materials.FirstOrDefault(i => i.Name == tempMaterialCollection.Material);
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM material_list " +
                "WHERE warehouse_id = " + warehouse.Id.ToString() + " and storekeeper_id = " + storekeeper.Id.ToString() + " and materials_id = " + material.Id.ToString() + ";";
            command.ExecuteNonQuery();
            var log = storekeeper.Name + " удалил(а) " + material.Name + " со склада " + warehouse.Name;
            UpdateLogs(log, DateTime.Now);
        }
        public int DeleteMaterialFromDb(MaterialsViewModel materialsViewModel)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM material " +
                "WHERE id = " + materialsViewModel.Id + ";";
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.LastInsertedId);
        }
        public void UpdateLogs(string change, DateTime dateTime)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO log (log.change, date) " +
                "VALUES (@CHANGE,@DATETIME);";
            command.Parameters.Add("@CHANGE", MySqlDbType.VarChar);
            command.Parameters.Add("@DATETIME", MySqlDbType.DateTime);
            command.Parameters["@CHANGE"].Value = change;
            command.Parameters["@DATETIME"].Value = dateTime;
            command.ExecuteNonQuery();
            Logs.Insert(0,new LogViewModel(new Log(change, dateTime)));
        }
    }
}
