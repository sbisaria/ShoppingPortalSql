using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace OnlineStore
{
    public partial class Index : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["productDb"].ConnectionString;
            string command = "select * from product";
            ProductGrid.DataSource = getData(connectionString,command).Tables[0];
            ProductGrid.DataBind();

        }

        private DataSet getData(string connectionString,string command)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button = new LinkButton()
                {
                    ID = e.Row.RowIndex.ToString(),
                    Text = "Add"
                };
                button.Click += LinkButton_Click;

                e.Row.Cells[5].Controls.Add(button);
            }
        }
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            var button = (LinkButton)sender;
            var itemKey = Convert.ToInt32(button.ID);
            int count = 0;
            var cart = (Dictionary<int, int>)Session["cart"];
            if (cart.TryGetValue(itemKey, out count))
                cart[itemKey] += 1;
            else
                cart[itemKey] = 1;
            Session["cart"] = cart;
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}