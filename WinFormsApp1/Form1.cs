using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calender
{
    public partial class Form1 : Form
    {
        const int MAXROW = 6; //最大行(週)
        const int MAXCOL = 7; //最大列(日〜土の7曜日)

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDataGridView(dtp.Value);
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            SetDataGridView(dtp.Value);
        }

        private void SetDataGridView(DateTime dt)
        {
            dgvCal.Rows.Clear();

            DateTime baseDate = dt;
            Console.WriteLine(String.Format("{0} = {1}","指定日", baseDate.ToShortDateString()));

            DateTime firstDate = new DateTime(baseDate.Year, baseDate.Month, 1); //月初(1日)
            Console.WriteLine(String.Format("{0} = {1}", firstDate.ToShortDateString()));

            //月初の１日は何曜日か？
            int num = (int)firstDate.DayOfWeek; //日曜日=0 〜土曜日=6
            Console.WriteLine(String.Format("{0} = {1}", firstDate.ToShortDateString(),num));

            DateTime startDay = firstDate.AddDays(-1 * num); //カレンダーの始まり位置(1日が何曜日か)
            DateTime endDate = firstDate.AddMonths(1).AddDays(-1); //月末(月初 + 1ヵ月 - 1日)
            Console.WriteLine(String.Format("{0} = {1}","左上", startDay.ToShortDateString()));
            Console.WriteLine(String.Format("{0} = {1}","月末", endDate.ToShortDateString()));
            
            int addDay = 0; //月初を１ずつ増やすカウンター
            DateTime wkDay = startDay;
            dgvCal.RowCount = 0; //行数を0に初期化
            for(int y = 0; y < MAXROW; y++) //カレンダーの最大週分のループ(行を生成)
            {
                dgvCal.Rows.Add(); //行追加

                for(int x = 0; x < MAXCOL; x++) //日曜〜土曜のループ
                {
                    wkDay = startDay.AddDays(addDay);
                    addDay++;

                     if(wkDay.Month != baseDate.Month)
                     {
                         //現在の月が指定の月と異なる場合は、カレンダーに表示させないようスキップ
                         continue;
                     }

                         //(注意)DataGridViewは、[列,行]で位置を指定する。
                     dgvCal[x,y].Value = wkDay.Day;

                     if(wkDay.CompareTo(endDate) == 0)
                     {
                          //現在の日にち = 月末日となる場合、処理終了
                          //翌日以降は翌月
                          break;
                     }
                }
               
                         
            }

        }

    }
}