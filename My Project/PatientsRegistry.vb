Imports System.Net
Imports System.Net.Http
Imports System.Text.Json.Nodes
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json.Linq


Public Class PatientsRegistry




    Private Sub LinkLabelEscLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelEscLogin.LinkClicked
        ' Create an instance of Form1
        Dim Form1 As New Form1()

        ' Show Form1 and hide the current form (PatientsRegistry)
        LoginForm.Show()
        Me.Hide()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ' Display the selected date in the TextBox
        TextBoxDateOfVisit.Text = DateTimePicker1.Value.ToString("yyyy-MM-dd")
    End Sub
    Private Sub DateTimePicker1_Enter(sender As Object, e As EventArgs) Handles DateTimePicker1.Enter
        ' Display the selected date in the TextBoxDateOfBirth
        TextBoxDateOfBirth.Text = DateTimePicker1.Value.ToString("yyyy-MM-dd")
    End Sub



    Private visitCounter As Integer = 1 ' Counter for visit id
    Private queueCounter As Integer = 1 ' Counter for queue number
    Private VisitID As String = "" ' Generated visit id


    Dim connectionString As String = "server=localhost;database=pharmacydb;user=root;password=Irke@12843"


    Private Sub PatientRegistryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Connect to the database using the connection string
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Fetch data for DataGridView1
                Dim query1 As String = "SELECT p.FirstName, p.LastName, p.PatientUPI, p.PhoneNumber, p.NationalID, p.Gender, v.VisitID, v.Date FROM Patients p INNER JOIN Visits v ON p.PatientUPI = v.PatientUPI"
                Using command1 As New MySqlCommand(query1, connection)
                    Using reader1 As MySqlDataReader = command1.ExecuteReader()
                        DataGridView1.Rows.Clear()

                        While reader1.Read()
                            Dim firstName As String = reader1("FirstName").ToString()
                            Dim lastName As String = reader1("LastName").ToString()
                            Dim patientUPI As String = reader1("PatientUPI").ToString()
                            Dim phoneNumber As String = reader1("PhoneNumber").ToString()
                            Dim nationalID As String = reader1("NationalID").ToString()
                            Dim gender As String = reader1("Gender").ToString()
                            Dim visitID As String = reader1("VisitID").ToString()
                            Dim dateofvisit As String = reader1("Date").ToString()

                            DataGridView1.Rows.Add(firstName, lastName, patientUPI, phoneNumber, nationalID, gender, visitID, "", dateofvisit)
                        End While
                    End Using
                End Using

                ' Fetch data for DataGridView2 (PatientQueueList)
                Dim query2 As String = "SELECT q.QueueNumber, CONCAT(p.FirstName, ' ', p.LastName) AS PatientName, q.PatientUPI, q.DepartmentFrom, q.DepartmentTo, q.QueueStatus, v.VisitID " &
                                   "FROM Queues q " &
                                   "INNER JOIN Patients p ON q.PatientUPI = p.PatientUPI " &
                                   "INNER JOIN Visits v ON q.PatientUPI = v.PatientUPI"
                Using command2 As New MySqlCommand(query2, connection)
                    Using reader2 As MySqlDataReader = command2.ExecuteReader()
                        DataGridView2.Rows.Clear()

                        While reader2.Read()
                            Dim queueNumber As String = reader2("QueueNumber").ToString()
                            Dim patientName As String = reader2("PatientName").ToString()
                            Dim patientUPI As String = reader2("PatientUPI").ToString()
                            Dim departmentFrom As String = reader2("DepartmentFrom").ToString()
                            Dim departmentTo As String = reader2("DepartmentTo").ToString()
                            Dim queueStatus As String = reader2("QueueStatus").ToString()
                            Dim visitID As String = reader2("VisitID").ToString()

                            DataGridView2.Rows.Add(queueNumber, patientName, patientUPI, queueStatus, departmentFrom, departmentTo, visitID)
                        End While
                    End Using
                End Using
            End Using

            ' Generate and display visit and queue numbers
            GenerateVisitAndQueueNumbers()

            ' Display the generated visit and queue numbers in the TextBoxes
            TextBoxPatientVisitID.Text = VisitID
            TextBoxQueueNo.Text = queueCounter.ToString()

        Catch ex As Exception
            ' Handle any exceptions that occur during the data retrieval process
            MessageBox.Show("Error fetching data from the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Check if the AddPatients tab is selected
        If PatientRegistryTabControl.SelectedTab IsNot Nothing AndAlso PatientRegistryTabControl.SelectedTab.Name = "AddPatients" Then
            ' If AddPatients tab is selected, display the visit number and queue number in the TextBoxes
            TextBoxPatientVisitID.Text = VisitID
            TextBoxQueueNo.Text = queueCounter.ToString()
        End If
    End Sub
    Private Sub ButtonSearchQueue_Click(sender As Object, e As EventArgs) Handles ButtonSearchQueue.Click
        Dim searchUPI As String = TextBoxSearchQueue.Text.Trim()

        ' Check if the searchUPI is not empty or whitespace
        If Not String.IsNullOrWhiteSpace(searchUPI) Then
            Try
                ' Connect to the database using the connection string
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    ' Define the SQL query to fetch data from the Queues table based on the PatientUPI
                    Dim query As String = "SELECT q.QueueNumber, CONCAT(p.FirstName, ' ', p.LastName) AS PatientName, q.PatientUPI, q.DepartmentFrom, q.DepartmentTo, q.QueueStatus, v.VisitID " &
                                       "FROM Queues q " &
                                       "INNER JOIN Patients p ON q.PatientUPI = p.PatientUPI " &
                                       "INNER JOIN Visits v ON q.PatientUPI = v.PatientUPI " &
                                       "WHERE q.PatientUPI = @SearchUPI"

                    ' Create a MySqlCommand object with the SQL query and the connection
                    Using command As New MySqlCommand(query, connection)
                        ' Add the search parameter
                        command.Parameters.AddWithValue("@SearchUPI", searchUPI)

                        ' Create a MySqlDataReader to read the data
                        Using reader As MySqlDataReader = command.ExecuteReader()
                            ' Clear any existing rows in the DataGridView
                            DataGridView2.Rows.Clear()

                            ' Loop through the rows returned by the query
                            While reader.Read()
                                ' Read data from the reader
                                Dim queueNumber As String = reader("QueueNumber").ToString()
                                Dim patientName As String = reader("PatientName").ToString()
                                Dim patientUPI As String = reader("PatientUPI").ToString()
                                Dim departmentFrom As String = reader("DepartmentFrom").ToString()
                                Dim departmentTo As String = reader("DepartmentTo").ToString()
                                Dim queueStatus As String = reader("QueueStatus").ToString()
                                Dim visitID As String = reader("VisitID").ToString()

                                ' Add the values to a new row in the DataGridView
                                DataGridView2.Rows.Add(queueNumber, patientName, patientUPI, departmentFrom, departmentTo, queueStatus, visitID)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' Handle any exceptions that occur during the data retrieval process
                MessageBox.Show("Error searching data in the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' If the searchUPI is empty or whitespace, show a message to the user
            MessageBox.Show("Please enter a valid Patient UPI.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Private Sub ButtonSearchAllPatients_Click(sender As Object, e As EventArgs) Handles ButtonSearchAllPatients.Click
        Dim searchKeyword As String = TextBoxSearchAllPatients.Text.Trim()

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT p.FirstName, p.LastName, p.PatientUPI, p.PhoneNumber, p.NationalID, p.Gender, v.VisitID, v.Date " &
                                  "FROM Patients p " &
                                  "INNER JOIN Visits v ON p.PatientUPI = v.PatientUPI " &
                                  "WHERE p.PatientUPI = @SearchKeyword OR p.FirstName LIKE CONCAT('%', @SearchKeyword, '%') OR p.LastName LIKE CONCAT('%', @SearchKeyword, '%')"

                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@SearchKeyword", searchKeyword)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        DataGridView1.Rows.Clear()

                        While reader.Read()
                            Dim firstName As String = reader("FirstName").ToString()
                            Dim lastName As String = reader("LastName").ToString()
                            Dim patientUPI As String = reader("PatientUPI").ToString()
                            Dim phoneNumber As String = reader("PhoneNumber").ToString()
                            Dim nationalID As String = reader("NationalID").ToString()
                            Dim gender As String = reader("Gender").ToString()
                            Dim visitID As String = reader("VisitID").ToString()
                            Dim dateofvisit As String = reader("Date").ToString()

                            DataGridView1.Rows.Add(firstName, lastName, patientUPI, phoneNumber, nationalID, gender, visitID, "", dateofvisit)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching data in the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub ButtonSendQueue_Click(sender As Object, e As EventArgs) Handles ButtonSendQueue.Click
        ' Retrieve data from form controls
        Dim patientUPI As String = TextBoxPatientUPI.Text()
        Dim nationalID As String = TextBoxNationalID.Text
        Dim firstName As String = TextBoxFirstName.Text
        Dim lastName As String = TextBoxLastName.Text
        Dim phoneNumber As String = TextBoxPhoneNo.Text
        Dim email As String = TextBoxEmail.Text
        Dim gender As String = ComboBoxGender.SelectedItem.ToString()
        Dim dateOfBirth As Date = DateTime.Parse(TextBoxDateOfBirth.Text)
        Dim DateOfVisit As Date = DateTime.Parse(TextBoxDateOfVisit.Text)
        Dim Homeaddress As String = TextBoxHomeAddress.Text
        Dim queueNo As String = TextBoxQueueNo.Text
        Dim visitID As String = TextBoxPatientVisitID.Text
        Dim nextOfKinName As String = TextBoxNextofKin.Text
        Dim relationship As String = TextBoxRelation2Kin.Text
        Dim nextOfKinContacts As String = TextBoxNOKcontacts.Text
        Dim nextOfKinAddress As String = TextBoxNextofkinaddress.Text
        Dim bloodPressure As String = TextBoxBloodPressure.Text
        Dim heartbeat As Integer = Integer.Parse(TextBoxHeartBeat.Text)
        Dim temperature As Decimal = Decimal.Parse(TextBoxTemperature.Text)
        Dim height As Decimal = Decimal.Parse(TextBoxHeight.Text)
        Dim weight As Decimal = Decimal.Parse(TextBoxWeight.Text)
        Dim PaymentScheme As String = TextBoxPaymentScheme.Text
        Dim allergies As String = TextBoxAllergies.Text
        Dim currentMedications As String = TextBoxCurrentMedication.Text
        Dim visitType As String = ComboBoxVisitType.SelectedItem.ToString()
        Dim departmentFrom As String = ComboBoxDepartmentFrom.SelectedItem.ToString()
        Dim departmentTo As String = ComboBoxDepartmentTO.SelectedItem.ToString()
        Dim queueStatus As String = ComboBoxQueueStatus.SelectedItem.ToString()



        ' Validate the data (you can add more validation as needed)
        If String.IsNullOrWhiteSpace(patientUPI) OrElse
           String.IsNullOrWhiteSpace(firstName) OrElse
            String.IsNullOrWhiteSpace(nationalID) OrElse
            String.IsNullOrWhiteSpace(gender) OrElse
            String.IsNullOrWhiteSpace(Homeaddress) OrElse
            String.IsNullOrWhiteSpace(phoneNumber) OrElse
            String.IsNullOrWhiteSpace(email) OrElse
            String.IsNullOrWhiteSpace(visitID) OrElse
            String.IsNullOrWhiteSpace(nextOfKinAddress) OrElse
            String.IsNullOrWhiteSpace(nextOfKinContacts) OrElse
            String.IsNullOrWhiteSpace(nextOfKinName) OrElse
            String.IsNullOrWhiteSpace(lastName) Then
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Update the data in the database
        Try
            ' Open connection to the database
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Insert data into Patients table
                Dim insertPatientQuery As String = "INSERT INTO Patients (PatientUPI, NationalID, FirstName, LastName, PhoneNumber, Email, Gender, DateOfBirth, QueueNo, NextOfKinName, Relationship, NextOfKinContacts, Homeaddress, NextOfKinaddress, VisitID, PaymentScheme) VALUES (@PatientUPI, @NationalID, @FirstName, @LastName, @PhoneNumber, @Email,@Gender, @DateOfBirth, @Homeaddress, @QueueNo, @VisitID,@PaymentScheme , @NextOfKinName, @Relationship, @NextOfKinContacts, @NextOfKinaddress)"
                Using insertPatientCommand As New MySqlCommand(insertPatientQuery, connection)
                    insertPatientCommand.Parameters.AddWithValue("@PatientUPI", patientUPI)
                    insertPatientCommand.Parameters.AddWithValue("@NationalID", nationalID)
                    insertPatientCommand.Parameters.AddWithValue("@FirstName", firstName)
                    insertPatientCommand.Parameters.AddWithValue("@LastName", lastName)
                    insertPatientCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber)
                    insertPatientCommand.Parameters.AddWithValue("@Email", email)
                    insertPatientCommand.Parameters.AddWithValue("@Gender", gender)
                    insertPatientCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth)
                    insertPatientCommand.Parameters.AddWithValue("@homeaddress", Homeaddress)
                    insertPatientCommand.Parameters.AddWithValue("@QueueNo", queueNo)
                    insertPatientCommand.Parameters.AddWithValue("@VisitID", visitID)
                    insertPatientCommand.Parameters.AddWithValue("@NextOfKinName", nextOfKinName)
                    insertPatientCommand.Parameters.AddWithValue("@Relationship", relationship)
                    insertPatientCommand.Parameters.AddWithValue("@PaymentScheme", PaymentScheme)
                    insertPatientCommand.Parameters.AddWithValue("@NextOfKinContacts", nextOfKinContacts)
                    insertPatientCommand.Parameters.AddWithValue("@NextOfKinaddress", nextOfKinAddress)
                    insertPatientCommand.ExecuteNonQuery()
                End Using

                ' Insert data into Visits table
                Dim insertVisitQuery As String = "INSERT INTO Visits (VisitID, PatientUPI, Date, Notes, VisitType) VALUES (@VisitID, @PatientUPI, @Date, @Notes, @VisitType)"
                Using insertVisitCommand As New MySqlCommand(insertVisitQuery, connection)
                    ' Generate a unique visit ID (e.g., using the current date and time)
                    Dim visit As String = DateTime.Now.ToString("yyyyMMddHHmmss")
                    insertVisitCommand.Parameters.AddWithValue("@VisitID", visitId)
                    insertVisitCommand.Parameters.AddWithValue("@PatientUPI", patientUPI)
                    insertVisitCommand.Parameters.AddWithValue("@VisitType", visitType)
                    insertVisitCommand.Parameters.AddWithValue("@Date", DateOfVisit)
                    insertVisitCommand.Parameters.AddWithValue("@Notes", "") ' You can add notes here if needed
                    insertVisitCommand.ExecuteNonQuery()
                End Using


                ' Insert data into Queues table
                Dim insertQueueQuery As String = "INSERT INTO Queues (QueueID, QueueNumber, QueueStatus, DepartmentFrom, DepartmentTo, PatientUPI) VALUES (@QueueID, @QueueNumber, @QueueStatus, @DepartmentFrom, @DepartmentTo, @PatientUPI)"
                Using insertQueueCommand As New MySqlCommand(insertQueueQuery, connection)
                    ' Generate a unique queue ID (e.g., using the current date)
                    Dim queueID As String = DateTime.Now.ToString("ddMMyyyy")
                    insertQueueCommand.Parameters.AddWithValue("@QueueID", queueID)
                    insertQueueCommand.Parameters.AddWithValue("@QueueNumber", queueNo)
                    insertQueueCommand.Parameters.AddWithValue("@QueueStatus", queueStatus)
                    insertQueueCommand.Parameters.AddWithValue("@DepartmentFrom", departmentFrom)
                    insertQueueCommand.Parameters.AddWithValue("@DepartmentTo", departmentTo)
                    insertQueueCommand.Parameters.AddWithValue("@PatientUPI", patientUPI)
                    insertQueueCommand.ExecuteNonQuery()
                End Using

                ' Insert data into MedicalDetails table
                Dim insertMedicalDetailsQuery As String = "INSERT INTO MedicalDetails (PatientUPI, BloodPressure, HeartbeatRate, Temperature, Height, Weight, Allergies, CurrentMedication) VALUES (@PatientUPI, @BloodPressure, @HeartbeatRate, @Temperature, @Height, @Weight, @Allergies, @CurrentMedication)"
                Using insertMedicalDetailsCommand As New MySqlCommand(insertMedicalDetailsQuery, connection)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@PatientUPI", patientUPI)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@BloodPressure", bloodPressure)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@HeartbeatRate", heartbeat)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@Temperature", temperature)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@Height", height)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@Weight", weight)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@Allergies", allergies)
                    insertMedicalDetailsCommand.Parameters.AddWithValue("@CurrentMedication", currentMedications)
                    insertMedicalDetailsCommand.ExecuteNonQuery()
                End Using
            End Using

            ' Show success message
            MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ' Show error message if an exception occurs
            MessageBox.Show("Error updating data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerateVisitAndQueueNumbers()
        ' Generate visit ID
        Dim currentDateTime As DateTime = DateTime.Now
        VisitID = currentDateTime.ToString("yyyyMMddHHmmss") & visitCounter.ToString("D3")
        visitCounter += 1

        ' Initialize queue ID
        Dim queueID As String = ""

        ' Generate unique queue ID
        Try
            ' Connect to the database
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Generate queue ID based on the current date and time
                queueID = currentDateTime.ToString("yyyyMMddHHmmssfff") ' Using milliseconds to ensure uniqueness

                ' Check if the queue ID is already present in the database
                Dim isQueueIDExistQuery As String = "SELECT COUNT(*) FROM Queues WHERE QueueID = @QueueID"
                Using command As New MySqlCommand(isQueueIDExistQuery, connection)
                    command.Parameters.AddWithValue("@QueueID", queueID)
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                    ' If the queue ID already exists, generate a new one until a unique one is found
                    Do While count > 0
                        currentDateTime = DateTime.Now
                        queueID = currentDateTime.ToString("yyyyMMddHHmmssfff")
                        command.Parameters("@QueueID").Value = queueID
                        count = Convert.ToInt32(command.ExecuteScalar())
                    Loop
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur during the queue ID generation process
            MessageBox.Show("Error generating queue ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' Assign the generated queue ID to the TextBox or wherever you want to store it
        ' For example:
        TextBoxQueueNo.Text = queueID

        ' Update the queue number based on the number of rows in the queue table
        Try
            ' Connect to the database
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Count the number of existing queue entries
                Dim countQuery As String = "SELECT COUNT(*) FROM Queues"
                Using countCommand As New MySqlCommand(countQuery, connection)
                    Dim queueCount As Integer = Convert.ToInt32(countCommand.ExecuteScalar())

                    ' Increment the queue number based on the count
                    Dim queueNumber As Integer = queueCount + 1

                    ' Assign the generated queue number to the TextBoxQueueNo
                    TextBoxQueueNo.Text = queueNumber.ToString()
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur during the queue number update process
            MessageBox.Show("Error updating queue number: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub TextBoxDateOfBirth_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxDateOfBirth.KeyDown
        ' Check if any key is pressed
        If e.KeyCode <> Keys.None Then
            ' Set focus to the DateTimePicker
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub ClearAllFields()
        ' Clear TextBoxes
        TextBoxPatientUPI.Clear()
        TextBoxNationalID.Clear()
        TextBoxFirstName.Clear()
        TextBoxLastName.Clear()
        TextBoxPhoneNo.Clear()
        TextBoxEmail.Clear()
        ComboBoxGender.SelectedIndex = -1
        TextBoxDateOfBirth.Clear()
        TextBoxHomeAddress.Clear()
        TextBoxQueueNo.Clear()
        TextBoxPatientVisitID.Clear()
        TextBoxNextofKin.Clear()
        TextBoxRelation2Kin.Clear()
        TextBoxNOKcontacts.Clear()
        TextBoxNextofkinaddress.Clear()
        TextBoxBloodPressure.Clear()
        TextBoxHeartBeat.Clear()
        TextBoxTemperature.Clear()
        TextBoxHeight.Clear()
        TextBoxWeight.Clear()
        TextBoxAllergies.Clear()
        TextBoxCurrentMedication.Clear()
        ComboBoxVisitType.SelectedIndex = -1

        ComboBoxDepartmentFrom.SelectedIndex = -1
        ComboBoxDepartmentTO.SelectedIndex = -1
        ComboBoxQueueStatus.SelectedIndex = -1
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            ' Check if the clicked cell is a button cell and its column index is the index of the "PatientDetails" button column
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridView1.Columns("PatientDetails").Index AndAlso TypeOf DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex) Is DataGridViewButtonCell Then
                ' Log message to indicate that the button click event is being handled
                MessageBox.Show("Button clicked event handled.")

                ' Get the row index of the clicked button
                Dim rowIndex As Integer = e.RowIndex

                ' Retrieve patientUPI from the selected row
                Dim patientUPI As String = If(DataGridView1.Rows(rowIndex).Cells("PatientUPI").Value IsNot DBNull.Value, DataGridView1.Rows(rowIndex).Cells("PatientUPI").Value.ToString(), String.Empty)

                ' Log message to indicate that patientUPI is retrieved
                MessageBox.Show("PatientUPI retrieved: " & patientUPI)

                ' Open the PatientTestForm passing the row index and patient UPI
                Dim patientTestForm As New PatientTestForm(rowIndex, patientUPI)
                patientTestForm.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            ' Log message to indicate any errors that occur
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





    Private Function CalculateAge(dateOfBirth As Date, currentDate As Date) As Integer
        ' Calculate the age based on date of birth and current date
        Dim age As Integer = currentDate.Year - dateOfBirth.Year
        If currentDate < dateOfBirth.AddYears(age) Then
            age -= 1
        End If
        Return age
    End Function


End Class
