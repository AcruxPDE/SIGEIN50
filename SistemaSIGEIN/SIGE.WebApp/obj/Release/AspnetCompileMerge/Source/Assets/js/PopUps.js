function returnDataToParentPopup(sender, args) {
    //get the data that was passed and return it to the parent reference. Note that you need to know that name of the function that will handle the data there
    var info = args.get_argument();
    if (sender.__parentBackReference && sender.__parentBackReference.useDataFromChild)
        sender.__parentBackReference.useDataFromChild(info);
}

function openChildDialog(url, wndName, title, windowProperties) {
    //in case of erroneous arguments, add some error handling and prevention
    if (!url)
        url = "errorPage.aspx";
    if (!wndName)
        wndName = "popup_" + Math.random();
    var currentWnd = GetRadWindow();
    var browserWnd = window;
    if (currentWnd)
        browserWnd = currentWnd.BrowserWindow;
    setTimeout(function () {
        var a = getModuleParameter(url);
        var wnd = browserWnd.radopen(a, wndName);
        wnd.__parentBackReference = window; //pass the current window object of the page that opens the dialog so it can be used later

        if (title)
            wnd.set_title(title);

        if (windowProperties) {
            if (windowProperties.width)
                wnd.SetWidth(windowProperties.width);
            if (windowProperties.height)
                wnd.SetHeight(windowProperties.height);
        }
        wnd.center();
        //you can pass more parameters for RadWindow settings, e.g., modality, dimensions, etc.
        //you can even add arguments that will pass data from the parent to the child as shown here
        //http://www.telerik.com/help/aspnet-ajax/window-programming-using-radwindow-as-dialog.html
        //in the On the Dialog Page section that shows how to access custom fields in the RadWindow object and use them.
        //of course, you can also use querystrings in the URL.
    }, 0);
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement && window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

function sendDataToParent(dataForParent) {
    //pass the data object onward
    GetRadWindow().close(dataForParent);
}

function confirmAction(sender, args, text, windowProperties) {
    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        if (shouldSubmit) {
            this.click();
        }
    });

    var vWindowsWidth = 400;
    var vWindowsHeight = 150;
    if (windowProperties) {
        if (windowProperties.width)
            vWindowsWidth = windowProperties.width;
        if (windowProperties.height)
            vWindowsHeight = windowProperties.height;
    }

    radconfirm(text, callBackFunction, vWindowsWidth, vWindowsHeight, null, "Confirmar");
    //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
    args.set_cancel(true);
}

function getModuleParameter(url) {
    var m = getParameterByName('m');
    if (m)
        url += (url.indexOf('?') !== -1 ? "&" : "?") + "m=" + m;

    return url;
}

function getParameterByName(name, url) {
    if (!url)
        url = window.location.href;

    //url = url.toLowerCase(); // This is just to avoid case sensitiveness  
    name = name.replace(/[\[\]]/g, "\\$&").toLowerCase();// This is just to avoid case sensitiveness for query parameter name

    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)");
    var results = regex.exec(url);

    if (!results)
        return null;

    if (!results[2])
        return '';

    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function centerPopUp(sender, args) {
    sender.center();
    //sender.moveTo(1, 1);
}
