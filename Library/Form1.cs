using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Form1 : Form
    {
        public static void chvisibltext(bool flag,params TextBox[] boxes)//change visible textboxes
        {
            foreach(TextBox textbx in boxes)

            {
                textbx.Visible = flag;
            }
        }

        public static void chvisiblbtn(bool flag,params Button[] btns)
        {
            foreach (Button btn in btns)
            {
                btn.Visible = flag;
            }
        }

        public static string Sha1(string plaintext)
        {
            System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed();//create object of type SHA1managed. Object's name is sha1
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);//sha1 can operate only with byte array. We convert string into byte array in UTF8 encoding
            byte[] hashBytes = sha1.ComputeHash(plaintextBytes);// we encrypt/hash the byte array

            StringBuilder sb = new StringBuilder();//create a stringbuilder object to convert hash bytes to string
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);//Concatenates bytes to string"{0:x2}"  -> two hexadecimal characters
            }
            return sb.ToString();//return hased string
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();// creates new db object
            //list is an array(collection) that can be expanded easily
            List<string> user = new List<string>();//create list of users
            List<string> password = new List<string>();//create list of passwords
            db.commr("Select * From Users", "UserName", ref user);//get all the contets of the users table and Username column
            db.commr("Select * From Users", "UserPassword", ref password);//same
            if (user.Contains(textBox1.Text) && password.Contains(textBox2.Text))//check if username and userpassword is correct
            {
                MessageBox.Show("login success");
                chvisibltext(true, textBox3, textBox4, textBox5, textBox6);
                chvisiblbtn(true, button2, button3, button4);
                chvisibltext(false,textBox1,textBox2);
                chvisiblbtn(false, button1);
                label1.Visible = false;
              
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            chvisibltext(false, textBox3, textBox4, textBox5, textBox6);
            chvisiblbtn(false, button2, button3, button4);
            chvisibltext(true, textBox1, textBox2);
            chvisiblbtn(true, button1);
            label1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            List<string> id = new List<string>();
            db.commr("Select * From Users", "UserId", ref id);
            int newid = id.Count + 1;
            MessageBox.Show(newid.ToString());
            //MessageBox.Show("");
            db.comma("Insert into Users(UserId,UserName,UserPassword,Status) values(" + newid++ + ",'" + textBox3.Text + "','" + Sha1(textBox4.Text) + "',3)");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            List<string> id = new List<string>();
            db.commr("Select * From Books", "BookId", ref id);
            int newid = id.Count + 1;
            db.comma("Insert into Books(BookId,Title,Status,Author) values(" + newid++ + ",'" + textBox5.Text + "', 0 ,'" + textBox6.Text + "')");
            MessageBox.Show("Book added");
        }
    }
}
