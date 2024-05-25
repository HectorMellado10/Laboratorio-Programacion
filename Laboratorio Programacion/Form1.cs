using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TuNamespace;

namespace Laboratorio_Programacion
{
    public partial class Form1 : Form
    {
        private int currentIndex = -1;
        private List<CatalogoConsolas> consolas = new List<CatalogoConsolas>();
        string connectionString = "Server=localhost;Database=Laboratorio progra;Uid=root;Pwd=Benjy134;";

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadComboBox();
            LoadData();
        }

        private void InitializeCustomComponents()
        {
            // Label
            Label labelInfo = new Label
            {
                Text = "Hector Luis Gustavo Mellado, 0905-23-796 y mi compañero: Pablo Grijalva, 0905-21-4966",
                Font = new System.Drawing.Font("Arial", 20),
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            Controls.Add(labelInfo);

            // DataGridView
            DataGridView dataGridViewConsolas = new DataGridView
            {
                Name = "dataGridViewConsolas",
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(600, 200)
            };
            Controls.Add(dataGridViewConsolas);

            // ComboBox
            ComboBox comboBoxCompanias = new ComboBox
            {
                Name = "comboBoxCompanias",
                Location = new System.Drawing.Point(20, 280),
                Size = new System.Drawing.Size(200, 30)
            };
            Controls.Add(comboBoxCompanias);

            // Buttons
            Button buttonSelectAll = new Button
            {
                Text = "Seleccionar Todo",
                Location = new System.Drawing.Point(20, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonSelectAll.Click += new EventHandler(buttonSelectAll_Click);
            Controls.Add(buttonSelectAll);

            Button buttonNextRecord = new Button
            {
                Text = "Siguiente Registro",
                Location = new System.Drawing.Point(160, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonNextRecord.Click += new EventHandler(buttonNextRecord_Click);
            Controls.Add(buttonNextRecord);

            Button buttonFilterNintendo = new Button
            {
                Text = "Filtrar Nintendo",
                Location = new System.Drawing.Point(300, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonFilterNintendo.Click += new EventHandler(buttonFilterNintendo_Click);
            Controls.Add(buttonFilterNintendo);

            Button buttonFilterSega = new Button
            {
                Text = "Filtrar Sega",
                Location = new System.Drawing.Point(440, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonFilterSega.Click += new EventHandler(buttonFilterSega_Click);
            Controls.Add(buttonFilterSega);

            Button buttonFilterSony = new Button
            {
                Text = "Filtrar Sony",
                Location = new System.Drawing.Point(580, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonFilterSony.Click += new EventHandler(buttonFilterSony_Click);
            Controls.Add(buttonFilterSony);
        }

        private void LoadComboBox()
        {
            ComboBox comboBoxCompanias = Controls.Find("comboBoxCompanias", true).FirstOrDefault() as ComboBox;
            if (comboBoxCompanias != null)
            {
                comboBoxCompanias.Items.AddRange(new string[] { "Atari", "Coleco", "Mattel", "Microsoft", "Nintendo", "Ouya Inc.", "Sega", "Sony" });
            }
        }

        private void LoadData()
        {
            DataGridView dataGridViewConsolas = Controls.Find("dataGridViewConsolas", true).FirstOrDefault() as DataGridView;
            if (dataGridViewConsolas != null)
            {
                consolas = GetConsolasFromDatabase();
                dataGridViewConsolas.DataSource = consolas;
            }
        }

        private List<CatalogoConsolas> GetConsolasFromDatabase()
        {
            List<CatalogoConsolas> consolas = new List<CatalogoConsolas>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM catalogo_consolas";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CatalogoConsolas consola = new CatalogoConsolas
                        {
                            IdConsola = reader.GetInt32("id_consola"),
                            NombreConsola = reader.GetString("nombre_consola"),
                            Compania = reader.GetString("compania"),
                            AnioLanzamiento = reader.GetInt32("anio_lanzamiento"),
                            Generacion = reader.GetByte("generacion")
                        };
                        consolas.Add(consola);
                    }
                }
            }
            return consolas;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonNextRecord_Click(object sender, EventArgs e)
        {
            if (consolas.Count == 0) return;

            currentIndex = (currentIndex + 1) % consolas.Count;
            CatalogoConsolas consola = consolas[currentIndex];

            MessageBox.Show($"Consola: {consola.NombreConsola}, Compañía: {consola.Compania}, Año: {consola.AnioLanzamiento}, Generación: {consola.Generacion}");
        }

        private void buttonFilterNintendo_Click(object sender, EventArgs e)
        {
            var filteredConsolas = consolas.Where(c => c.Compania == "Nintendo").ToList();
            UpdateDataGridView(filteredConsolas);
        }

        private void buttonFilterSega_Click(object sender, EventArgs e)
        {
            var filteredConsolas = consolas.Where(c => c.Compania == "Sega").ToList();
            UpdateDataGridView(filteredConsolas);
        }

        private void buttonFilterSony_Click(object sender, EventArgs e)
        {
            var filteredConsolas = consolas.Where(c => c.Compania == "Sony").ToList();
            UpdateDataGridView(filteredConsolas);
        }

        private void UpdateDataGridView(List<CatalogoConsolas> data)
        {
            DataGridView dataGridViewConsolas = Controls.Find("dataGridViewConsolas", true).FirstOrDefault() as DataGridView;
            if (dataGridViewConsolas != null)
            {
                dataGridViewConsolas.DataSource = data;
            }
        }
    }
}