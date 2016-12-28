<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.master" Title="Event Calendar with Day/Week/Month Views" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="js/modal.js"></script>
	<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/calendar_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/scheduler_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/month_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/navigator_white.css' type="text/css" rel="stylesheet" /> 
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<style type="text/css">
    #toolbar 
    {
    	margin-bottom: 10px;
    }
    
    #toolbar a 
    {
    	display: inline-block;
    	height: 20px;
    	text-decoration: none;
    	padding: 5px;
    	color: #666;
    	border: 1px solid #aaa;

	    background: -webkit-gradient(linear, left top, left bottom, from(#fafafa), to(#e2e2e2));
	    background: -webkit-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -moz-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -ms-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: -o-linear-gradient(top, #fafafa 0%, #e2e2e2);
	    background: linear-gradient(top, #fafafa 0%, #e2e2e2);
	    filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#fafafa", endColorStr="#e2e2e2");    	
    }

</style>

<div style="float:left; width: 150px">
<DayPilot:DayPilotNavigator 
            ID="DayPilotNavigator1"
            runat="server" 
            CssOnly="true"
            CssClassPrefix="navigator_white"
            ShowMonths="3"
            SkiptMonth="3"
            ClientObjectName="dp_navigator"
        />

</div>
<div id="tabs" style="margin-left:150px">
    <div id="toolbar">
    <a href="#" id="toolbar_day">Day</a>
    <a href="#" id="toolbar_week">Week</a>
    <a href="#" id="toolbar_month">Month</a>
    </div>

    <div>
        <DayPilot:DayPilotCalendar 
        ID="DayPilotCalendarDay" 
        runat="server" 
        DataEndField="eventend"
        DataStartField="eventstart" 
        DataTextField="name" 
        DataIdField="id" 
        CssOnly="true"
        CssClassPrefix="calendar_white"
        ViewType="Day"
        ClientObjectName="dp_day"
        OnCommand="DayPilotCalendarDay_Command"
        TimeRangeSelectedHandling="JavaScript"
        TimeRangeSelectedJavaScript="create(start, end);"
        EventClickHandling="JavaScript"
        EventClickJavaScript="edit(e.id())"
        />

        <DayPilot:DayPilotCalendar 
        ID="DayPilotCalendarWeek" 
        runat="server" 
        DataEndField="eventend"
        DataStartField="eventstart" 
        DataTextField="name" 
        DataIdField="id" 
        CssOnly="true"
        CssClassPrefix="calendar_white"
        ViewType="Week"
        ClientObjectName="dp_week"
        OnCommand="DayPilotCalendarWeek_Command"
        TimeRangeSelectedHandling="JavaScript"
        TimeRangeSelectedJavaScript="create(start, end);"
        EventClickHandling="JavaScript"
        EventClickJavaScript="edit(e.id())"
        />

    <DayPilot:DayPilotMonth 
        ID="DayPilotMonth1" 
        runat="server" 
        DataEndField="eventend"
        DataStartField="eventstart" 
        DataTextField="name" 
        DataIdField="id" 
        ClientObjectName="dp_month"
        CssOnly="true"
        CssClassPrefix="month_white"
        EventHeight="25"
        OnCommand="DayPilotMonth1_Command"
        TimeRangeSelectedHandling="JavaScript"
        TimeRangeSelectedJavaScript="create(start, end);"
        EventClickHandling="JavaScript"
        EventClickJavaScript="edit(e.id())"
        />
    </div>
</div>

<script type="text/javascript">
    document.getElementById('header').innerHTML = "";
    var switcher = null;

    function edit(id) {
        var modal = new DayPilot.Modal();
        modal.closed = function () {
            if (this.result == "OK") {
                switcher.active.control.commandCallBack('refresh');
            }
        };
        modal.showUrl("Edit.aspx?id=" + id);
    }

    function create(start, end) {
        var modal = new DayPilot.Modal();
        modal.closed = function () {
            if (this.result == "OK") {
                switcher.active.control.commandCallBack('refresh');
            }
            switcher.active.control.clearSelection();
        };
        modal.showUrl("New.aspx?start=" + start + "&end=" + end);
    }

    $(document).ready(function () {
        switcher = new DayPilot.Switcher();

        switcher.addButton("toolbar_day", dp_day);
        switcher.addButton("toolbar_week", dp_week);
        switcher.addButton("toolbar_month", dp_month);

        switcher.addNavigator(dp_navigator);

        switcher.show(dp_day);
    });

</script>

</asp:Content>