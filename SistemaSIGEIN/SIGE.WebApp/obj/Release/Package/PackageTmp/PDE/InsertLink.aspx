<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertLink.aspx.cs" Inherits="SIGE.WebApp.PDE.InsertLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agregar link</title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset style="width: 220px; height: 100px">
            Título:
        <input type="text" id="linkName" /><br />
            <br />
            Link:  
         <input type="text" id="linkUrl" /><br />
     
   <%--         Link class:
        <input type="text" id="linkClass" value=""/><br />--%>
            <br />
            <input type="button" onclick="javascript: insertLink();" value="Agregar link" />
        </fieldset>

        <script type="text/javascript">
            if (window.attachEvent) {
                window.attachEvent("onload", initDialog);
            }
            else if (window.addEventListener) {
                window.addEventListener("load", initDialog, false);
            }


            var linkUrl = document.getElementById("linkUrl");
            var linkTarget = document.getElementById("linkUrl");
            var linkClass = "";
            var linkName = document.getElementById("linkName");

            var workLink = null;

            function getRadWindow() {
                if (window.radWindow) {
                    return window.radWindow;
                }
                if (window.frameElement && window.frameElement.radWindow) {
                    return window.frameElement.radWindow;
                }
                return null;
            }

            function initDialog() {
                var clientParameters = getRadWindow().ClientParameters; //return the arguments supplied from the parent page

                linkUrl.value = clientParameters.href;
                linkTarget.value = clientParameters.target;
                linkClass.value = clientParameters.className;
                linkName.value = clientParameters.innerHTML;

                workLink = clientParameters;
            }

            function insertLink() //fires when the Insert Link button is clicked
            {
                //create an object and set some custom properties to it      
                workLink.href = linkUrl.value;
                workLink.target = linkTarget.value;
                workLink.className = linkClass.value;
                workLink.innerHTML = linkName.value;

                getRadWindow().close(workLink); //use the close function of the getRadWindow to close the dialog and pass the arguments from the dialog to the callback function on the main page.
            }
        </script>

    </form>
</body>
</html>
