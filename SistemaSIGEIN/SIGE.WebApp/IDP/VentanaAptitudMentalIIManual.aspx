<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalIIManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMentalIIManual" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style id="MyCss" type="text/css">
        .CenterDiv {
            text-align: center;
            padding: 2px;
            line-height: 12px;
            font-family: 'Arial Black';
        }

        .DescripcionStyle {
            padding: 2px;
            line-height: 12px;
            font-family: 'Arial Black';
        }

        .DivControlButtons {
            border: 1px solid #CCC;
            text-align: center;
            width: 200px;
            height: 50px;
            padding: 2px;
            position: fixed;
            right: 9px;
            margin-bottom: 5px;
        }

        .DivMoveLeft {
            text-align: right;
            float: left;
            margin-right: 15px;
            margin-left: 15px;
            width: 142px;
        }

        .DivBtnTerminarDerecha {
            float: right;
            width: 100px;
            height: 46px;
            position: absolute;
            right: 10px;
            bottom: 0px;
            margin-bottom: 2px;
        }


        label 
        {font-weight:bold !important;
         width:25px !important;
        }
      
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var vPruebaEstatus = "";
            function close_window(sender, args) {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                            }
                        });
                        var text = "¿Estas seguro que deseas terminar tu prueba?";
                        radconfirm(text, callBackFunction, 400, 160, null, "");
                        args.set_cancel(true);
            }

            function CloseTest() {
                //window.close();
                GetRadWindow().close();
            }


            function valueChanged(sender, args) {
                var vNewValue = args.get_newValue();

                if (vNewValue != 'a' && vNewValue != 'b' && vNewValue != 'c' && vNewValue != 'd' && vNewValue != 'e') {
                    sender.set_value("");
                }
            }


        </script>
    </telerik:RadCodeBlock>
     <label name="" id="lbltitulo" class="labelTitulo">Aptitud mental II</label>
   <%--  <div style="clear:both;"></div>--%>
    <div style="height: calc(100% - 100px); overflow:auto;">

                <table style="width: 90%;  margin-left: 5%; margin-right: 5%; ">
                    <thead>
                        <tr>
                            <td width="100%"></td>
                        </tr>
                    </thead>
                    <tbody>
                         <tr>
                              <td>
                                    <div class="ctrlBasico">
                                        <div style="clear:both;height:25px" ></div>
                                <label name="" runat="server" id="Label2" style="text-align:center; font-weight:bold; width:70px !important;" >Hoja 1 </label>
                                        </div>
                              </td>
                         </tr>
                              <tr>
                                  
                                <td>
                                    <div class="ctrlBasico">
                                    <label id="Label5" name="lbUno"   runat="server">1:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta1" Width="70px" Style="text-align: center; "  >
                                     <%--  <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                        </div>

                                <div class="ctrlBasico">
                                    <label id="Label4" name="lbDos"   runat="server">2:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta2" Width="70px" Style="text-align: center; " >
                                     <%-- <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>

                                     <div class="ctrlBasico">
                                    <label id="Label3" name="lbTres"   runat="server">3:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta3" Width="70px" Style="text-align: center; " >
                                     <%-- <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <label id="Label1" name="lbCuatro"   runat="server">4:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta4" Width="70px" Style="text-align: center; " >
                                      <%-- <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lbCinco" name="lbCinco"   runat="server">5:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta5" Width="70px" Style="text-align: center; " >
                                       <%-- <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                </div>

                                    <div class="ctrlBasico">
                                    <label id="lbSeis" name="lbUno"   runat="server">6:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta6" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                        </div>

                                <div class="ctrlBasico">
                                    <label id="lbSiete" name="lbDos"   runat="server">7:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta7" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb8" name="lb8"   runat="server">8:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta8" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb9" name="lb9"   runat="server">9:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta9" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb10" name="lb10"  runat="server">10:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta10" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>

                                    <div class="ctrlBasico">
                                    <label id="lb11" name="lb11"   runat="server">11:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta11" Width="70px"  Style="text-align: center; ">
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb12" name="lb12"   runat="server">12:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta12" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb13" name="lb13"   runat="server">13:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta13" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb14" name="lb14"   runat="server">14:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta14" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb15" name="lb15"   runat="server">15:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta15" Width="70px"  Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div style="clear: both;"></div>
                                    </td>
                        </tr>
                            <tr>
                              <td>
                                <div style="clear:both;height:25px" ></div>
                                <label name="" runat="server" id="Label6" style="text-align:center; font-weight:bold; width:70px !important;" >Hoja 2 </label>
                              </td>
                         </tr>
                         <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <label id="lb16" name="lb16"   runat="server">16:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta16" Width="70px" Style="text-align: center; " >
                                   <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                            </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb17" name="lb17"   runat="server">17:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta17" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb18" name="lb18"   runat="server">18:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta18" Width="70px" Style="text-align: center; ">
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb19" name="lb19"   runat="server">19:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta19" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb20" name="lb20"   runat="server">20:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta20" Width="70px"  Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                    <div class="ctrlBasico">
                                    <label id="lb21" name="lb21"   runat="server">21:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta21" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb22" name="lb22"   runat="server">22:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta22" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb23" name="lb23"   runat="server">23:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta23" Width="70px" Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb24" name="lb24"   runat="server">24:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta24" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb25" name="lb25"   runat="server">25:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta25" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                    </div>
                              
                                    <div class="ctrlBasico">
                                    <label id="lb26" name="lb26"   runat="server">26:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta26" Width="70px" Style="text-align: center; " >
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb27" name="lb27"   runat="server">27:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta27" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb28" name="lb28"   runat="server">28:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta28" Width="70px" Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb29" name="lb29"   runat="server">29:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta29" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb30" name="lb30"   runat="server">30:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta30" Width="70px"  Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>
                            <tr>
                              <td>
                                <div style="clear:both;height:25px" ></div>
                                <label name="" runat="server" id="Label7" style="text-align:center; font-weight:bold; width:70px !important;" >Hoja 3 </label>
                              </td>
                         </tr>
                         <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <label id="lb31" name="lb31"   runat="server">31:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta31" Width="70px" Style="text-align: center; "  >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb32" name="lb32"   runat="server">32:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta32" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb33" name="lb33"   runat="server">33:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta33" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb34" name="lb34"   runat="server">34:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta34" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb35" name="lb35"   runat="server">35:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta35" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                          
                                    <div class="ctrlBasico">
                                    <label id="lb36" name="lb26"   runat="server">36:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta36" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb37" name="lb27"   runat="server">37:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta37" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb38" name="lb38"   runat="server">38:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta38" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb39" name="lb39"   runat="server">39:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta39" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb40" name="lb40"   runat="server">40:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta40" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                    <div class="ctrlBasico">
                                    <label id="lb41" name="lb41"   runat="server">41:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta41" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb42" name="lb42"   runat="server">42:</label>
                                  
                                      <telerik:RadTextBox  runat="server" ID="txtPregunta42" Width="70px" Style="text-align: center; " >
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                      </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb43" name="lb43"   runat="server">43:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta43" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb44" name="lb44"   runat="server">44:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta44" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb45" name="lb45"   runat="server">45:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta45" Width="70px"  Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div style="clear: both;"></div>
                            </td>
                        </tr>
                        <tr>
                              <td>
                                <div style="clear:both;height:25px" ></div>
                                <label name="" runat="server" id="Label8" style="text-align:center; font-weight:bold; width:70px !important;" >Hoja 4 </label>
                              </td>
                         </tr>
                         <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <label id="lb46" name="lb46"   runat="server">46:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta46" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb47" name="lb47"   runat="server">47:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta47" Width="70px" Style="text-align: center; "  >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb48" name="lb48"   runat="server">48:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta48" Width="70px" Style="text-align: center; ">
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb49" name="lb49"   runat="server">49:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta49" Width="70px" Style="text-align: center; " >
                                   <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                            </telerik:RadTextBox>
                                </div>
                        
                                <div class="ctrlBasico">
                                    <label id="lb50" name="lb50"   runat="server">50:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta50" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                         
                                    <div class="ctrlBasico">
                                    <label id="lb51" name="lb51"   runat="server">51:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta51" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb52" name="lb52"   runat="server">52:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta52" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb53" name="lb53"   runat="server">53:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta53" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb54" name="lb54"   runat="server">54:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta54" Width="70px" Style="text-align: center; ; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb55" name="lb55"   runat="server">55:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta55" Width="70px"  Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                       
                                    <div class="ctrlBasico">
                                    <label id="lb56" name="lb56"   runat="server">56:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta56" Width="70px"  Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb57" name="lb57"   runat="server">57:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta57" Width="70px" Style="text-align: center; " >
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb58" name="lb58"   runat="server">58:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta58" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb59" name="lb59"   runat="server">59:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta59" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb60" name="lb60"   runat="server">60:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta60" Width="70px"  Style="text-align: center; ">
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                </div>
                                <div style="clear: both;"></div>
                                </td>
                         </tr>
                        <tr>
                            <td>
                                <div style="clear:both;height:25px" ></div>
                                <label name="" runat="server" id="Label9" style="text-align:center; font-weight:bold; width:70px !important;" >Hoja 5 </label>
                              </td>
                         </tr>
                       <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <label id="lb61" name="lb61"   runat="server">61:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta61" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb62" name="lb62"   runat="server">62:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta62" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb63" name="lb63"   runat="server">63:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta63" Width="70px" Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb64" name="lb64"   runat="server">64:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta64" Width="70px"  Style="text-align: center; ">
                                    <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                           </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb65" name="lb65"   runat="server">65:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta65" Width="70px" Style="text-align: center; " >
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                          
                                    <div class="ctrlBasico">
                                    <label id="lb66" name="lb66"   runat="server">66:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta66" Width="70px" Style="text-align: center; " >
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb67" name="lb67"   runat="server">67:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta67" Width="70px" Style="text-align: center; " >
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb68" name="lb68"   runat="server">68:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta68" Width="70px" Style="text-align: center; ">
                                      <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                         </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb69" name="lb69"   runat="server">69:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta69" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb70" name="lb70"   runat="server">70:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta70" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                         
                                    <div class="ctrlBasico">
                                    <label id="lb71" name="lb71"   runat="server">71:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta71" Width="70px" Style="text-align: center; " >
                                       <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                    </telerik:RadTextBox>
                                        </div>
                                <div class="ctrlBasico">
                                    <label id="lb72" name="lb72"   runat="server">72:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta72" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                     <div class="ctrlBasico">
                                    <label id="lb73" name="lb73"   runat="server">73:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta73" Width="70px" Style="text-align: center; ">
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb74" name="lb74"   runat="server">74:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta74" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <label id="lb75" name="lb75"   runat="server">75:</label>
                                    <telerik:RadTextBox  runat="server" ID="txtPregunta75" Width="70px" Style="text-align: center; " >
                                     <%-- <ClientEvents OnValueChanged="valueChanged" />--%>
                                          </telerik:RadTextBox>
                                </div>
                                <div style="clear: both;"></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
         <div style="clear: both; height: 10px;"></div>
        <div class="divControlDerecha" style="margin-right:30px">
        Tiempo de captura:  <telerik:RadNumericTextBox runat="server" ID="txtnMinutosLaboral2" Value="0" Name="txnMinutos" Width="90px" MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" CssClass="LeftAligned" AutoPostBack="false" ></telerik:RadNumericTextBox> (min)
            </div>
        <div style="clear: both; height: 10px;"></div>
       </div>
    <div style="clear: both; height: 10px;"></div>
     <div class="DivBtnTerminarDerecha ">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
