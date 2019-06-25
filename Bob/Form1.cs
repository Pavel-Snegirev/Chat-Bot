using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Bob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Point mouseOffset; // получение координат для перемещения формы ждля мыши
        private bool isMouseDown = false; //
        string kontrol_enter = ""; // переменная для управления режимами работы программы, с использованием контрольнвых слов используется в методе KontrolE
        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None; 
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;//цвет фона  
            this.TransparencyKey = this.BackColor;//он же будет заменен на прозрачный цвет
            richTextBox2.Text = "Привет, меня зовут БОБ. Я виртуальный ассистент.Напиши СПРАВКА (заглавными буквами) и ознакомься с моими возможностями.";
        }

      public void KontrolE() // метод контрольных слов, запускает режимы работы бота

        {
           

            switch (kontrol_enter)
            {
                
                case "ВЫХОД": // ЗАКРЫВАЕТ ПРИЛОЖЕНИЕ
                    this.Close();
                    break;
                                        
                            
                case "ЗАПУСТИ ХРОМ":
                    Process.Start("chrome.exe");
                    break;
                

               
            }

        }

       static string Trim(string str, char[] chars) //функция удаления букв
        {
            string strA = str; // Корректировать строку если не правильно

            for (int i = 0; i < chars.Length; i++)
            {
                strA = strA.Replace(char.ToString(chars[i]), "");
            }

            return strA;
        }

       

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            kontrol_enter = richTextBox1.Text; //ПРИНИМАЕМ ДАННЫЕ С ПОЛЯ ВВОДА И ЗАПИСЫВАЕМ В ПЕРЕМЕННУЮ
        }

        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {

        }


        

       string Ans(string kontrol_enter) // функция генерации ответа

        {
            string tr = "():^^=!?"; //символы которые нужно учитывать
            var ans = ""; // ответ бота
            kontrol_enter = kontrol_enter.ToLower(); //перевод букв в нижний регистр
            kontrol_enter = Trim(kontrol_enter, tr.ToCharArray());
            string[] baza = File.ReadAllLines(@"D:\Проекты C#\Bob\Bob\bin\Debug\answer_databse.txt", Encoding.Default); // база вопросов и Ответов
            for (int i = 0; i < baza.Length; i += 2) // цикл поиска в базе ответов
            {
               

                if (kontrol_enter == baza[i])
                {
                    ans = baza[i + 1]; //выдает ответ
                    break; //завершает цикл вопроса
                }
                else
                {
                    ans = "рости я еще не все знаю, я только учусь";
                    richTextBox2.Text = Convert.ToString(ans);
                }


            }

            return ans;


        }
        private void Button1_Click(object sender, EventArgs e)
        {


            KontrolE();

           
               
                richTextBox2.Text = Convert.ToString("Вопрос :" + kontrol_enter + "\n" + "Ответ: " + Ans(kontrol_enter) + "\n");

                richTextBox1.Clear();
            



        }
        /*---------------------------------ПЕРЕТАСКИВАНИЕ ПРОГРАММЫ ЗА ЛЮБОЙ ЭЛЕМЕНТ КРОМЕ РАМКИ- начало--------------*/
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the isMouseDown field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
        /*--------------конец----------*/
    }

}
