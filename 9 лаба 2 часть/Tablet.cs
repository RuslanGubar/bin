using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _9_лаба_2_часть
{
    
    public partial class Tablet : Form
    {
        private string model;
        private double diagonal;
        private string operationSystem;
        private short memory;
        private short storage;
        private string cpu;
        
        //заполнение полей объекта значениями из строкового массива
        public void SetStringValues(string[] array)
        {
            model = array[0];
            diagonal = Convert.ToDouble(array[1]);
            operationSystem = array[2];
            memory = Convert.ToInt16(array[3]);
            storage = Convert.ToInt16(array[4]);
            cpu = array[5];
        }
        //получение строкового массива со значениями полей объекта
        public string[] GetStringValues()
        {
            string[] array = new string[6]; //создаем массив

            //заполняем его значениями
            array[0] = model;
            array[1] = diagonal.ToString();
            array[2] = operationSystem;
            array[3] = memory.ToString();
            array[4] = storage.ToString();
            array[5] = cpu;
            return array; //возвращаем результат
        }
        private string filename = "tablets.dat"; 
        private List<Tablet> Tablets = new List<Tablet>();
        private int current = 0;
        public Tablet()
        {
            InitializeComponent();
        }
        private void AddTablet()
        {
            Tablet buf = new Tablet(); 
            string[] TabletInfo = new string[6];
            TabletInfo[0] = tbModel.Text;
            TabletInfo[1] = tbDiagonal.Text;
            TabletInfo[2] = tbOS.Text;
            TabletInfo[3] = tbMemory.Text;
            TabletInfo[4] = tbStorage.Text;
            TabletInfo[5] = tbCPU.Text;
            buf.SetStringValues(TabletInfo); //заносим значения в объект
            Tablets.Add(buf); //добавляем объект в список
           
        }
        private void SaveFile()
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(filename,
                FileMode.OpenOrCreate, FileAccess.Write))
                {
                    bf.Serialize(fs, Tablets); 
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ошибка ввода-вывода: " + ex.ToString());
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Ошибка сериализации: " + ex.ToString());
            }
        }
        private void Tablet_Load(object sender, EventArgs e)
        {

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            tbModel.Clear();
            tbDiagonal.Clear();
            tbOS.Clear();
            tbMemory.Clear();
            tbStorage.Clear();
            tbCPU.Clear();
            btnSave.Visible = true;
            btnCancel.Visible = true;
            Count();
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddTablet(); 
            SaveFile(); 
            current = Tablets.Count - 1; 
            btnSave.Visible = false;
            btnCancel.Visible = false;
            MessageBox.Show("Запись добавлена");
            
            
        }
        private void TabletToTextBoxes(int current)
        {
            if (Tablets.Count > 0)
            {
                string[] TabletInfo = Tablets[current].GetStringValues();
                tbModel.Text = TabletInfo[0];
                tbDiagonal.Text = TabletInfo[1];
                tbOS.Text = TabletInfo[2];
                tbMemory.Text = TabletInfo[3];
                tbStorage.Text = TabletInfo[4];
                tbCPU.Text = TabletInfo[5];
            }
            else 
            {
                tbModel.Clear();
                tbDiagonal.Clear();
                tbOS.Clear();
                tbMemory.Clear();
                tbStorage.Clear();
                tbCPU.Clear();
            }
        }
        private void ScrollTablet(sbyte inc)
        {
            
            if ((current + inc <= Tablets.Count - 1) && (current + inc >= 0))
            {
                current += inc; 
                TabletToTextBoxes(current); 
            }
        }

        
        //задание в самой программе 15 пункт
        private void tsbPrev_Click(object sender, EventArgs e)
        {

            sbyte z = -1;
            ScrollTablet(z);
            TabletToTextBoxes(current);
        }

        private void tsbNext_Click(object sender, EventArgs e)
        {
            sbyte z=1;
         ScrollTablet(z);
            TabletToTextBoxes(current);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            TabletToTextBoxes(current);
        }

        private void DeleteTablet()
        {
            Tablets.RemoveAt(current); //удаляем значение из списка по его номеру
            if (current > 0) //если удалялась не первая запись
                current--; //делаем текущей предыдущую
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (Tablets.Count > 0) //если список не пуст
            {
                DeleteTablet(); //удаляем запись
                                //если после удаления в списке не осталось значений 
                if (Tablets.Count == 0)
                {
                    //очищаем текстовые поля
                    tbModel.Clear();
                    tbDiagonal.Clear();
                    tbOS.Clear();
                    tbMemory.Clear();
                    tbStorage.Clear();
                    tbCPU.Clear();
                }
                else
                {
                    TabletToTextBoxes(current); //выводим информацию о текущей записи
                }
                SaveFile(); //сохраняем изменения 
                MessageBox.Show("Запись удалена");
            }
        }

       
        private void tslCount_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void tslCount_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void tslCount_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void tslCount_LocationChanged(object sender, EventArgs e)
        {
           
        }
        //задание 2
        private void Count()
        {
            int have = 0;
            string content;
            int sum = 0;
            if (Tablets.Count > 0)
            {
                sum = have + 1;
                content = "Кол-во записей: " + sum;
            }
            else
            {
                content = "Нет записей для отображения";
            }
            tslCount.Text = content;

        }
    }
}
