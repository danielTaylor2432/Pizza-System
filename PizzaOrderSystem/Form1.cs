using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PizzaOrderSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel2.Hide();
            panel3.Hide();
            panel5.Hide();
            panel7.Hide();
            panel3.Hide();
            panel4.Hide();
            showName.Hide();
            hiddenMoney.Hide();
            orderHistory.Enabled = false;
            editAccount.Enabled = false;

            //editAccount.ForeColor = System.Drawing.Color.Gray; 
            editAccount.BackColor = System.Drawing.Color.LightGray;

            //orderHistory.ForeColor = System.Drawing.Color.Gray;
            orderHistory.BackColor = System.Drawing.Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Show();
            panel3.Hide();
            panel2.Hide();
            panel7.Hide();
            panel4.Hide();
            string memberEmail = showName.Text;
            textBox5.AppendText(Order.GetOrdersForMember(memberEmail));


        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Show();
            panel3.Hide();
            panel2.Hide();
            panel7.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            string message = "Order has been updated.";
            string title = "Success";
            MessageBox.Show(message, title);
            
            //string message = "Congratulation! You are our new member #1001.";
            //string title = "Welcome";
            //MessageBox.Show(message, title);
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel5.Hide();
            panel7.Show();
            panel4.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel3.Hide();
            panel5.Hide();
            panel7.Hide();
            panel4.Hide();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel2.Hide();
            panel5.Hide();
            panel7.Hide();
            panel4.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Order.ValidateCreditCard(orderName.Text, orderNumber.Text, orderAddress.Text, orderCVS.Text, orderEmail.Text))
            {
                decimal final;
                Decimal.TryParse(hiddenMoney.Text, out final);

                Order createOrder = new Order(final, "In Progress", orderEmail.Text);
                Order.saveOrderToDatabase(createOrder);

                string message = "sucker!";
                string title = "Contratz";
                MessageBox.Show(message, title);
                orderAddress.Text = string.Empty;
                orderCVS.Text = string.Empty;
                orderEmail.Text = string.Empty;
                orderName.Text = string.Empty;
                orderNumber.Text = string.Empty;
            }
            else
            {
                string message = "Incorrect";
                string title = "fail";
                MessageBox.Show(message, title);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string username = createName.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (Member.validateCreateMember(createName.Text, createEmail.Text, createAddress.Text, createPassword.Text, createPhone.Text))
            {
                Member createMember = new Member(createName.Text, createAddress.Text, createEmail.Text, createPassword.Text, createPhone.Text);
                Member.saveMemberToDatabase(createMember);

                string message = "Account Made!";
                string title = "Contratz";
                MessageBox.Show(message, title);
                createName.Text = string.Empty;
                createAddress.Text = string.Empty;
                createEmail.Text = string.Empty;
                createPassword.Text = string.Empty;
                createPhone.Text = string.Empty;
            }
            else
            {
                string message = "Incorrect";
                string title = "fail";
                MessageBox.Show(message, title);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            bool exist = Member.checkLogIn(loginEmail.Text, loginPassword.Text);

            if (exist)
            {
                orderHistory.Enabled = true;
                editAccount.Enabled = true;

                loginInit.Enabled = false;
                loginInit.BackColor = System.Drawing.Color.LightGray;
                panel3.Hide();

                //editAccount.ForeColor = System.Drawing.Color.White;
                editAccount.BackColor = System.Drawing.Color.White;
                orderHistory.BackColor = System.Drawing.Color.White;
                string loginEmailShow = loginEmail.Text;
                string loginPasswordShow = loginPassword.Text;

                Member userInfo = Member.GetUserInfo(loginEmailShow, loginPasswordShow);

                if (userInfo != null)
                {
                    showName.Text = loginEmailShow;
                    showName.Show();
                }
                else
                {
                    // Handle login failure
                    showName.Text = "Login failed. Please check your credentials.";
                }
            }
            else
            {
                string message = "Please enter a valid Member Login";
                string title = "fail";
                MessageBox.Show(message, title);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void editAccount_Click(object sender, EventArgs e)
        {
            panel4.Show();
            panel3.Hide();
            panel2.Hide();
            panel5.Hide();
            panel7.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Member.validateEditMember(editName.Text, editEmail.Text, editAddress.Text, editPassword.Text, editPhone.Text))
            {
                Member editMember = new Member(editName.Text, editAddress.Text, editEmail.Text, editPassword.Text, editPhone.Text);
                Member.EditMemberInDatabase(editMember);
                string message = "Edited!";
                string title = "Finished";
                MessageBox.Show(message, title);
                editName.Text = string.Empty;
                editAddress.Text = string.Empty;
                editEmail.Text = string.Empty;
                editPassword.Text = string.Empty;
                editPhone.Text = string.Empty;
            }
            else
            {
                string message = "Incorrect Parameters or Email";
                string title = "fail";
                MessageBox.Show(message, title);
            }
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cheeseUp_ValueChanged(object sender, EventArgs e)
        {
            decimal cheesePrice = 9.95m; 
            decimal pepperoniPrice = 10.35m; 
            decimal pineapplePrice = 13.95m; 
            decimal blendPrice = 15.50m;

            // Get the quantity from each numeric up-down control
            int cheeseQuantity = (int)cheeseUp.Value;
            int pepperoniQuantity = (int)pepperoniUp.Value;
            int pineappleQuantity = (int)pineappleUp.Value;
            int blendQuantity = (int)blendUp.Value;

            // Calculate the total cost
            decimal totalCost = (cheeseQuantity * cheesePrice) + (pepperoniQuantity * pepperoniPrice) +
                                (pineappleQuantity * pineapplePrice) + (blendQuantity * blendPrice);

            // Update the label to display the total cost
            hiddenMoney.Text = totalCost.ToString();
            endPrice.Text = $"Total Cost: {totalCost:C}";
        }

        private void pepperoniUp_ValueChanged(object sender, EventArgs e)
        {
            decimal cheesePrice = 9.95m;
            decimal pepperoniPrice = 10.35m;
            decimal pineapplePrice = 13.95m;
            decimal blendPrice = 15.50m;

            // Get the quantity from each numeric up-down control
            int cheeseQuantity = (int)cheeseUp.Value;
            int pepperoniQuantity = (int)pepperoniUp.Value;
            int pineappleQuantity = (int)pineappleUp.Value;
            int blendQuantity = (int)blendUp.Value;

            // Calculate the total cost
            decimal totalCost = (cheeseQuantity * cheesePrice) + (pepperoniQuantity * pepperoniPrice) +
                                (pineappleQuantity * pineapplePrice) + (blendQuantity * blendPrice);

            // Update the label to display the total cost
            hiddenMoney.Text = totalCost.ToString();
            endPrice.Text = $"Total Cost: {totalCost:C}";
        }

        private void pineappleUp_ValueChanged(object sender, EventArgs e)
        {
            decimal cheesePrice = 9.95m;
            decimal pepperoniPrice = 10.35m;
            decimal pineapplePrice = 13.95m;
            decimal blendPrice = 15.50m;

            // Get the quantity from each numeric up-down control
            int cheeseQuantity = (int)cheeseUp.Value;
            int pepperoniQuantity = (int)pepperoniUp.Value;
            int pineappleQuantity = (int)pineappleUp.Value;
            int blendQuantity = (int)blendUp.Value;

            // Calculate the total cost
            decimal totalCost = (cheeseQuantity * cheesePrice) + (pepperoniQuantity * pepperoniPrice) +
                                (pineappleQuantity * pineapplePrice) + (blendQuantity * blendPrice);

            // Update the label to display the total cost
            hiddenMoney.Text = totalCost.ToString();
            endPrice.Text = $"Total Cost: {totalCost:C}";
        }

        private void blendUp_ValueChanged(object sender, EventArgs e)
        {
            decimal cheesePrice = 9.95m;
            decimal pepperoniPrice = 10.35m;
            decimal pineapplePrice = 13.95m;
            decimal blendPrice = 15.50m;

            // Get the quantity from each numeric up-down control
            int cheeseQuantity = (int)cheeseUp.Value;
            int pepperoniQuantity = (int)pepperoniUp.Value;
            int pineappleQuantity = (int)pineappleUp.Value;
            int blendQuantity = (int)blendUp.Value;

            // Calculate the total cost
            decimal totalCost = (cheeseQuantity * cheesePrice) + (pepperoniQuantity * pepperoniPrice) +
                                (pineappleQuantity * pineapplePrice) + (blendQuantity * blendPrice);

            // Update the label to display the total cost
            hiddenMoney.Text = totalCost.ToString();
            endPrice.Text = $"Total Cost: {totalCost:C}";
        }

        private void endPrice_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void showName_Click(object sender, EventArgs e)
        {

        }
    }
}
