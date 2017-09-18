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
    public partial class Cart : System.Web.UI.Page
    {
        static string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = WebConfigurationManager.ConnectionStrings["productDb"].ConnectionString;
            if (Session["cart"] != null)
            {
                var invertory = (DataTable)Session["inventory"];
                var cart = (Dictionary<string, int>)Session["cart"];
                if (cart.Count == 0)
                {
                    PlaceOrder.Visible = false;
                    Total_Amount.Visible = false;
                    TotalAmout.Visible = false;
                    MessageLabel.Text = "Cart is empty";
                }
                else
                {
                    double totalAmount = 0;
                    PlaceOrder.Visible = true;
                    Total_Amount.Visible = true;
                    TotalAmout.Visible = true;
                    MessageLabel.Text = "Items in the cart are:";
                    var cartDataTable = GetCartData(cart, invertory, out totalAmount);
                    TotalAmout.Text = totalAmount.ToString();
                    CartGridView.DataSource = cartDataTable;
                    CartGridView.DataBind();
                }
            }
            else
            {
                PlaceOrder.Visible = false;
                Total_Amount.Visible = false;
                TotalAmout.Visible = false;
                MessageLabel.Text = "Cart is empty.";
            }
        }

        protected void Shopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            var orderCount = GetOrdersCount();
            var orderId = SaveOrder(orderCount);

            var inventory = (DataTable)Session["inventory"];
            var cart = (Dictionary<string, int>)Session["cart"];

            foreach (var key in cart.Keys)
            {
                var row = inventory.Select($"p_id = {key}").First();
                var price = Convert.ToDecimal(row["Price"]);
                SaveOrderItem(orderId, key, cart[key], price);
            }
            
            Session["cart"] = new Dictionary<string, int>();
            Response.Redirect("Index.aspx");
        }

        private void SaveOrderItem(string orderId, string productId, int quantity, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string command = $"insert into orderDetails values('{orderId}','{productId}','{quantity}','{price}')";
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private string SaveOrder(int orderCount)
        {
            var orderId = $"Order{++orderCount}";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string command = $"insert into orders values('{orderId}','{DateTime.UtcNow}','{TotalAmout.Text}')";
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                return orderId;
            }
        }

        private int GetOrdersCount()
        {
            var orderCount = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string command = "select count(*) as 'Count' from Orders;";
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                var data = reader["count"].ToString();
                if (int.TryParse(data, out orderCount))
                    return orderCount;
                return 0;
            }
        }

        private DataTable GetCartData(Dictionary<string, int> cart, DataTable inventory, out double totalAmount)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Rate", typeof(double)));
            dataTable.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dataTable.Columns.Add(new DataColumn("SubTotal", typeof(double)));
            totalAmount = 0;
            foreach (var key in cart.Keys)
            {
                var row = inventory.Select($"p_id = {key}").First();
                var newCartRow = dataTable.NewRow();
                newCartRow["ProductName"] = row["Name"];
                newCartRow["Rate"] = row["Price"];
                newCartRow["Quantity"] = cart[key];
                var subTotal = Convert.ToDouble(row["Price"]) * cart[key];
                totalAmount += subTotal;
                newCartRow["SubTotal"] = subTotal;

                dataTable.Rows.Add(newCartRow);
            }

            return dataTable;
        }

    }
}