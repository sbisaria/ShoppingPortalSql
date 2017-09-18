using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineStore
{
    public partial class DataSourceDemo : System.Web.UI.Page
    {
        static string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (IsInputValid() == true)
            {
                try
                {
                    connectionString = WebConfigurationManager.ConnectionStrings["productDb"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string commandString = $"exec add_product '{Name.Text}','{Price.Text}'";
                        SqlCommand command = new SqlCommand(commandString, conn);
                        command.ExecuteNonQuery();
                    }
                    Name.Text = Price.Text = "";
                    Error.Text = "";
                    Error.Visible = false;
                    GridView1.DataBind();
                }
                catch (Exception)
                {
                    Error.Text = "Some error occurred.TRy Again.";
                    Error.Visible = true;
                }
            }
        }
        public bool IsInputValid()
        {
            decimal price = 0;
            if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Price.Text))
            {
                Error.Text = "Invalid values in Name/Price";
                return false;
            }
            else if (decimal.TryParse(Price.Text, out price)==false)
            {
                Error.Text = "Invalid values in Name/Price";
                return false;
            }
            else if (price < 0)
            {
                Error.Text = "Price cannot be less that 0";
                return false;
            }
            else
                Error.Text = "";
            return true;
        }
    }
}