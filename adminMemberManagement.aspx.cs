using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class adminMemberManagement : System.Web.UI.Page
    {
        public Member m { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            m = new Member();
            txtClasse = "";
            lstMsg = new List<string>();
        }
        // Go button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (checkId()) {
                getMemberByID();
            }
            
        }
        // Active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                updateMemberStatusByID("active");
            }        
        }
        // pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                updateMemberStatusByID("pending");
            }
           
        }
        // deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                updateMemberStatusByID("deactive");
            }       
        }
        // delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                deleteMemberByID();
            }
        }


        // user defined function

        void deleteMemberByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member x = memberDAO.find(m);
            if (x != null)
            {
                if (memberDAO.delete(m))
                {
                    lstMsg.Add("Delete successfull ");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
                else
                {
                    lstMsg.Add("Delete operation in database failed");
                    txtClasse = "warning";
                }
            }
            else {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }
           
        }

        void getMemberByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member x = memberDAO.find(m);
            if (x != null)
            {
                TextBox2.Text = x.fullname;
                TextBox7.Text = x.account_status;
                TextBox8.Text = x.dob;
                TextBox3.Text = x.contact_no;
                TextBox4.Text = x.email;
                TextBox9.Text = x.state;
                TextBox10.Text = x.city;
                TextBox11.Text = x.pincode;
                TextBox6.Text = x.fulladdress;
            }
            else {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }
        }

        void updateMemberStatusByID(string status)
        {
            m.account_status = status;
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member x = memberDAO.find(m);
            if (x != null)
            {
                x.account_status = status;
                if (memberDAO.update(x))
                {
                    lstMsg.Add("This member is now "+x.account_status);
                    txtClasse = "success";
                    getMemberByID();
                    GridView1.DataBind();
                }
                else
                {
                    lstMsg.Add("Update operation in database failed");
                    txtClasse = "warning";
                }
            }
            else
            {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }


        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox6.Text = "";
        }
        public bool checkFields()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (m.fullname.Length < 4)
            {
                lstMsg.Add("Fullname lenght must be more or equal to 4 characters");
            }
            if (m.fulladdress.Length == 0)
            {
                lstMsg.Add("Fulladress field is empty");
            }

            if (m.member_id.Length == 0)
            {
                lstMsg.Add("MemberId field is empty");
            }

            if (m.city.Length==0)
            {
                lstMsg.Add("City not choosen");
            }
            if (m.state.Length==0)
            {
                lstMsg.Add("State not choosen");
            }
            if (m.pincode.Length < 8)
            {
                lstMsg.Add("Pinconde must be at least 8 characters");
            }
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (!re.IsMatch(m.email))
            {
                lstMsg.Add("Invalid email");
            }
            if (m.dob.Length == 0)
            {
                lstMsg.Add("Dob not choosen");
            }
            else
            {
                String[] v = m.dob.Split('-');
                DateTime dob = new DateTime(Int32.Parse(v[0]), Int32.Parse(v[1]), Int32.Parse(v[2]));
                DateTime today = DateTime.Now;
                int d = today.Year - dob.Year;
                if (d < 14)
                {
                    lstMsg.Add("The DOB at least 14 years from today: " + today.ToString("dddd,dd MMMM yyyy"));
                }
            }
            if (m.contact_no.Length == 0)
            {
                lstMsg.Add("Invalid contact no (10 digits)");
            }
            else
            {
                strRegex = @"^0[5-8]([-. ]?[0-9]{2}){4}$";
                re = new Regex(strRegex, RegexOptions.IgnoreCase);
                if (!re.IsMatch(m.contact_no))
                {
                    lstMsg.Add("Invalid phone number: 0[5-8] xx xx xx xx | 0[5-8]-xx-xx-xx-xx | 0[5-8]xxxxxxxx");
                }
            }

            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public bool checkId() {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (m.member_id.Length == 0)
            {
                lstMsg.Add("MemberId field is empty");
            }
            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            m.member_id = TextBox1.Text.Trim();
            m.fullname=TextBox2.Text.Trim();
            m.contact_no = TextBox3.Text.Trim();
            m.email = TextBox4.Text.Trim();
            m.fulladdress = TextBox6.Text.Trim();
            m.account_status=TextBox7.Text.Trim();
            m.dob=TextBox8.Text.Trim();      
            m.state=TextBox9.Text.Trim();
            m.city=TextBox10.Text.Trim();
            m.pincode=TextBox11.Text.Trim();
            
        }
    }
}