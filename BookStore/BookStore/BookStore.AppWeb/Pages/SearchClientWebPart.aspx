<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchClientWebPart.aspx.cs" Inherits="BookStore.AppWeb.Pages.SearchClientWebPart" %>
<%@ Register TagPrefix="webparts" TagName="SearchWebPart" Src="~/WebParts/SearchWebPart.ascx" %>

<!DOCTYPE html>

<html>
<head>
    <title></title>
</head>
<body>
    <form runat="server">
        <webparts:SearchWebPart runat="server" />
    </form>
</body>
</html>
