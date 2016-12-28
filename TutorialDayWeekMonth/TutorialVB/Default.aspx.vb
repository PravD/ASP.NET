' Copyright © 2013 Annpoint, s.r.o.
'   Use of this software is subject to license terms. 
'   http://www.daypilot.org/
'
'   If you have purchased a DayPilot Pro license, you are allowed to use this 
'   code under the conditions of DayPilot Pro License Agreement:
'
'   http://www.daypilot.org/files/LicenseAgreement.pdf
'
'   Otherwise, you are allowed to use it for evaluation purposes only under 
'   the conditions of DayPilot Pro Trial License Agreement:
'   
'   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
'   
'

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports DayPilot.Utils
Imports DayPilot.Web.Ui
Imports DayPilot.Web.Ui.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Events
Imports DayPilot.Web.Ui.Events.Scheduler

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then
			DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1))
			DayPilotCalendarDay.DataBind()
		End If

	End Sub

	Protected Sub DayPilotCalendarDay_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
		Select Case e.Command
			Case "navigate"
				DayPilotCalendarDay.StartDate = CDate(e.Data("day"))
				DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1))
				DayPilotCalendarDay.DataBind()
				DayPilotCalendarDay.Update()
			Case "refresh"
				DayPilotCalendarDay.DataSource = GetData(DayPilotCalendarDay.StartDate, DayPilotCalendarDay.EndDate.AddDays(1))
				DayPilotCalendarDay.DataBind()
				DayPilotCalendarDay.Update()
		End Select
	End Sub

	Protected Sub DayPilotCalendarWeek_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
		Select Case e.Command
			Case "navigate"
				DayPilotCalendarWeek.StartDate = CDate(e.Data("day"))
				DayPilotCalendarWeek.DataSource = GetData(DayPilotCalendarWeek.StartDate, DayPilotCalendarWeek.EndDate.AddDays(1))
				DayPilotCalendarWeek.DataBind()
				DayPilotCalendarWeek.Update()
			Case "refresh"
				DayPilotCalendarWeek.DataSource = GetData(DayPilotCalendarWeek.StartDate, DayPilotCalendarWeek.EndDate.AddDays(1))
				DayPilotCalendarWeek.DataBind()
				DayPilotCalendarWeek.Update()
		End Select
	End Sub

	Protected Sub DayPilotMonth1_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
		Select Case e.Command
			Case "navigate"
				DayPilotMonth1.StartDate = CDate(e.Data("day"))
				DayPilotMonth1.DataSource = GetData(DayPilotMonth1.VisibleStart, DayPilotMonth1.VisibleEnd)
				DayPilotMonth1.DataBind()
				DayPilotMonth1.Update()
			Case "refresh"
				DayPilotMonth1.DataSource = GetData(DayPilotMonth1.VisibleStart, DayPilotMonth1.VisibleEnd)
				DayPilotMonth1.DataBind()
				DayPilotMonth1.Update()
		End Select
	End Sub


	Private Function GetData(ByVal start As Date, ByVal [end] As Date) As DataTable
		Dim da As New SqlDataAdapter("SELECT * FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings("daypilot").ConnectionString)
		da.SelectCommand.Parameters.AddWithValue("start", start)
		da.SelectCommand.Parameters.AddWithValue("end", [end])

		Dim dt As New DataTable()
		da.Fill(dt)

		Return dt
	End Function

End Class
