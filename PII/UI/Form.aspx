<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="PII.UI.Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PII</title>
    <link rel="stylesheet" type="text/css" href="../Styles/kube.min.css" />
</head>
<body>
    <form id="form1" runat="server" class="forms  columnar" runat="server">
    <div style="padding: 10px;">
        <span style="text-align: center;">
            <h3>
                Fill in the details for
                <asp:Label ID="lblName" runat="server"></asp:Label></h3>
        </span>
        <fieldset>
            <ul>
                <li>
                    <label for="user_email" class="bold">
                        Email</label>
                    <input type="email" name="user_email" id="user_email" size="40" />
                </li>
                <li>
                    <label for="user_name" class="bold">
                        Name</label>
                    <input type="text" name="user_name" id="user_name" size="40" />
                </li>
                <li>
                    <label class="bold" for="foo">
                        Phone number</label>
                    (
                    <input type="text" size="3" id="foo" name="foo" />
                    )
                    <input type="text" size="3" id="foo" name="foo" />
                    -
                    <input type="text" size="3" id="foo" name="foo" />
                    &nbsp;&nbsp;ext:
                    <input type="text" size="3" id="foo" name="foo" />
                    <div class="descr">
                        Needed if there are questions about your order</div>
                </li>
                <li class="push">
                    <input type="submit" name="send" class="btn" value="Submit" />
                </li>
            </ul>
        </fieldset>
    </div>
    </form>
</body>
</html>
