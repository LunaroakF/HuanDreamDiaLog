using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace HuanDreamDiaLog
{
    public partial class Dream : Form
    {
        int shuliang;
        int yan;
        string APPNAME = "HuanDreamDiaLog";
        public Dream()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = APPNAME;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = int.Parse(CTC.Text);
                if (count <= 0)
                {
                    MessageBox.Show("生成数量非正整数", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("生成数量非正整数", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                int count = int.Parse(yanchi.Text);
                if (count <= 0)
                {
                    MessageBox.Show("延迟值非正整数", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("延迟值非正整数", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            stop.Enabled = false;
            shuliang = int.Parse(CTC.Text);
            yan = int.Parse(yanchi.Text);
            
            if (onetime.Checked)
            {
                Thread thread = new Thread(new ThreadStart(Start));
                thread.Start();
            }
            else if (always.Checked)
            {
                Thread thread = new Thread(new ThreadStart(Astart));
                thread.Start();
            }
        }

        public void Create(string title, string mains, int type1,int type2)
        {
            if (type1 == 1)
            {
                if (type2 == 1) { MessageBox.Show(mains, title, MessageBoxButtons.OK); }
                if (type2 == 2) { MessageBox.Show(mains, title, MessageBoxButtons.OKCancel); }
                if (type2 == 3) { MessageBox.Show(mains, title, MessageBoxButtons.RetryCancel); }
                if (type2 == 4) { MessageBox.Show(mains, title, MessageBoxButtons.YesNoCancel); }
                if (type2 == 5) { MessageBox.Show(mains, title, MessageBoxButtons.YesNo); }
                if (type2 == 6) { MessageBox.Show(mains, title, MessageBoxButtons.AbortRetryIgnore); }
            }
            else if (type1 == 2)
            {
                if (type2 == 1) { MessageBox.Show(mains, title, MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (type2 == 2) { MessageBox.Show(mains, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Stop); }
                if (type2 == 3) { MessageBox.Show(mains, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop); }
                if (type2 == 4) { MessageBox.Show(mains, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop); }
                if (type2 == 5) { MessageBox.Show(mains, title, MessageBoxButtons.YesNo, MessageBoxIcon.Stop); }
                if (type2 == 6) { MessageBox.Show(mains, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop); }
            }
            else if (type1 == 3)
            {
                if (type2 == 1) { MessageBox.Show(mains, title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                if (type2 == 2) { MessageBox.Show(mains, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); }
                if (type2 == 3) { MessageBox.Show(mains, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning); }
                if (type2 == 4) { MessageBox.Show(mains, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning); }
                if (type2 == 5) { MessageBox.Show(mains, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning); }
                if (type2 == 6) { MessageBox.Show(mains, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning); }
            }
            else if (type1 == 4)
            {
                if (type2 == 1) { MessageBox.Show(mains, title, MessageBoxButtons.OK, MessageBoxIcon.Question); }
                if (type2 == 2) { MessageBox.Show(mains, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question); }
                if (type2 == 3) { MessageBox.Show(mains, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question); }
                if (type2 == 4) { MessageBox.Show(mains, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question); }
                if (type2 == 5) { MessageBox.Show(mains, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                if (type2 == 6) { MessageBox.Show(mains, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question); }
            }
        }
        public void Start()
        {
            this.Text = APPNAME + "：" + "生成" + title.Text + "-" + NR.Text + "*" + shuliang.ToString();
            stop.Enabled = true;
            onetime.Enabled = false;
            always.Enabled = false;
            while (shuliang > 0)
            {
                System.Threading.Thread.Sleep(yan);
                Thread thread = new Thread(new ThreadStart(Thing));
                thread.Start();
                shuliang--;
            }
            stop.Enabled = false;
            onetime.Enabled = true;
            always.Enabled = true;
            this.Text = APPNAME;
        }
        public void Thing()
        {
            Create(title.Text, NR.Text, Cicon(comboBox1), Cbt(comboBox2));
        }
        public void Astart()
        {
            this.Text = APPNAME + "：" + "生成" + title.Text + "-" + NR.Text + "*" + shuliang.ToString();
            stop.Enabled = true;
            onetime.Enabled = false;
            always.Enabled = false;
            while (shuliang > 0)
            {
                Create(title.Text, NR.Text, Cicon(comboBox1), Cbt(comboBox2));
                shuliang--;
            }
            stop.Enabled = false;
            onetime.Enabled = true;
            always.Enabled = true;
            this.Text = APPNAME;
        }

        public int Cicon(ComboBox main)
        {
            if (main.Text == "无") { return 1; }
            else if (main.Text == "意外性终止(红×)") { return 2; }
            else if (main.Text == "警告(感叹号)") { return 3; }
            else if (main.Text == "询问用户(问号)") { return 4; }
            else { return 1; }
        }
        public int Cbt(ComboBox main)
        {
            if (main.Text == "确定") { return 1; }
            else if (main.Text == "确定+取消") { return 2; }
            else if (main.Text == "重试+取消") { return 3; }
            else if (main.Text == "是+否+取消") { return 4; }
            else if (main.Text == "是+否") { return 5; }
            else if (main.Text == "中止+重试+忽略") { return 6; }
            else { return 1; }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (onetime.Checked)
            {
                always.Checked = false;
            }
            if (onetime.Checked==false&&always.Checked==false)
            {
                onetime.Checked = true;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (always.Checked)
            {
                onetime.Checked = false;
            }
            if (onetime.Checked == false && always.Checked == false)
            {
                always.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            shuliang = 0;
            stop.Enabled = false;
            onetime.Enabled = true;
            always.Enabled = true;
            this.Text = APPNAME;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
