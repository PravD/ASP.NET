/* Copyright © 2013 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/

   If you have purchased a DayPilot Pro license, you are allowed to use this 
   code under the conditions of DayPilot Pro License Agreement:

   http://www.daypilot.org/files/LicenseAgreement.pdf

   Otherwise, you are allowed to use it for evaluation purposes only under 
   the conditions of DayPilot Pro Trial License Agreement:
   
   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
   
*/

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DayPilot.Utils;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Scheduler;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1));
            DayPilotCalendarDay.DataBind();
        }

    }

    protected void DayPilotCalendarDay_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotCalendarDay.StartDate = (DateTime) e.Data["day"];
                DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1));
                DayPilotCalendarDay.DataBind();
                DayPilotCalendarDay.Update();
                break;
            case "refresh":
                DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1));
                DayPilotCalendarDay.DataBind();
                DayPilotCalendarDay.Update();
                break;
        }
    }

    protected void DayPilotCalendarWeek_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotCalendarWeek.StartDate = (DateTime)e.Data["day"];
                DayPilotCalendarWeek.DataSource = GetData(DayPilotCalendarWeek.StartDate, DayPilotCalendarWeek.EndDate.AddDays(1));
                DayPilotCalendarWeek.DataBind();
                DayPilotCalendarWeek.Update();
                break;
            case "refresh":
                DayPilotCalendarWeek.DataSource = GetData(DayPilotCalendarWeek.StartDate, DayPilotCalendarWeek.EndDate.AddDays(1));
                DayPilotCalendarWeek.DataBind();
                DayPilotCalendarWeek.Update();
                break;
        }
    }

    protected void DayPilotMonth1_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotMonth1.StartDate = (DateTime)e.Data["day"];
                DayPilotMonth1.DataSource = GetData(DayPilotMonth1.VisibleStart, DayPilotMonth1.VisibleEnd);
                DayPilotMonth1.DataBind();
                DayPilotMonth1.Update();
                break;
            case "refresh":
                DayPilotMonth1.DataSource = GetData(DayPilotMonth1.VisibleStart, DayPilotMonth1.VisibleEnd);
                DayPilotMonth1.DataBind();
                DayPilotMonth1.Update();
                break;
        }
    }


    private DataTable GetData(DateTime start, DateTime end)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("start", start);
        da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

}
