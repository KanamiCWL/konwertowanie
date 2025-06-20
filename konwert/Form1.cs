using System;
using System.Windows.Forms;


    namespace konwert
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
                InitializeCategories();
            }

            private void InitializeCategories()
            {
                comboCategory.Items.Add("Długość");
                comboCategory.Items.Add("Waga");
                comboCategory.Items.Add("Temperatura");
                comboCategory.SelectedIndex = 0; // domyślnie długość
            }

            private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
                comboFrom.Items.Clear();
                comboTo.Items.Clear();

                switch (comboCategory.SelectedItem.ToString())
                {
                    case "Długość":
                        comboFrom.Items.AddRange(new object[] { "metry (m)", "kilometry (km)", "mile (mi)" });
                        comboTo.Items.AddRange(new object[] { "metry (m)", "kilometry (km)", "mile (mi)" });
                        break;
                    case "Waga":
                        comboFrom.Items.AddRange(new object[] { "kilogramy (kg)", "funty (lb)" });
                        comboTo.Items.AddRange(new object[] { "kilogramy (kg)", "funty (lb)" });
                        break;
                    case "Temperatura":
                        comboFrom.Items.AddRange(new object[] { "Celsjusz (°C)", "Fahrenheit (°F)" });
                        comboTo.Items.AddRange(new object[] { "Celsjusz (°C)", "Fahrenheit (°F)" });
                        break;
                }

                comboFrom.SelectedIndex = 0;
                comboTo.SelectedIndex = 1;
                lblResult.Text = "";
            }

            private void btnConvert_Click(object sender, EventArgs e)
            {
                double value;
                if (!double.TryParse(txtValue.Text, out value))
                {
                    MessageBox.Show("Wprowadź poprawną liczbę.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string category = comboCategory.SelectedItem.ToString();
                string fromUnit = comboFrom.SelectedItem.ToString();
                string toUnit = comboTo.SelectedItem.ToString();

                double result = double.NaN;

                if (category == "Długość")
                {
                    double meters = ConvertLengthToMeters(value, fromUnit);
                    result = ConvertMetersToLength(meters, toUnit);
                }
                else if (category == "Waga")
                {
                    double kg = ConvertWeightToKg(value, fromUnit);
                    result = ConvertKgToWeight(kg, toUnit);
                }
                else if (category == "Temperatura")
                {
                    result = ConvertTemperature(value, fromUnit, toUnit);
                }

                if (double.IsNaN(result))
                {
                    lblResult.Text = "Niepoprawne jednostki.";
                }
                else
                {
                    lblResult.Text = string.Format("{0} {1} = {2:F4} {3}", value, fromUnit, result, toUnit);
                }
            }

            private double ConvertLengthToMeters(double val, string unit)
            {
                if (unit.StartsWith("metry"))
                    return val;
                if (unit.StartsWith("kilometry"))
                    return val * 1000;
                if (unit.StartsWith("mile"))
                    return val * 1609.34;
                return double.NaN;
            }

            private double ConvertMetersToLength(double val, string unit)
            {
                if (unit.StartsWith("metry"))
                    return val;
                if (unit.StartsWith("kilometry"))
                    return val / 1000;
                if (unit.StartsWith("mile"))
                    return val / 1609.34;
                return double.NaN;
            }

            private double ConvertWeightToKg(double val, string unit)
            {
                if (unit.StartsWith("kilogramy"))
                    return val;
                if (unit.StartsWith("funty"))
                    return val * 0.453592;
                return double.NaN;
            }

            private double ConvertKgToWeight(double val, string unit)
            {
                if (unit.StartsWith("kilogramy"))
                    return val;
                if (unit.StartsWith("funty"))
                    return val / 0.453592;
                return double.NaN;
            }

            private double ConvertTemperature(double val, string from, string to)
            {
                if (from.StartsWith("Celsjusz") && to.StartsWith("Fahrenheit"))
                    return val * 9 / 5 + 32;
                if (from.StartsWith("Fahrenheit") && to.StartsWith("Celsjusz"))
                    return (val - 32) * 5 / 9;
                if (from == to)
                    return val;

                return double.NaN;
            }
        }
    }
