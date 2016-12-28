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
Imports System.Web.UI

Partial Public Class [New]
	Inherits Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then
			TextBoxStart.Text = Convert.ToDateTime(Request.QueryString("start")).ToString("M/d/yyyy HH:mm")
			TextBoxEnd.Text = Convert.ToDateTime(Request.QueryString("end")).ToString("M/d/yyyy HH:mm")
		End If
	End Sub
	Protected Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim start As Date = Convert.ToDateTime(TextBoxStart.Text)
		Dim [end] As Date = Convert.ToDateTime(TextBoxEnd.Text)

		dbInsertEvent(start, [end], TextBoxName.Text)
		Modal.Close(Me, "OK")
	End Sub

	Private Sub dbInsertEvent(ByVal start As Date, ByVal [end] As Date, ByVal name As String)
		Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("daypilot").ConnectionString)
			con.Open()
			Dim cmd As New SqlCommand("INSERT INTO [event] (eventstart, eventend, name) VALUES(@start, @end, @name)", con)
			'cmd.Parameters.AddWithValue("id", id);
			cmd.Parameters.AddWithValue("start", start)
			cmd.Parameters.AddWithValue("end", [end])
			cmd.Parameters.AddWithValue("name", name)
			cmd.ExecuteNonQuery()
		End Using
	End Sub

	Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		Modal.Close(Me)
	End Sub
End Class
