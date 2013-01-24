<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="PII.UI.Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PII</title>
    <link rel="stylesheet" type="text/css" href="../Styles/kube.min.css" />
</head>
<body>
    <form id="form1" runat="server" class="forms  columnar" runat="server">
    <div style="padding: 10px;" class="threequarter">
        <span style="text-align: center;">
            <h3>
                Fill in the details for
                <asp:Label ID="lblName" runat="server"></asp:Label></h3>
        </span>
        <fieldset>
            <legend>Infomation </legend>
            <ul>
                <li>
                    <label class="bold" for="foo">
                        Name <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtName" required="required" type="text" placeholder="" />
                    <span class="descr">Albert Einstein</span> </li>
                <li>
                    <fieldset>
                        <section class="bold">Date of birth <span class="req">*</span></section>
                        <ul class="multicolumn">
                            <li>
                                <asp:DropDownList runat="server" ID="drpMonth">
                                    <asp:ListItem>---</asp:ListItem>
                                </asp:DropDownList>
                                <div class="descr">
                                    Month</div>
                            </li>
                            <li>
                                <asp:DropDownList runat="server" ID="drpDay">
                                    <asp:ListItem>---</asp:ListItem>
                                </asp:DropDownList>
                                <div class="descr">
                                    Day</div> 
                            </li>
                            <li> 
                                <asp:DropDownList runat="server" ID="drpYear">
                                    <asp:ListItem>---</asp:ListItem>
                                </asp:DropDownList>
                                <div class="descr">
                                    Year</div>
                            </li>
                        </ul>
                    </fieldset>
                </li>
                <li class="form-section">Personal</li>
                <li>
                    <label class="bold" for="foo">
                        Spouse's Name <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtSpouse" required="required" type="text" placeholder="" />
                    <span class="descr">Megan Fox</span> </li>
                <li>
                    <label class="bold" for="foo">
                        Children's Name <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtChildren" TextMode="MultiLine" required="required"
                        Style="height: 50px;" class="width-20" />
                    <span class="descr">John Peter, Tom Cruise</span> </li>
                <li>
                    <label class="bold" for="foo">
                        Grand Children's Name <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtGrandChildren" TextMode="MultiLine" required="required"
                        Style="height: 50px;" class="width-20" />
                    <span class="descr">Issac Newton, James Mathew</span> </li>
                <li class="form-section">Contact</li>
                <li>
                    <label class="bold" for="foo">
                        Email <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtEmail" required="required" type="email" class="five" />
                    <div class="descr">
                        albert@gmail.com</div>
                </li>
                <li>
                    <label class="bold" for="foo">
                        Phone number <span class="req">*</span></label>
                    (
                    <asp:TextBox runat="server" ID="txtPhone1" required="required" type="text" size="3" />
                    )
                    <asp:TextBox runat="server" ID="txtPhone2" required="required" type="text" size="3" />
                    -
                    <asp:TextBox runat="server" ID="txtPhone3" required="required" type="text" size="4" />
                    <div class="descr">
                        (754) 315 - 2159</div>
                </li>
                <li>
                    <label class="bold" for="foo">
                        Street Address <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtStreet" required="required" type="text" class="five" />
                    <div class="descr">
                        124 Main Street</div>
                </li>
                <li>
                    <label class="bold" for="foo">
                        City <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtCity" required="required" type="text" class="five" />
                    <div class="descr">
                        Buffalo </div>
                </li>
                <li>
                    <label class="bold" for="foo">
                        State <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtState" required="required" type="text" class="five" />
                    <div class="descr">
                        New York </div>
                </li>
                <li>
                    <label class="bold" for="foo">
                        Zip <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtZip" required="required" type="text" class="five" />
                    <div class="descr">
                        15489</div>
                </li>
                <li class="form-section">Employment</li>
                <li>
                    <label class="bold" for="foo">
                        Past Employer's Names <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtPastEmployer" TextMode="MultiLine" required="required"
                        Style="height: 50px;" class="width-20" />
                    <span class="descr">Ford, Fishcer Price</span> </li>
                <li>
                    <label class="bold" for="foo">
                        Current Employer's Names <span class="req">*</span></label>
                    <asp:TextBox runat="server" ID="txtCurrentEmployer" TextMode="MultiLine" required="required"
                        Style="height: 50px;" class="width-20" />
                    <span class="descr">Bank of America, Boeing</span> </li>
                <li class="push">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" type="button" 
                        CssClass="btn" onclick="btnSubmit_Click"/>
                </li>
            </ul>
        </fieldset>
    </div>
    </form>
</body>
</html>
