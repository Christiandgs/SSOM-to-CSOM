<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoriesClientWebPart.aspx.cs" Inherits="BookStore.AppWeb.Pages.CategoriesClientWebPart" %>
<%@ Register TagPrefix="webparts" TagName="CategoriesWebPart" Src="~/WebParts/CategoriesWebPart.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title></title>
</head>
<body>
    <form runat="server">
        <webparts:CategoriesWebPart runat="server" />
    </form>
</body>
</html>
