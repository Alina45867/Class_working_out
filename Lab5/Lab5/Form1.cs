using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        static readonly Currency[] currencies = new Currency[3] //массив валют
        {
            new Currency() { Name = "RUB",Course = 1.0M}, new Currency(){ Name = "UAH",Course = 2.75M}, new Currency() { Name = "EUR",Course = 90.78M}
        };
        static account acc = new account(10000, currencies[0]);
        public Form1() // Загружаем коллекцию в ComboBox
        {
            InitializeComponent();
            comboBox1.Items.AddRange(currencies);
            comboBox4.Items.AddRange(currencies);
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Зачислить";
            button2.Text = "Снять";
            label1.Text = "Баланс";
            label2.Text = "Валюта";
            label3.Text = "Сумма";
            label4.Text = "Валюта";
            label5.Text = "Транзакция";
            label6.Text = "Состояние счета";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var resultat = decimal.TryParse(richTextBox2.Text, out decimal sum);
            if (!resultat || sum<=0)
            { 
                MessageBox.Show("Ошибка, неверная сумма транзакции");
                return;
            };
            if (!acc.Transaction((Currency)comboBox4.SelectedItem, sum))
                MessageBox.Show("вы хотите снять больше, чем есть на счете");
            richTextBox1.Text = acc.ToString(); //вывод нового баланса
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            acc.currensydisplay = (Currency)comboBox1.SelectedItem; // изменение комбо-бокс при смене валюты и вывод
            richTextBox1.Text = acc.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var resultat = decimal.TryParse(richTextBox2.Text, out decimal sum);
            if (!resultat || sum <= 0)
            { MessageBox.Show("Ошибка, неверная сумма транзакции");
                return;
            };
            if (!acc.Transaction((Currency)comboBox4.SelectedItem,-sum))
                MessageBox.Show("Вы хотите снять больше, чем у вас есть;(");
            richTextBox1.Text = acc.ToString();

        }
    }
    public class Currency // класс с валютой
    {
        private string name; // имя курса 
        private decimal course; //курс
        public string Name
        {
            get { return name; } //чтение и запись поля
            set { name = value; }
        }
        public decimal Course
        {
            get { return course; }
            set { course = value; }
        }
        public override string ToString()
        {                         // переопределенние метода, перевод в текст , вызов новой реализации
            return $"{Name}";
        }
    }
    public class account //класс аккаунта
    {
        private decimal balance; //баланс
        private Currency currency; //курс
        public Currency currensydisplay
        {
            set { currency = value; }
        }
        public account(decimal b, Currency c)
        {
            balance = b;
            currency = c;
        }
        public bool Transaction(Currency c, decimal am) //метод транзакции, проверка успешности перевода
        {
            decimal temp = balance + am * c.Course;
            if (temp < 0)
            {
                return false;
            }
            else balance = temp;
            return true;
        }
        public override string ToString() //переопределение, перевод в текстовый вид, вызов новой реализации
        {
            return $"{Math.Round(balance / currency.Course, 2)}";
        }
    }
}
