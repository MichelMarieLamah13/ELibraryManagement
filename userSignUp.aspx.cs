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
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class userSignUp : System.Web.UI.Page
    {
        public Member m { get; set; }
        public string txtClasse {get; set;}
        public List<string> lstMsg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillState();
                fillCity();
            }
            m = new Member();
            txtClasse = "";
            lstMsg = new List<string>();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                signUpNewMember();
            }
        }

        
        public void signUpNewMember()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member x = memberDAO.find(m);
            if (x == null)
            {
                if (memberDAO.create(m))
                {
                    lstMsg.Add("Welcome "+m.member_id+". You can now login");
                    txtClasse = "success";
                }
                else {
                    lstMsg.Add("Add operation in database failed");
                    txtClasse = "warning";
                }
            }
            else {
                lstMsg.Add("This member already exists");
                txtClasse = "warning";
            }
           
        }

        public void fillState()
        {
            DropDownList1.Items.Clear();
            ListItem item = new ListItem("Select your state", "Select");
            DropDownList1.Items.Add(item);
            List<State> lst = State.getAll();
            if (lst != null) {
                foreach (State s in lst)
                {
                    item = new ListItem(s.state_name, s.state_name);
                    DropDownList1.Items.Add(item);
                }
            }
        }

        public void fillCity()
        {
            DropDownList2.Items.Clear();
            String state = DropDownList1.SelectedItem.Value;

            ListItem item = new ListItem("Select your city", "Select");
            DropDownList2.Items.Add(item);
            List<City> lst = City.getByState(state);
            if (lst != null) {
                foreach (City c in lst)
                {
                    item = new ListItem(c.city_name, c.city_name);
                    DropDownList2.Items.Add(item);
                }
            }       
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            fillCity();
        }

        public bool checkFields()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (m.fullname.Length < 4) {
                lstMsg.Add("Fullname lenght must be more or equal to 4 characters");
            }
            if (m.fulladdress.Length ==0) {
                lstMsg.Add("Fulladress field is empty");
            }

            if (m.password.Length < 6) {
                lstMsg.Add("Password must be at least 6 characters");
            }

            if (m.member_id.Length == 0) {
                lstMsg.Add("MemberId field is empty");
            }

            if (m.city == "Select") {
                lstMsg.Add("City not choosen");
            }
            if (m.state == "Select")
            {
                lstMsg.Add("State not choosen");
            }
            if (m.pincode.Length < 8) {
                lstMsg.Add("Pinconde must be at least 8 characters");
            }
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (!re.IsMatch(m.email)) {
                lstMsg.Add("Invalid email");
            }
            if (m.dob.Length == 0)
            {
                lstMsg.Add("Dob not choosen");
            }
            else {
                String[] v = m.dob.Split('-');
                DateTime dob = new DateTime(Int32.Parse(v[0]), Int32.Parse(v[1]), Int32.Parse(v[2]));
                DateTime today = DateTime.Now;
                int d = today.Year-dob.Year;
                if (d < 14) {
                    lstMsg.Add("The DOB at least 14 years from today: " + today.ToString("dddd,dd MMMM yyyy"));
                }
            }
            if (m.contact_no.Length == 0)
            {
                lstMsg.Add("Invalid contact no (10 digits)");
            }
            else {
                strRegex = @"^0[5-8]([-. ]?[0-9]{2}){4}$";
                re = new Regex(strRegex, RegexOptions.IgnoreCase);
                if (!re.IsMatch(m.contact_no)) {
                    lstMsg.Add("Invalid phone number: 0[5-8] xx xx xx xx | 0[5-8]-xx-xx-xx-xx | 0[5-8]xxxxxxxx");
                }
            }

            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count==0;
        }

        public void getValues() { 
            m.fullname = TextBox1.Text.Trim();
            m.dob = TextBox2.Text.Trim();
            m.contact_no = TextBox3.Text.Trim();
            m.email = TextBox4.Text.Trim();
            m.state = DropDownList1.SelectedItem.Value;
            m.city = DropDownList2.SelectedItem.Value;
            m.pincode = TextBox7.Text.Trim();
            m.fulladdress = TextBox5.Text.Trim();
            m.member_id = TextBox8.Text.Trim();
            m.password = TextBox9.Text.Trim();
            m.account_status = "pending";
        }
    }
}