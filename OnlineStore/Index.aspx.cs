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
        static string connectionString;
        static bool isFirstLoad = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(isFirstLoad == true)
            {
                isFirstLoad = false;
                Session["cart"] = new Dictionary<string, int>();
            }
            connectionString = WebConfigurationManager.ConnectionStrings["productDb"].ConnectionString;
            var inventory = GetData().Tables[0];
            Session["inventory"] = inventory;
            ProductGrid.DataSource = inventory;
            ProductGrid.DataBind();

        }

        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string command = "select * from product";
                    SqlCommand cmd = new SqlCommand(command, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception exception)
            {
                Response.Write("<script>" +
                    "if(confirm('Some error occured'))" +
                    "{" +
                    "window.location='Error.aspx'" +
                    "}"+
                    "</script>");
            }
            return ds;
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button = new LinkButton()
                {
                    ID = e.Row.Cells[0].Text,
                    Text = "Add To Cart"
                };
                button.Click += LinkButton_Click;
                e.Row.Cells.Add(new TableCell());
                e.Row.Cells[3].Controls.Add(button);
            }
        }
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            var button = (LinkButton)sender;
            var itemKey = button.ID;
            int count = 0;
            var cart = (Dictionary<string, int>)Session["cart"];
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