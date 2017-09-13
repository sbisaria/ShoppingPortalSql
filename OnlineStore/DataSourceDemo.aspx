<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataSourceDemo.aspx.cs" Inherits="OnlineStore.DataSourceDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="p_id" DataSourceID="Inventory" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="p_id" HeaderText="Product Id" ReadOnly="True" SortExpression="p_id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
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
            <asp:SqlDataSource ID="Inventory" runat="server" ConnectionString="<%$ ConnectionStrings:OnlineStoreConnection %>"
                DeleteCommand="delete_product" 
                InsertCommand="add_product" InsertCommandType="StoredProcedure"
                SelectCommand="SELECT * FROM [Product]" UpdateCommand="UPDATE [Product] SET [Name] = @Name, [Price] = @Price WHERE [p_id] = @p_id" DeleteCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="p_id" Type="String" />
                    <asp:Parameter Name="o_id" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="p_id" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Price" Type="Double" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Price" Type="Double" />
                    <asp:Parameter Name="p_id" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        &nbsp;&nbsp;&nbsp;
            <br />
            <asp:Label ID="Label3" runat="server" BackColor="#0066CC" ForeColor="White" Height="30px" Text="Add new item :" Width="140px"></asp:Label>
            <br />
            <br />
&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Id"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Price" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            &nbsp;
            <asp:Label ID="Label4" runat="server" Text="Price"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
&nbsp;
            <br />
&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-top: 0px" Text="Add" />
        </div>
    </form>
</body>
</html>
