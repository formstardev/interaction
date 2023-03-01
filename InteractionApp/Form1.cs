using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Speech.Recognition;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Speech.AudioFormat;
//using Google.Cloud.Speech.V1;
//using Grpc.Core;

namespace InteractionApp
{

    public partial class Form1 : Form
    {

        int cnt = 0, cnt1 = 0;
        //Panel[] panels1=new Panel[100];
        //Panel[] panels2 = new Panel[100];
        MySqlConnection connection;
        SQLiteConnection sqlitecon;
        int sh;
        List<string>[] list = new List<string>[4];
        List<string>[] cmds = new List<string>[3]; 
        int selid;
        public Form1()
        {
            InitializeComponent();
            //  voicereceive();
            
            // Get the primary screen
            Screen primaryScreen = Screen.PrimaryScreen;

            // Get the width and height of the screen
            int screenWidth = primaryScreen.Bounds.Width;
            int screenHeight = primaryScreen.Bounds.Height;
            this.Size = new Size(screenWidth*7/10, screenHeight*4/5);
            panel2.Size = new Size(this.Width / 5, this.Height);
            panel1.Size = new Size(this.Width * 4 / 5-10, this.Height * 3 / 4);
            panel1.Location = new Point(this.Width / 5 - 1, 0);
            panel3.Size = new Size(this.Width * 4 / 5, this.Height / 4);
            panel3.Location = new Point(this.Width / 5 - 1, this.Height * 3 / 4);
            richTextBox1.Size = new Size(panel3.Width * 7 / 10, panel3.Height / 5);
            richTextBox1.Location = new Point((panel3.Width - richTextBox1.Width) / 2, (panel3.Height - richTextBox1.Height) / 2);
            int[] columnWidths = { 1 };
            richTextBox1.SelectionTabs = columnWidths;
            button1.Size = new Size(richTextBox1.Width / 25, richTextBox1.Height * 3 / 4);
            button1.Location = new Point(richTextBox1.Location.X + richTextBox1.Width-button1.Width*5/4, richTextBox1.Location.Y + richTextBox1.Height / 8);
            button2.Size = new Size(panel2.Width * 9 / 10, panel2.Height / 20);
            button2.Location = new Point((panel2.Width - button2.Width) / 2,button2.Height/4);
            button3.Size = new Size(richTextBox1.Width / 20, richTextBox1.Height * 3 / 4);
            button3.Location = new Point(richTextBox1.Location.X - button1.Width *2, richTextBox1.Location.Y + richTextBox1.Height / 8);

            // Output the results to the console
            //Console.WriteLine("Screen width: " + screenWidth);
            //Console.WriteLine("Screen height: " + screenHeight);

            /*for (int i = 0; i < panels1.Length; i++)
            {
                panels1[i] = new Panel();
                panels2[i] = new Panel();
            }*/

            //  Color backgroundColor = Color.FromArgb(205, 255, 255);

            // Set the form's background color to the new color
            //  this.BackColor = backgroundColor;
          //  button1.BackColor = Color.FromArgb(229, 56, 59);
          //  button1.Text = "Execute";
           // button1.BackColor = Color.FromArgb(229, 56, 59);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
          //  button1.Size = new Size(100, 40);
            button1.Font = new Font("Arial", 10, FontStyle.Regular);
            button1.Paint += (sender, e) => {
                GraphicsPath path1 = new GraphicsPath();
                int cornerRadius1 = 5;
                int width = button1.Width;
                int height = button1.Height;
                path1.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
                path1.AddArc(width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
                path1.AddArc(width - cornerRadius1, height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
                path1.AddArc(0, height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
                path1.CloseFigure();
                button1.Region = new Region(path1);
            };
            button1.BackColor = Color.FromArgb(64, 65, 79);
            button3.BackColor = Color.FromArgb(68, 70, 84);
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.Paint += (sender, e) => {
                GraphicsPath path1 = new GraphicsPath();
                int cornerRadius1 = 20;
                int width = button3.Width;
                int height = button3.Height;
                path1.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
                path1.AddArc(width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
                path1.AddArc(width - cornerRadius1, height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
                path1.AddArc(0, height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
                path1.CloseFigure();
                button3.Region = new Region(path1);
            };
            button2.Text = "+ New chat";
            button1.FlatAppearance.BorderColor = Color.FromArgb(241, 198, 198);
            button2.Font = new Font("Arial", 12, FontStyle.Regular);
            button2.BackColor=Color.FromArgb(32, 33, 35);
            button2.ForeColor = Color.FromArgb(255,255,255);
            button2.FlatAppearance.BorderSize = 1;
            button2.Paint += (sender, e) => {
                GraphicsPath path1 = new GraphicsPath();
                int cornerRadius1 = 5;
                int width = button2.Width;
                int height = button2.Height;
                path1.AddArc(0, 0, cornerRadius1, cornerRadius1, 180, 90);
                path1.AddArc(width - cornerRadius1, 0, cornerRadius1, cornerRadius1, 270, 90);
                path1.AddArc(width - cornerRadius1, height - cornerRadius1, cornerRadius1, cornerRadius1, 0, 90);
                path1.AddArc(0, height - cornerRadius1, cornerRadius1, cornerRadius1, 90, 90);
                path1.CloseFigure();
                button2.Region = new Region(path1);
            };

            richTextBox1.ForeColor = Color.FromArgb(242, 243, 244);
            richTextBox1.BackColor = Color.FromArgb(64, 65, 79);
            richTextBox1.Font = new Font("Arial", 15, FontStyle.Regular);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 2;
            int x = richTextBox1.Width;
            int y = richTextBox1.Height;
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddLine(cornerRadius, 0, x - cornerRadius, 0);
            path.AddArc(x - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddLine(x, cornerRadius * 2, x, y - cornerRadius * 2);
            path.AddArc(x - cornerRadius * 2, y - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddLine(x - cornerRadius, y, cornerRadius, y);
            path.AddArc(0, y - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.AddLine(0, y - cornerRadius * 2, 0, cornerRadius);
            richTextBox1.Region = new System.Drawing.Region(path);

           
            panel3.BackColor = Color.FromArgb(52, 54, 64);
            panel3.ForeColor = Color.FromArgb(31, 31, 33);
            richTextBox1.SelectionCharOffset = -10;
            /*  panel3.Paint += (sender, e) =>
              {
                  Color startColor = Color.FromArgb(66, 68, 81);
                  Color endColor = Color.FromArgb(53, 55, 64);

                  // Create the gradient brush
                  Rectangle gradientRect = new Rectangle(0, 0, Width, Height);
                  LinearGradientBrush brush = new LinearGradientBrush(gradientRect, startColor, endColor, LinearGradientMode.Vertical);

                  // Fill the group box with the gradient
                  e.Graphics.FillRectangle(brush, gradientRect);

                  // Draw the group box text
                  TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

              };*/

            panel2.BackColor= Color.FromArgb(32, 33, 35);
            panel2.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(68, 70, 84);
            panel1.AutoScroll = true;


            databaseConnect();
            databaseLoad();
          
        }



        private void requestView(string str)
        {

            if (str == "") MessageBox.Show("Type your command!", "Message Box Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                int sh = 0;
                Label label1 = new Label();
                label1.ForeColor = Color.FromArgb(255, 255, 255);
                label1.Font = new Font("Arial", 14);
                int width = TextRenderer.MeasureText(str, label1.Font).Width;
                int height = TextRenderer.MeasureText(str, label1.Font).Height;
                Panel subpanel1 = new Panel();
                for (int i = 0; i < cnt; i++)
                {
                    sh += (height + 100);
                }
                subpanel1.Location = new Point(0, sh);

                //  label1.AutoSize = true; // Set this to true to make the label adjust its size to fit its contents



                subpanel1.Size = new Size(panel1.Size.Width, height + 50);
                subpanel1.BackColor = Color.FromArgb(52, 53, 65);
                label1.Size = new Size(width, height);
                label1.Location = new Point((subpanel1.Width) / 4+40, (subpanel1.Height - label1.Height) / 2);
                PictureBox pB = new PictureBox();
                pB.Location = new Point((subpanel1.Width) / 4, (subpanel1.Height - label1.Height) / 2);
                pB.Size = new Size(button1.Width*3/4,label1.Height);
                pB.SizeMode = PictureBoxSizeMode.StretchImage;
                string imagePath = "communication.png";
                string filePath = System.IO.Path.Combine(Application.StartupPath, imagePath);

                pB.Image = Image.FromFile(filePath);
                label1.Text = str;

                subpanel1.Controls.Add(label1);
                subpanel1.Controls.Add(pB);
                panel1.Controls.Add(subpanel1);
                cnt++;


            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            insertReq(richTextBox1.Text);
            requestView(richTextBox1.Text);
            
            int[] columnWidths = { 1 };
            richTextBox1.SelectionTabs = columnWidths;
            if (richTextBox1.Text == "chrome")
            {
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                string url = "https://chat.openai.com";

                Process.Start(chromePath, url);
            }
            else if (richTextBox1.Text == "firefox")
            {
                Process.Start("firefox.exe");
            }
            else if (richTextBox1.Text == "opera")
            {
                //Process.Start("opera.exe");
            }
            richTextBox1.Text = "";
        }
        private void databaseConnect()
        {
            /*   string server;
               string database;
               string uid;
               string password;
              server = "localhost";
              database = "mydb";
              uid = "root";
              password = "";
              string connectionString;
              connectionString = "SERVER=" + server + ";" + "DATABASE=" +database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

              connection = new MySqlConnection(connectionString);*/

            /*  string connectionString = "Data Source=localhost,3306;Initial Catalog=mydb;User ID=root;Password=null;";
              SqlConnection connection = new SqlConnection(connectionString);*/

            //    connection.Open();
            //     MessageBox.Show("connected successfully!");
            // DateTime dateToInsert = DateTime.Now;
            //    MySqlCommand command = new MySqlCommand("INSERT INTO commandHistory (title,created_at) VALUES ('hello',@DateValue)", connection);
            //  command.Parameters.AddWithValue("@DateValue", dateToInsert);
            // string query = "INSERT INTO commandHistory (title, created_at) VALUES('John Smith', '33')";

            //create command and assign the query and connection from the constructor
            //MySqlCommand cmd = new MySqlCommand(query, connection);

            //Execute command
            //    command.ExecuteNonQuery();
            //  SQLiteConnection.CreateFile("MyDatabase.sqlite");

            // Open a connection to the SQLite database
           sqlitecon = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
           sqlitecon.Open();
           MessageBox.Show("connected successfully");

        }
        private void databaseLoad()
        {
            /* databaseConnect();
             string query = "SELECT * FROM commandHistory";

             //Create a list to store the result
             List<string>[] list = new List<string>[3];
             list[0] = new List<string>();
             list[1] = new List<string>();
             list[2] = new List<string>();

             MySqlCommand cmd = new MySqlCommand(query, connection);
             //Create a data reader and Execute the command
             MySqlDataReader dataReader = cmd.ExecuteReader();

             //Read the data and store them in the list
             while (dataReader.Read())
             {
                 list[0].Add(dataReader["id"] + "");
                 list[1].Add(dataReader["title"] + "");
              //   list[2].Add(dataReader["create_at"].ToString());
             }

             //close Data Reader
             dataReader.Close();

             for(int i=0;i<Count();i++)
             {

                 drawchatlist();
                 cnt1++;
             }*/
            
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            SQLiteCommand command = sqlitecon.CreateCommand();
            command.CommandText = "SELECT * FROM cmdlog";

            // Execute the command and retrieve the results
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list[0].Add(reader["id"] + "");
                list[1].Add(reader["title"] + "");
                list[2].Add(reader["created_at"]+"");
                list[3].Add(reader["updated_at"]+"");
                // Process the data
            }
            selid = int.Parse(list[0][0]);
            cnt1 = 0;
            for (int i = 0; i < Count(); i++)
            {
                drawchatlist(int.Parse(list[0][i]),list[1][i]);
                cnt1++;
            }
            
        }
        public int Count()
        {
            string query = "SELECT Count(*) FROM cmdlog";
            int Count = -1;

            //Open Connection
            // databaseConnect();
            //Create Mysql Command
            SQLiteCommand cmd = new SQLiteCommand(query, sqlitecon);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                return Count;
            
        }
        public void insert(string title)
        {
            //DateTime dateToInsert = DateTime.Now;
            string dateToInsert = DateTime.Now.ToString();
             SQLiteCommand command = new SQLiteCommand("INSERT INTO cmdlog (title,created_at) VALUES (@title,@DateValue)", sqlitecon);
             command.Parameters.AddWithValue("@DateValue", dateToInsert);
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
        }
        public void delete(string str)
        {
            // Open a connection to the database

                // Create a command with a parameterized query and a WHERE clause
            /*    using (var command = new SQLiteCommand("SELECT * FROM cmdlog WHERE Name=@title", sqlitecon))
                {
                    // Set the value of the parameter
                    command.Parameters.AddWithValue("@title", str);

                    // Execute the query
                    using (var reader = command.ExecuteReader())
                    {
                        // Process the results
                        while (reader.Read())
                        {
                            // Access the data using the column names or indexes
                            //int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            //Console.WriteLine("ID: " + id + ", Name: " + name);
                        }
                    }
                }
            */

            using (var command = new SQLiteCommand("DELETE FROM cmdlog WHERE title = @title", sqlitecon))
            {
                // Set the value of the parameter
                command.Parameters.AddWithValue("@title", str);

                // Execute the command
                int rowsDeleted = command.ExecuteNonQuery();
               // Console.WriteLine("Rows deleted: " + rowsDeleted);
            }
            panel2.Controls.Clear();
            panel2.Controls.Add(button2);
            /* for (int i=0;i<100;i++)
             {
                 panels1[i].Controls.Clear();
                 panels2[i].Controls.Clear();
             }*/
            int tempid = selid;
            databaseLoad();
            selid = tempid;
        }
        public void insertReq(string req)
        {
           // string dateToInsert = DateTime.Now.ToString();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO commands (content,log_id) VALUES (@req,@logid)", sqlitecon);
          //  command.Parameters.AddWithValue("@DateValue", dateToInsert);
            command.Parameters.AddWithValue("@req", req);
            command.Parameters.AddWithValue("@logid", selid);
            command.ExecuteNonQuery();
        }
        public void getcmds(int logid)
        {
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            int num = 0;
            SQLiteCommand command = new SQLiteCommand("select * from commands where log_id=@logid", sqlitecon);
            command.Parameters.AddWithValue("@logid", logid);
            // Execute the query
            using (var reader = command.ExecuteReader())
            {
                // Process the results
                while (reader.Read())
                {
                    num++;
                    list[0].Add(reader["cmd_id"] + "");
                    list[1].Add(reader["content"] + "");
                    list[2].Add(reader["log_id"] + "");
                    // Access the data using the column names or indexes
                    //int id = reader.GetInt32(0);
                    //string cmds = reader.GetString(1);
                    //Console.WriteLine("ID: " + id + ", Name: " + name);
                }
            }
            for(int i=0;i<num;i++)
            {
                requestView(list[1][i]);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            cnt = 0;
            insert("Log History" + cnt1.ToString());
            int id=0;
            using (var command = new SQLiteCommand("SELECT * FROM cmdlog WHERE Name=@title", sqlitecon))
            {
                  // Set the value of the parameter
                  command.Parameters.AddWithValue("@title", "Log History" + cnt1.ToString());

                  // Execute the query
                  using (var reader = command.ExecuteReader())
                  {
                      // Process the results
                      while (reader.Read())
                      {
                          // Access the data using the column names or indexes
                          id = reader.GetInt32(0);
                          //string name = reader.GetString(1);
                          //Console.WriteLine("ID: " + id + ", Name: " + name);
                      }
                  }
            }
            drawchatlist(id,"Log History"+cnt1.ToString());

            cnt1++;
            panel1.Controls.Clear();
            //  databaseConnect();
            /*for (int i = 0; i < 100; i++)
            {
                panels1[i].Controls.Clear();

            }*/

        }
        void drawchatlist(int id, string title)
        {

            Label temp = new Label();
            temp.Text = id.ToString();
            temp.Hide();
            sh = button1.Height * 7 / 4;
            for (int i = 0; i < cnt1; i++)
            {
                sh += button1.Height * 7 / 4;
            }
            Panel subpanel2 = new Panel();
            subpanel2.Controls.Add(temp);
            subpanel2.Size = new Size(panel2.Width * 9 / 10, panel2.Height / 20);
            subpanel2.Location = new Point((panel2.Width - button2.Width) / 2, sh + button2.Height / 4);
            if (selid == id)
            {
                subpanel2.BackColor = Color.FromArgb(52, 53, 65);
            }
            else
            {
                subpanel2.BackColor = Color.FromArgb(32, 33, 35);
            }
            
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int cornerRadius = 10;
            int x = subpanel2.Width;
            int y = subpanel2.Height;
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddLine(cornerRadius, 0, x - cornerRadius, 0);
            path.AddArc(x - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddLine(x, cornerRadius * 2, x, y - cornerRadius * 2);
            path.AddArc(x - cornerRadius * 2, y - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddLine(x - cornerRadius, y, cornerRadius, y);
            path.AddArc(0, y - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.AddLine(0, y - cornerRadius * 2, 0, cornerRadius);
            subpanel2.Region = new System.Drawing.Region(path);
            // Create some controls to add to the GroupBox
            Label label1 = new Label();
            label1.Text = title;
            label1.ForeColor = Color.FromArgb(215, 215, 215);
            label1.Font = new Font("Arial", 12);
            int width = TextRenderer.MeasureText(label1.Text, label1.Font).Width;
            int height = TextRenderer.MeasureText(label1.Text, label1.Font).Height;
            label1.Size = new Size(width, height);
            label1.Location = new Point((subpanel2.Width) / 4, (subpanel2.Height - label1.Height) / 2);
            subpanel2.Controls.Add(label1);
          /*  PictureBox pB = new PictureBox();
            pB.Size = new Size(button1.Width, label1.Height);
            pB.Location = new Point(label1.Location.X - pB.Width * 5 / 4, (subpanel2.Height - label1.Height) / 2);

            pB.SizeMode = PictureBoxSizeMode.StretchImage;
            string imagePath = "speech-bubble.png";
            string filePath = System.IO.Path.Combine(Application.StartupPath, imagePath);

            pB.Image = Image.FromFile(filePath);
            subpanel2.Controls.Add(pB);*/

            /*  PictureBox pBedit = new PictureBox();
              pBedit.Size = new Size(button1.Width, label1.Height);
              pBedit.Location = new Point(label1.Location.X +label1.Width + pBedit.Width /4, (subpanel2.Height - label1.Height) / 2);

              pBedit.SizeMode = PictureBoxSizeMode.StretchImage;
              imagePath = "edit.png";
              filePath = System.IO.Path.Combine(Application.StartupPath, imagePath);

              pBedit.Image = Image.FromFile(filePath);
              subpanel2.Controls.Add(pBedit);
            */
            PictureBox pBdel = new PictureBox();
            pBdel.Size = new Size(button1.Width , label1.Height*4/3);
            pBdel.Location = new Point(label1.Location.X + label1.Width + pBdel.Width * 5 / 4, (subpanel2.Height - label1.Height) / 2);

            pBdel.SizeMode = PictureBoxSizeMode.StretchImage;
            string imagePath = "delete.png";
            string filePath = System.IO.Path.Combine(Application.StartupPath, imagePath);

            pBdel.Image = Image.FromFile(filePath);
            pBdel.MouseEnter += (sender, e) =>
            {
                // Code to handle the Enter event
                pBdel.BackColor = Color.FromArgb(0, 0, 0);
                
            };
            pBdel.MouseLeave += (sender, e) =>
            {
                // Code to handle the Leave event
                pBdel.BackColor = Color.FromArgb(52, 53, 65);

            };
            pBdel.Click += (sender, e) =>
            {
                // Code to handle the Click event
                string str = label1.Text;
                delete(str);
            };
           /* label1.MouseEnter += (sender, e) =>
            {
                // Code to handle the Enter event
                label1.ForeColor = Color.FromArgb(255,255,255);

            };
            label1.MouseLeave += (sender, e) =>
            {
                // Code to handle the Leave event
                label1.ForeColor = Color.FromArgb(215, 215, 215);

            };
            label1.Click += (sender, e) =>
            {
                // Code to handle the Click event
                MessageBox.Show(id.ToString()+"selected");
                selid = id;
            };*/
            subpanel2.MouseEnter += (sender, e) =>
            {
                if(selid!=id)
                {
                    label1.ForeColor = Color.FromArgb(255, 255, 255);
                    subpanel2.BackColor = Color.FromArgb(52, 53, 60);
                    
                }
                // Code to handle the Enter event



            };
            subpanel2.MouseLeave += (sender, e) =>
            {
                if (selid != id)
                {
                    // Code to handle the Leave event
                    label1.ForeColor = Color.FromArgb(215, 215, 215);
                    subpanel2.BackColor = Color.FromArgb(32, 33, 35);
                }
            };
            subpanel2.Click += (sender, e) =>
            {
                // Code to handle the Click event
                MessageBox.Show(id.ToString()+"selected");
                selid = id;
                foreach (Panel p in panel2.Controls.OfType<Panel>())
                {
                    p.BackColor = Color.FromArgb(32, 33, 35);
                }
                subpanel2.BackColor = Color.FromArgb(52, 53, 65);
                panel1.Controls.Clear();
                cnt = 0;
                getcmds(selid);
                
            };
            subpanel2.Controls.Add(pBdel);


            panel2.Controls.Add(subpanel2);
            //cnt1++;
            
            richTextBox1.Text = "";
            // Get the screen that the mouse is currently on
           /* Screen currentScreen = Screen.FromPoint(Cursor.Position);

            // Iterate over all the screens to find the one that the mouse is on
            foreach (Screen screen in Screen.AllScreens)
            {
                // Check if the current screen matches the screen that the mouse is on
                if (screen.Bounds.Contains(Cursor.Position))
                {
                    currentScreen = screen;
                    break;
                }
            }*/

            // Determine which panel on the current screen the mouse is over


        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true; // Cancel the form closing event
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // code to handle mouse enter event
            button1.BackColor = Color.FromArgb(32, 33, 35);
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            richTextBox1.SelectionCharOffset = -10;
            int[] columnWidths = { 1 };
            richTextBox1.SelectionTabs = columnWidths;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter key was pressed
                e.Handled = true;
                insertReq(richTextBox1.Text);
                requestView(richTextBox1.Text);

                int[] columnWidths = { 1 };
                richTextBox1.SelectionTabs = columnWidths;
                if (richTextBox1.Text == "chrome")
                {
                    string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                    string url = "https://chat.openai.com";

                    Process.Start(chromePath, url);
                }
                else if (richTextBox1.Text == "firefox")
                {
                    Process.Start("firefox.exe");
                }
                else if (richTextBox1.Text == "opera")
                {
                    //Process.Start("opera.exe");
                }
                richTextBox1.Text = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.SelectionCharOffset = -10;
            int[] columnWidths = { 1 };
            richTextBox1.SelectionTabs = columnWidths;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor =Color.FromArgb(32, 33, 35);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(68, 70, 84);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Add words to the grammar
            //   builder.Append(new Choices("Hello", "Goodbye", "Yes", "No", "Please", "Thank you", "Sorry", "Excuse me", "Help", "Friend", "Family", "Love", "Fun", "Happy", "Sad", "Beautiful", "Interesting", "Important", "Amazing", "Awesome."));
            // Create a new SpeechRecognitionEngine instance.


            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

            // Create a grammar builder and add a phrase to it.
             GrammarBuilder grammarBuilder = new GrammarBuilder();

            //grammarBuilder.Append(new Choices("Hello",
            //      "Goodbye", "Yes", "No", "Please", "Thank you", 
            //      "Sorry", "Excuse me", "Help", "Friend", "Family",
            //      "Love", "Fun", "Happy", "Sad", "Beautiful",
            //      "Interesting", "Important", "Amazing", "Awesome"
            //      ));
            string wordPath = "ss.txt";
            string filePath = System.IO.Path.Combine(Application.StartupPath, wordPath);
            string[] words = new string[2001];
            for(int i=0;i<words.Length;i++)
            {
                words[i] = "chrome";
            }
            if (File.Exists(filePath))
            {
                int k = 0;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        words[k++] = line;
                        if (k > 2000) break;
                       // grammarBuilder.Append(line);
                        //Console.WriteLine(line); // process the line as needed
                    }
                }
            }

            //grammarBuilder.Append(new Choices(words));
              grammarBuilder.AppendDictation();
            // Create a grammar from the grammar builder.
             Grammar grammar = new Grammar(grammarBuilder);

            recognizer.LoadGrammar(grammar);
            recognizer.SetInputToDefaultAudioDevice();
            //recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            // Start recognition.
            //recognizer.RecognizeAsync(RecognizeMode.Multiple);
            RecognitionResult result = recognizer.Recognize();
            if (result != null)
            {
               // MessageBox.Show(result.Text);
              //  richTextBox1.Text += result.Text;
                SpeechAudioFormatInfo audioFormat = result.Audio.Format;
                int audioSize = (int)result.Audio.AudioPosition.TotalSeconds;


                // Compute the root mean square (RMS) amplitude of the audio stream.
                float rms = 0;
                for (int i = 0; i < audioSize; i += 2)
                {
                    int sample = result.Audio.Format.SamplesPerSecond;
                    rms += sample * sample;
                }
                rms = (float)Math.Sqrt(rms / (audioSize / 2));

                // Update your animation based on the RMS amplitude.
                // For example, you could scale a circle based on the RMS amplitude.
                float scale = rms * 10;
                richTextBox1.Text += (rms.ToString() + "  ");
            }
            else
            {
                MessageBox.Show("Speak again!");
                //richTextBox1.Text = result.Text;
            }



        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
         //   throw new NotImplementedException();
            // Get the recognized text and audio stream.
            //string recognizedText = e.Result.Text;
            SpeechAudioFormatInfo audioFormat = e.Result.Audio.Format;
            int audioSize = (int)e.Result.Audio.AudioPosition.TotalSeconds;
            

            // Compute the root mean square (RMS) amplitude of the audio stream.
            float rms = 0;
            for (int i = 0; i < audioSize; i += 2)
            {
                int sample = e.Result.Audio.Format.SamplesPerSecond;
                rms += sample * sample;
            }
            rms = (float)Math.Sqrt(rms / (audioSize / 2));

            // Update your animation based on the RMS amplitude.
            // For example, you could scale a circle based on the RMS amplitude.
            float scale = rms * 10;
            richTextBox1.Text += (rms.ToString()+"  ");
            //Console.WriteLine("RMS amplitude: " + rms + ", Scale: " + scale);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            // code to handle mouse enter event
            button1.BackColor = Color.FromArgb(64, 65, 79);
        }
    }

}
