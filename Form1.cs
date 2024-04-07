namespace WinFormsAPI1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillGridView();
            FillEmployeeToDDl();
        }

        private void FillEmployeeToDDl()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:9999/api/Employees").Result;

            if (result.IsSuccessStatusCode)
            {
                var emps = result.Content.ReadAsAsync<List<Employee>>().Result;
                comboBox1.DataSource = emps;
                comboBox1.DisplayMember = "FirstName";
                comboBox1.ValueMember = "EmployeeID";
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private async Task FillGridView()
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:9999/api/Employees").Result;

            if (result.IsSuccessStatusCode)
            {
                var emps = result.Content.ReadAsAsync<List<Employee>>().Result;
                dataGridView1.DataSource = emps;
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private void buttAdd_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee
            {
                EmployeeID = int.Parse(textBox1.Text),
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
                Departmentid = int.Parse(textBox4.Text),
                Position = textBox5.Text,
                Salary = int.Parse(textBox6.Text),
            };
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9999");
            var result = client.PostAsJsonAsync("api/Employees", obj).Result;
            if (result.IsSuccessStatusCode)
            {
                FillGridView();
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = comboBox1.SelectedIndex;
            id++;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9999");
            var result = client.GetAsync($"api/Employees/{id}").Result;
            if (result.IsSuccessStatusCode)
            {
                var emps = result.Content.ReadAsAsync<Employee>().Result;
                textBox1.Text = emps.EmployeeID.ToString();
                textBox2.Text = emps.FirstName;
                textBox3.Text = emps.LastName;
                textBox4.Text = emps.Departmentid.ToString();
                textBox5.Text = emps.Position;
                textBox6.Text = emps.Salary.ToString();
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee
            {
                EmployeeID = int.Parse(textBox1.Text),
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
                Departmentid = int.Parse(textBox4.Text),
                Position = textBox5.Text,
                Salary = int.Parse(textBox6.Text),
            };

        }
    }
}