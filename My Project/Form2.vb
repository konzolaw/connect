Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports MySql.Data.MySqlClient

Public Class Form2
    Private Sub LinkLabelEsc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelEsc.LinkClicked
        ' Display a message box with a personalized message
        MessageBox.Show($"Thank you for working with us", "Message")

        ' Create an instance of Form1
        Dim Form1 As New Form1()

        ' Show Form1 and hide the current form (PatientsRegistry)
        LoginForm.Show()
        Me.Hide()
    End Sub
    Dim connectionString As String = "server=localhost;database=pharmacydb;user=root;password=Irke@12843"
    Private Sub PatientList_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Connect to the database using the connection string
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Fetch data for DataGridView2 (PatientQueueList)
                Dim query2 As String = "SELECT v.VisitID, CONCAT(p.FirstName, ' ', p.LastName) AS PatientName, p.PatientUPI, q.QueueStatus, q.DepartmentFrom, q.DepartmentTo " &
                            "FROM Visits v " &
                            "INNER JOIN Patients p ON v.PatientUPI = p.PatientUPI " &
                            "INNER JOIN Queues q ON v.PatientUPI = q.PatientUPI"
                Using command2 As New MySqlCommand(query2, connection)
                    Using reader2 As MySqlDataReader = command2.ExecuteReader()
                        DataGridView2.Rows.Clear()

                        While reader2.Read()
                            Dim visitID As String = reader2("VisitID").ToString()
                            Dim patientName As String = reader2("PatientName").ToString()
                            Dim patientUPI As String = reader2("PatientUPI").ToString()
                            Dim queueStatus As String = reader2("QueueStatus").ToString()
                            Dim departmentFrom As String = reader2("DepartmentFrom").ToString()
                            Dim departmentTo As String = reader2("DepartmentTo").ToString()

                            DataGridView2.Rows.Add(visitID, patientName, patientUPI, queueStatus, departmentFrom, departmentTo)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur during the data retrieval process
            MessageBox.Show("Error fetching data for DataGridView2: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Dim searchPatientName As String = TextBoxSearch.Text.Trim()

        ' Check if the searchPatientName is not empty or whitespace
        If Not String.IsNullOrWhiteSpace(searchPatientName) Then
            Try
                ' Connect to the database using the connection string
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    ' Define the SQL query to fetch data for DataGridView2 based on the PatientName
                    Dim query As String = "SELECT v.VisitID, CONCAT(p.FirstName, ' ', p.LastName) AS PatientName, p.PatientUPI, q.QueueStatus, q.DepartmentFrom, q.DepartmentTo " &
                                    "FROM Visits v " &
                                    "INNER JOIN Patients p ON v.PatientUPI = p.PatientUPI " &
                                    "INNER JOIN Queues q ON v.PatientUPI = q.PatientUPI " &
                                    "WHERE CONCAT(p.FirstName, ' ', p.LastName) LIKE @SearchPatientName"

                    ' Create a MySqlCommand object with the SQL query and the connection
                    Using command As New MySqlCommand(query, connection)
                        ' Add the search parameter
                        command.Parameters.AddWithValue("@SearchPatientName", "%" & searchPatientName & "%")

                        ' Create a MySqlDataReader to read the data
                        Using reader As MySqlDataReader = command.ExecuteReader()
                            ' Clear any existing rows in DataGridView2
                            DataGridView2.Rows.Clear()

                            ' Loop through the rows returned by the query
                            While reader.Read()
                                ' Read data from the reader
                                Dim visitID As String = reader("VisitID").ToString()
                                Dim patientName As String = reader("PatientName").ToString()
                                Dim patientUPI As String = reader("PatientUPI").ToString()
                                Dim queueStatus As String = reader("QueueStatus").ToString()
                                Dim departmentFrom As String = reader("DepartmentFrom").ToString()
                                Dim departmentTo As String = reader("DepartmentTo").ToString()

                                ' Add the values to a new row in DataGridView2
                                DataGridView2.Rows.Add(visitID, patientName, patientUPI, queueStatus, departmentFrom, departmentTo)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' Handle any exceptions that occur during the data retrieval process
                MessageBox.Show("Error searching data in the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' If the searchPatientName is empty or whitespace, show a message to the user
            MessageBox.Show("Please enter a valid Patient Name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


End Class