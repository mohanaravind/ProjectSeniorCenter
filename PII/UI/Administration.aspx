<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administration.aspx.cs"
    Inherits="PII.UI.Administration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Websites Administration</title>
    <link rel="stylesheet" type="text/css" href="../Styles/kube.min.css" />
    <link href="../Styles/mystyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="bgnoise">
    <form id="form1" runat="server" class="forms">
    <div style="padding: 10px; padding-top: 20px; width: 600px; margin: 0 auto;">
        <span style="text-align: center;">
            <h3>
                Administration
            </h3>
        </span>
        <fieldset style="background-color: White;">
            <legend>Websites</legend>
            <div>
                <ul class="multicolumn">
                    <li class="width-33">
                        <label class="bold" for="name">
                            Name</label>
                        <asp:TextBox ID="txtName" runat="server" required="required"></asp:TextBox>
                    </li>
                    <li class="width-33">
                        <label class="bold" for="url">
                            URL</label>
                        <asp:TextBox ID="txtURL" runat="server" required="required"></asp:TextBox>
                    </li>
                    <li class="width-33">
                        <label class="bold" for="add">
                            &nbsp;</label>
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" OnClick="btnAdd_Click" />
                    </li>
                </ul>
            </div>
            <br />
            <div>
                <asp:GridView ID="grdWebsites" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no websites to display"
                    CssClass="width-100 bordered" HorizontalAlign="Center" 
                    onrowdeleting="grdWebsites_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>Status</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Image ID="imgStatus" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Status") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:HyperLinkField DataTextField="URL" NavigateUrl="" HeaderText="URL" 
                            DataNavigateUrlFields="URL" />
                        <asp:BoundField DataField="LastUpdatedOn" HeaderText="Created On" />
                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
                    </Columns>
                    <HeaderStyle CssClass="thead-black" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    </form>
</body>
</html>
