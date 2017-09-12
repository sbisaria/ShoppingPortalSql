<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="OnlineStore.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="MessageLabel" runat="server" Text="Items in the cart are:" ForeColor="Blue" Font-Size="XX-Large"></asp:Label>
            <asp:GridView ID="CartGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:Label ID="Label2" runat="server" Text="Total: "></asp:Label>
            <asp:Label ID="TotalAmout" runat="server" ForeColor="#0066FF" Text="00"></asp:Label>
            <br />
            <asp:Button ID="PlaceOrder" runat="server" Text="Place Order" OnClick="PlaceOrder_Click" /><asp:Button ID="Shopping" runat="server" Text="Continue Shopping.." OnClick="Shopping_Click" />
        </div>
    </form>
</body>
</html>
