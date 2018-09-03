(function () {
    var $;
    var organigrama = window.organigrama = window.organigrama || {};
    var org;
    var winNodo;
    var winItem;
    var ajaxPanel;

    organigrama.initialize = function () {
        $ = $telerik.$;
        winNodo = window.winNodo;
        winItem = window.winItem;
        org = window.orgChart;
        contextMenu = window.contextMenu;
        //ajaxPanel = window.ajaxPanel;
        setup();
    };

    var nodeId;
    var itemId;

    window.onClientItemClicked = function (sender, args) {
        var item = args.get_item();
        var command = item.get_value();
        switch (command) {
            case "Empleado":
                winItem.set_title("Editar empleado");
                winItem.setUrl("/Administracion/Empleado.aspx?EmpleadoId=" + itemId);
                winItem.SetWidth(document.documentElement.clientWidth - 20);
                winItem.SetHeight(document.documentElement.clientHeight - 20);
                winItem.show();
                winItem.center();
                break;
            case "Puesto":
                winNodo.set_title("Editar puesto");
                winNodo.setUrl("/Administracion/VentanaDescriptivoPuesto.aspx?PuestoId=" + nodeId);
                winNodo.SetWidth(document.documentElement.clientWidth - 20);
                winNodo.SetHeight(document.documentElement.clientHeight - 20);
                winNodo.show();
                winNodo.center();
                break;
            case "Plaza":
                winNodo.set_title("Editar plaza");
                winNodo.setUrl("/Administracion/VentanaPlaza.aspx?PlazaId=" + nodeId);
                winNodo.SetWidth(590);
                winNodo.SetHeight(430);
                winNodo.show();
                winNodo.center();
                break;
        }
    }

    function setup() {

        $(org.get_element()).delegate(".rocItem", $telerik.isTouchDevice ? "touchend" : "contextmenu", function (e) {
            var target = $telerik.getTouchTarget(e);
            if (!$(target).hasClass("rocEmptyItem")) {
                showMenu(e, true);
                var item = org.extractGroupItemFromDomElement(target);
                itemId = item.getId();
                var node = org.extractNodeFromDomElement(target);
                nodeId = node.getId();
            }
        })
        .delegate(".rocGroup", $telerik.isTouchDevice ? "touchend" : "contextmenu", function (e) {
            showMenu(e, false);
            var target = $telerik.getTouchTarget(e);

            var node = org.extractNodeFromDomElement(target);
            nodeId = node.getId();
        });
    }

    function showMenu(e, isVisible) {
        contextMenu.get_items().getItem(0).set_visible(!isVisible);
        contextMenu.get_items().getItem(1).set_visible(isVisible);
        contextMenu.show(e);
        e.preventDefault();
    }

    window.onClientClose = function (sender, args) {
        //if (!sender._isCancelled) {
        //    ajaxPanel.ajaxRequest();
        //}
        //else {
        //    sender._isCancelled = false;
        //}
    }
})();