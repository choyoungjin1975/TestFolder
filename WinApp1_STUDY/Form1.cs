using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
         데이터베이스 속성에서 connection string의 값을 가져온 것임
        (LocalDB)\MSSQLLocalDB;
        AttachDbFilename=C:\Users\Cho\source\repos\WinApp1\MyTables.mdf;
        Integrated Security = True;
        Connect Timeout = 30;
        */
        string sConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Cho\source\repos\WinApp1\MyTables.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection sConn = new SqlConnection();  //Databse file에 연결: ms-sql  ※다른 DB에서는 ODBC 뭐 이런 것이 앞에 붙을 수 있다.
        SqlCommand sCmd = new SqlCommand(); //sql 명령문 처리

        public string GetToken(int index, string inputStr, string sdel)
        {   //inputStr 문자열을 'sdel'구분자로 분할하여 그 중 index번째(0부터 시작)의 문자열을 반환
            //ex) GetToken(3, "0|1|2|3|4|5", "|") → "3"
            string[] sArr = inputStr.Split(sdel[0]);
            return sArr[index];
        }

        private void addColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
            string str = frm2.sRet;
            dataGridView1.Columns.Add("coltext", str);
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void fileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {   //Database 연결문자열을 구성
                //openFileDialog1.FileName: 선택한 파일의 전체 경로


                #region 내가 구성한 파일명 치환방법.
                /*
                string first = "";
                string second = "";
                string third = "";

                first = GetToken(1, sConString, ";");
                second = GetToken(1, first, "=");
                MessageBox.Show("first: " + first + "\r\nsecond: " + second);
                third = sConString.Replace(second, openFileDialog1.FileName);
                MessageBox.Show(third);
                */
                #endregion

                #region 강사님 구성한 파일명 치환방법.
                string[] sArr = sConString.Split(';');  // 4개의 필드 중 2번째 필드(AttachDbFilename)
                                                        //string sConnStr = string.Format("{0};AttachDbFilename={1};{2};{3}", sArr[0], openFileDialog1.FileName, sArr[1], sArr[2]);

                string sConnStr = $"{sArr[0]};AttachDbFilename={openFileDialog1.FileName};{sArr[1]};{sArr[2]}";
                #endregion

                sConn.ConnectionString = sConnStr;
                sConn.Open();
                sCmd.Connection = sConn;

                toolStripStatusLabel1.Text = "Connect";
                toolStripStatusLabel1.BackColor = Color.Green;
            }
        }
    }
}
